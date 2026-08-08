using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Ratings.Queries.GetRatingStatistics
{
    public class GetRatingStatisticsQueryHandler : IRequestHandler<GetRatingStatisticsQuery, GetRatingStatisticsResponse>
    {
        private readonly IServiceRatingRepository _ratingRepository;
        private readonly ITenantService _tenantService;

        public GetRatingStatisticsQueryHandler(
            IServiceRatingRepository ratingRepository,
            ITenantService tenantService)
        {
            _ratingRepository = ratingRepository;
            _tenantService = tenantService;
        }

        public async Task<GetRatingStatisticsResponse> Handle(GetRatingStatisticsQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            var query = _ratingRepository.Query()
                .Where(r => r.ClientId == clientId);

            // Tarih filtresi
            if (request.StartDate.HasValue)
            {
                query = query.Where(r => r.CreatedDate >= request.StartDate.Value);
            }

            if (request.EndDate.HasValue)
            {
                query = query.Where(r => r.CreatedDate <= request.EndDate.Value);
            }

            var ratings = await query.ToListAsync(cancellationToken);

            if (ratings.Count == 0)
            {
                return new GetRatingStatisticsResponse();
            }

            // Ortalamalar
            var averageRating = (decimal)ratings.Average(r => r.Rating);
            var averageServiceQuality = (decimal)ratings.Average(r => r.ServiceQuality);
            var averagePriceValue = (decimal)ratings.Average(r => r.PriceValue);
            var averageOnTimeDelivery = (decimal)ratings.Average(r => r.OnTimeDelivery);
            var averageStaffBehavior = (decimal)ratings.Average(r => r.StaffBehavior);

            // Dağılım
            var distribution = new RatingDistribution
            {
                FiveStar = ratings.Count(r => r.Rating == 5),
                FourStar = ratings.Count(r => r.Rating == 4),
                ThreeStar = ratings.Count(r => r.Rating == 3),
                TwoStar = ratings.Count(r => r.Rating == 2),
                OneStar = ratings.Count(r => r.Rating == 1)
            };

            // Aylık trend
            var monthNames = new[] { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", 
                                     "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };

            var monthlyTrends = ratings
                .Where(r => r.CreatedDate.HasValue)
                .GroupBy(r => new { Year = r.CreatedDate!.Value.Year, Month = r.CreatedDate!.Value.Month })
                .Select(g => new MonthlyRatingTrend
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    MonthName = monthNames[g.Key.Month - 1],
                    AverageRating = (decimal)g.Average(r => r.Rating),
                    RatingCount = g.Count()
                })
                .OrderBy(m => m.Year)
                .ThenBy(m => m.Month)
                .ToList();

            // Kategori ortalamaları
            var categoryAverages = new List<CategoryAverage>
            {
                new CategoryAverage
                {
                    Category = "Hizmet Kalitesi",
                    Average = averageServiceQuality,
                    Count = ratings.Count
                },
                new CategoryAverage
                {
                    Category = "Fiyat Uygunluğu",
                    Average = averagePriceValue,
                    Count = ratings.Count
                },
                new CategoryAverage
                {
                    Category = "Zamanında Teslim",
                    Average = averageOnTimeDelivery,
                    Count = ratings.Count
                },
                new CategoryAverage
                {
                    Category = "Personel Davranışı",
                    Average = averageStaffBehavior,
                    Count = ratings.Count
                }
            };

            return new GetRatingStatisticsResponse
            {
                TotalRatings = ratings.Count,
                AverageRating = Math.Round(averageRating, 2),
                AverageServiceQuality = Math.Round(averageServiceQuality, 2),
                AveragePriceValue = Math.Round(averagePriceValue, 2),
                AverageOnTimeDelivery = Math.Round(averageOnTimeDelivery, 2),
                AverageStaffBehavior = Math.Round(averageStaffBehavior, 2),
                Distribution = distribution,
                MonthlyTrends = monthlyTrends,
                CategoryAverages = categoryAverages
            };
        }
    }
}
