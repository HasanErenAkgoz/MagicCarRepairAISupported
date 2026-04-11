using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Commands.SubmitWorkOrderReview
{
    public class SubmitWorkOrderReviewCommandHandler : IRequestHandler<SubmitWorkOrderReviewCommand, SubmitWorkOrderReviewResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IEntityRepository<ServiceRating, int> _ratingRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenantService _tenantService;

        public SubmitWorkOrderReviewCommandHandler(
            ICustomerRepository customerRepository,
            IWorkOrderRepository workOrderRepository,
            IEntityRepository<ServiceRating, int> ratingRepository,
            IHttpContextAccessor httpContextAccessor,
            ITenantService tenantService)
        {
            _customerRepository = customerRepository;
            _workOrderRepository = workOrderRepository;
            _ratingRepository = ratingRepository;
            _httpContextAccessor = httpContextAccessor;
            _tenantService = tenantService;
        }

        public async Task<SubmitWorkOrderReviewResponse> Handle(SubmitWorkOrderReviewCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                throw new UnauthorizedAccessException("User not authenticated");

            var clientId = _tenantService.GetCurrentClientId();
            if (!clientId.HasValue)
                throw new UnauthorizedAccessException("Client ID not found");

            var customers = await _customerRepository.GetListAsync(cancellationToken, c => c.UserId == userId && c.ClientId == clientId.Value);
            var currentCustomer = customers.FirstOrDefault();
            if (currentCustomer == null)
                throw new UnauthorizedAccessException("Customer not found");

            var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId, cancellationToken);
            if (workOrder == null)
                throw new InvalidOperationException("Work order not found");

            if (workOrder.CustomerId != currentCustomer.Id)
                throw new UnauthorizedAccessException("You don't have permission to review this work order");

            var existingRatings = await _ratingRepository.GetListAsync(cancellationToken,
                r => r.WorkOrderId == request.WorkOrderId && r.CustomerId == currentCustomer.Id);
            if (existingRatings.Any())
                throw new InvalidOperationException("You have already reviewed this work order");

            var clampedRating = Math.Max(1, Math.Min(5, request.Rating));
            var rating = new ServiceRating
            {
                WorkOrderId = request.WorkOrderId,
                CustomerId = currentCustomer.Id,
                ClientId = clientId.Value,
                Rating = clampedRating,
                ServiceQuality = clampedRating,
                PriceValue = clampedRating,
                OnTimeDelivery = clampedRating,
                StaffBehavior = clampedRating,
                Comment = request.Comment
            };

            await _ratingRepository.AddAsync(rating, cancellationToken);
            await _ratingRepository.SaveChangesAsync();

            return new SubmitWorkOrderReviewResponse
            {
                Id = rating.Id,
                WorkOrderId = rating.WorkOrderId,
                Rating = rating.Rating,
                Comment = rating.Comment
            };
        }
    }
}
