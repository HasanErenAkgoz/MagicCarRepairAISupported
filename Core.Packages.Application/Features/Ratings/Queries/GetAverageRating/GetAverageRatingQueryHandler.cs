using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Ratings.Queries.GetAverageRating
{
    public class GetAverageRatingQueryHandler : IRequestHandler<GetAverageRatingQuery, GetAverageRatingResponse>
    {
        private readonly IServiceRatingRepository _serviceRatingRepository;
        private readonly ITenantService _tenantService;

        public GetAverageRatingQueryHandler(
            IServiceRatingRepository serviceRatingRepository,
            ITenantService tenantService)
        {
            _serviceRatingRepository = serviceRatingRepository;
            _tenantService = tenantService;
        }

        public async Task<GetAverageRatingResponse> Handle(GetAverageRatingQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 0;

            var approvedRatings = _serviceRatingRepository.Query()
                .Where(r => r.ClientId == clientId && r.Status == RatingStatus.Approved)
                .ToList();

            var totalRatings = approvedRatings.Count;
            var averageRating = totalRatings > 0 
                ? Math.Round(approvedRatings.Average(r => r.CalculateAverageRating()), 2)
                : 0m;

            var ratingDistribution = new GetAverageRatingResponse
            {
                AverageRating = averageRating,
                TotalRatings = totalRatings,
                Rating5 = approvedRatings.Count(r => r.Rating == 5),
                Rating4 = approvedRatings.Count(r => r.Rating == 4),
                Rating3 = approvedRatings.Count(r => r.Rating == 3),
                Rating2 = approvedRatings.Count(r => r.Rating == 2),
                Rating1 = approvedRatings.Count(r => r.Rating == 1)
            };

            return ratingDistribution;
        }
    }
}
