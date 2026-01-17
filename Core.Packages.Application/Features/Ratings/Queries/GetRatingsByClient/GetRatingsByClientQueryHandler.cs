using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Ratings.Queries.GetRatingsByClient
{
    public class GetRatingsByClientQueryHandler : IRequestHandler<GetRatingsByClientQuery, GetRatingsByClientResponse>
    {
        private readonly IServiceRatingRepository _serviceRatingRepository;
        private readonly ITenantService _tenantService;

        public GetRatingsByClientQueryHandler(
            IServiceRatingRepository serviceRatingRepository,
            ITenantService tenantService)
        {
            _serviceRatingRepository = serviceRatingRepository;
            _tenantService = tenantService;
        }

        public async Task<GetRatingsByClientResponse> Handle(GetRatingsByClientQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 0;

            // Get all ratings for the client
            var allRatings = await _serviceRatingRepository.GetByClientIdAsync(clientId, cancellationToken);

            // Filter by status if requested
            if (request.OnlyApproved == true)
            {
                allRatings = allRatings.Where(r => r.Status == RatingStatus.Approved).ToList();
            }

            var totalCount = allRatings.Count;

            var ratings = allRatings
                .OrderByDescending(r => r.CreatedDate)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(r => new ServiceRatingDto
                {
                    Id = r.Id,
                    WorkOrderId = r.WorkOrderId,
                    CustomerName = $"{r.Customer?.FirstName ?? ""} {r.Customer?.LastName ?? ""}".Trim(),
                    VehicleInfo = r.WorkOrder != null && r.WorkOrder.Vehicle != null
                        ? $"{r.WorkOrder.Vehicle.Brand} {r.WorkOrder.Vehicle.Model} ({r.WorkOrder.Vehicle.LicensePlate})"
                        : "N/A",
                    Rating = r.Rating,
                    AverageRating = r.CalculateAverageRating(),
                    ServiceQuality = r.ServiceQuality,
                    PriceValue = r.PriceValue,
                    OnTimeDelivery = r.OnTimeDelivery,
                    StaffBehavior = r.StaffBehavior,
                    Comment = r.Comment,
                    ServiceReply = r.ServiceReply,
                    Status = r.Status,
                    CreatedDate = r.CreatedDate ?? DateTime.UtcNow
                })
                .ToList();

            var averageRating = await _serviceRatingRepository.GetAverageRatingByClientIdAsync(clientId, cancellationToken);

            return new GetRatingsByClientResponse
            {
                Ratings = ratings,
                TotalCount = totalCount,
                AverageRating = averageRating,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
