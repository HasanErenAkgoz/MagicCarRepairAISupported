using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Ratings.Commands.CreateRating
{
    public class CreateRatingCommandHandler : IRequestHandler<CreateRatingCommand, CreateRatingResponse>
    {
        private readonly IServiceRatingRepository _serviceRatingRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public CreateRatingCommandHandler(
            IServiceRatingRepository serviceRatingRepository,
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _serviceRatingRepository = serviceRatingRepository;
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<CreateRatingResponse> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            // Validate WorkOrder
            var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId);
            if (workOrder == null || workOrder.ClientId != clientId)
            {
                throw new DomainException("WORKORDER_NOT_FOUND", new { WorkOrderId = request.WorkOrderId });
            }

            // Check if WorkOrder is completed
            // WorkOrder can only be rated if it's delivered
            if (workOrder.Status != WorkOrderStatus.Delivered)
            {
                throw new DomainException("RATING_ONLY_FOR_COMPLETED_WORKORDERS", new { WorkOrderId = request.WorkOrderId, Status = workOrder.Status });
            }

            // Check if rating already exists for this WorkOrder
            var existingRating = await _serviceRatingRepository.GetByWorkOrderIdAsync(request.WorkOrderId, cancellationToken);
            if (existingRating != null)
            {
                throw new DomainException("RATING_ALREADY_EXISTS_FOR_WORKORDER", new { WorkOrderId = request.WorkOrderId });
            }

            // Validate rating values (1-5)
            if (request.Rating < 1 || request.Rating > 5 ||
                request.ServiceQuality < 1 || request.ServiceQuality > 5 ||
                request.PriceValue < 1 || request.PriceValue > 5 ||
                request.OnTimeDelivery < 1 || request.OnTimeDelivery > 5 ||
                request.StaffBehavior < 1 || request.StaffBehavior > 5)
            {
                throw new DomainException("RATING_VALUE_MUST_BE_BETWEEN_1_AND_5");
            }

            var rating = new ServiceRating
            {
                WorkOrderId = request.WorkOrderId,
                CustomerId = workOrder.CustomerId,
                ClientId = clientId,
                Rating = request.Rating,
                ServiceQuality = request.ServiceQuality,
                PriceValue = request.PriceValue,
                OnTimeDelivery = request.OnTimeDelivery,
                StaffBehavior = request.StaffBehavior,
                Comment = request.Comment,
                Photos = request.Photos,
                Status = RatingStatus.Pending // Admin onayı bekliyor
            };

            await _serviceRatingRepository.AddAsync(rating, cancellationToken);

            var response = _mapper.Map<CreateRatingResponse>(rating);
            response.AverageRating = rating.CalculateAverageRating();

            return response;
        }
    }
}
