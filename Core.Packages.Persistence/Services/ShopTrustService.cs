using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Cache;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MagicCarRepairAISupported.Persistence.Services
{
    /// <summary>
    /// Tenant filtresi bypass edilerek tüm servislerin güven metriklerini hesaplar.
    /// Sonuçlar 1 saatlik Redis cache'e alınır; DB'ye her istekte gidilmez.
    /// </summary>
    public class ShopTrustService : IShopTrustService
    {
        private readonly BaseDbContext _db;
        private readonly IRedisCacheService _cache;
        private static readonly TimeSpan CacheTtl = TimeSpan.FromHours(1);
        private const string CacheKeyPrefix = "trust:shop:";

        public ShopTrustService(BaseDbContext db, IRedisCacheService cache)
        {
            _db    = db;
            _cache = cache;
        }

        public async Task<Dictionary<int, ShopTrustMetrics>> GetTrustMetricsAsync(
            IEnumerable<int> clientIds,
            CancellationToken cancellationToken = default)
        {
            var ids = clientIds.ToList();
            var result = new Dictionary<int, ShopTrustMetrics>();

            // Cache'te olanları al, olmayanları DB'den çek
            var missedIds = new List<int>();
            foreach (var id in ids)
            {
                var cached = await _cache.GetCacheValueAsync($"{CacheKeyPrefix}{id}");
                if (cached != null)
                {
                    var metrics = JsonSerializer.Deserialize<ShopTrustMetricsCacheDto>(cached);
                    if (metrics != null)
                        result[id] = new ShopTrustMetrics(metrics.AverageRating, metrics.ReviewCount, metrics.CompletionRate, metrics.AvgQuoteResponseHours);
                }
                else
                {
                    missedIds.Add(id);
                }
            }

            if (missedIds.Count == 0) return result;

            // Sadece cache miss olan ID'ler için DB sorgusu
            var fresh = await FetchFromDbAsync(missedIds, cancellationToken);
            foreach (var (id, metrics) in fresh)
            {
                result[id] = metrics;
                var dto = new ShopTrustMetricsCacheDto(metrics.AverageRating, metrics.ReviewCount, metrics.CompletionRate, metrics.AvgQuoteResponseHours);
                await _cache.SetCacheValueAsync($"{CacheKeyPrefix}{id}", JsonSerializer.Serialize(dto), CacheTtl);
            }

            return result;
        }

        private async Task<Dictionary<int, ShopTrustMetrics>> FetchFromDbAsync(
            List<int> ids,
            CancellationToken cancellationToken)
        {
            var result = new Dictionary<int, ShopTrustMetrics>();
            var since = DateTime.UtcNow.AddDays(-90);

            // 1. Ortalama rating ve yorum sayısı
            var ratings = await _db.ServiceRatings
                .IgnoreQueryFilters()
                .Where(r => ids.Contains(r.ClientId) && r.Status == RatingStatus.Approved)
                .GroupBy(r => r.ClientId)
                .Select(g => new
                {
                    ClientId    = g.Key,
                    AvgRating   = g.Average(r => (double)r.Rating),
                    ReviewCount = g.Count()
                })
                .ToListAsync(cancellationToken);

            // 2. İş emri tamamlanma oranı (son 90 gün)
            var workOrders = await _db.WorkOrders
                .IgnoreQueryFilters()
                .Where(w => ids.Contains(w.ClientId) && w.CreatedDate >= since)
                .GroupBy(w => w.ClientId)
                .Select(g => new
                {
                    ClientId  = g.Key,
                    Total     = g.Count(),
                    Completed = g.Count(w =>
                        w.Status == WorkOrderStatus.Delivered ||
                        w.Status == WorkOrderStatus.ReadyForDelivery)
                })
                .ToListAsync(cancellationToken);

            // 3. Ortalama teklif yanıt süresi (son 90 gün)
            var quoteResponses = await _db.QuoteResponses
                .IgnoreQueryFilters()
                .Where(r => ids.Contains(r.ClientId) &&
                            r.CreatedDate >= since &&
                            r.CreatedDate != null)
                .Select(r => new { r.ClientId, r.QuoteDate, r.CreatedDate })
                .ToListAsync(cancellationToken);

            var responseHoursMap = quoteResponses
                .Where(r => r.CreatedDate.HasValue)
                .Select(r => new
                {
                    r.ClientId,
                    Hours = (r.QuoteDate - r.CreatedDate!.Value).TotalHours
                })
                .Where(x => x.Hours >= 0 && x.Hours < 720)
                .GroupBy(x => x.ClientId)
                .ToDictionary(g => g.Key, g => g.Average(x => x.Hours));

            // Birleştir
            foreach (var id in ids)
            {
                var rat  = ratings.FirstOrDefault(r => r.ClientId == id);
                var wo   = workOrders.FirstOrDefault(w => w.ClientId == id);
                responseHoursMap.TryGetValue(id, out var avgH);

                result[id] = new ShopTrustMetrics(
                    AverageRating:         rat?.AvgRating   ?? 0d,
                    ReviewCount:           rat?.ReviewCount  ?? 0,
                    CompletionRate:        wo is { Total: > 0 }
                                               ? (double)wo.Completed / wo.Total
                                               : 0d,
                    AvgQuoteResponseHours: avgH == 0 ? null : (double?)avgH
                );
            }

            return result;
        }
    }

    // JSON serializasyon için yalın record — ShopTrustMetrics'in serileşebilir kopyası
    internal record ShopTrustMetricsCacheDto(
        double AverageRating,
        int ReviewCount,
        double CompletionRate,
        double? AvgQuoteResponseHours);
}
