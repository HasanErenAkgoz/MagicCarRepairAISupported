namespace MagicCarRepairAISupported.Application.Common.Services
{
    /// <summary>
    /// Servis başına gerçek zamanlı güven metriklerini hesaplar.
    /// Tenant filtresi bypass edilerek çapraz servis sorgulama yapar.
    /// </summary>
    public interface IShopTrustService
    {
        /// <summary>
        /// Verilen clientId listesi için güven metriklerini hesaplar.
        /// </summary>
        Task<Dictionary<int, ShopTrustMetrics>> GetTrustMetricsAsync(
            IEnumerable<int> clientIds,
            CancellationToken cancellationToken = default);
    }

    public record ShopTrustMetrics(
        /// <summary>ServiceRating ortalama puanı (0–5).</summary>
        double AverageRating,
        /// <summary>Onaylı yorum sayısı.</summary>
        int ReviewCount,
        /// <summary>Son 90 günde tamamlanan / toplam iş emri oranı (0–1).</summary>
        double CompletionRate,
        /// <summary>Son 90 günde QuoteResponse ortalama yanıt süresi (saat). Null = veri yok.</summary>
        double? AvgQuoteResponseHours
    )
    {
        public static ShopTrustMetrics Empty => new(0, 0, 0, null);
    }
}
