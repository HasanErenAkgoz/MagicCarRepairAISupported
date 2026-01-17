using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Ratings.Queries.GetRatingsByWorkOrder
{
    public class GetRatingsByWorkOrderQueryHandler : IRequestHandler<GetRatingsByWorkOrderQuery, GetRatingsByWorkOrderResponse>
    {
        private readonly IServiceRatingRepository _ratingRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;

        public GetRatingsByWorkOrderQueryHandler(
            IServiceRatingRepository ratingRepository,
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService)
        {
            _ratingRepository = ratingRepository;
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
        }

        public async Task<GetRatingsByWorkOrderResponse> Handle(GetRatingsByWorkOrderQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // WorkOrder kontrolü
            var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId);
            if (workOrder == null || workOrder.ClientId != clientId)
            {
                return new GetRatingsByWorkOrderResponse
                {
                    WorkOrderId = request.WorkOrderId,
                    WorkOrderNumber = string.Empty,
                    Ratings = new List<RatingItem>(),
                    TotalCount = 0
                };
            }

            // Rating'leri getir
            var ratings = await _ratingRepository.Query()
                .Include(r => r.Customer)
                .Where(r => r.WorkOrderId == request.WorkOrderId && r.ClientId == clientId)
                .OrderByDescending(r => r.CreatedDate)
                .ToListAsync(cancellationToken);

            var ratingItems = ratings.Select(r => new RatingItem
            {
                Id = r.Id,
                CustomerId = r.CustomerId,
                CustomerName = r.Customer?.FullName ?? "Bilinmeyen Müşteri",
                OverallRating = r.Rating,
                ServiceQuality = r.ServiceQuality,
                PriceValue = r.PriceValue,
                OnTimeDelivery = r.OnTimeDelivery,
                StaffBehavior = r.StaffBehavior,
                Comment = r.Comment,
                Reply = r.ServiceReply,
                CreatedDate = r.CreatedDate ?? DateTime.UtcNow,
                ReplyDate = r.ServiceReplyDate
            }).ToList();

            return new GetRatingsByWorkOrderResponse
            {
                WorkOrderId = request.WorkOrderId,
                WorkOrderNumber = workOrder.WorkOrderNumber,
                Ratings = ratingItems,
                TotalCount = ratingItems.Count
            };
        }
    }
}
