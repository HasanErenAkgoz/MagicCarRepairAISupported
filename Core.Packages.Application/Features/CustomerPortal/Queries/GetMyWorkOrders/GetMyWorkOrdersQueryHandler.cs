using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Queries.GetMyWorkOrders
{
    public class GetMyWorkOrdersQueryHandler : IRequestHandler<GetMyWorkOrdersQuery, List<GetMyWorkOrdersResponse>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenantService _tenantService;

        public GetMyWorkOrdersQueryHandler(
            ICustomerRepository customerRepository,
            IWorkOrderRepository workOrderRepository,
            IHttpContextAccessor httpContextAccessor,
            ITenantService tenantService)
        {
            _customerRepository = customerRepository;
            _workOrderRepository = workOrderRepository;
            _httpContextAccessor = httpContextAccessor;
            _tenantService = tenantService;
        }

        public async Task<List<GetMyWorkOrdersResponse>> Handle(GetMyWorkOrdersQuery request, CancellationToken cancellationToken)
        {
            // Get current user ID from HttpContext
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("User not authenticated");
            }

            var clientId = _tenantService.GetCurrentClientId();
            if (!clientId.HasValue)
            {
                throw new UnauthorizedAccessException("Client ID not found");
            }

            // Find customer by UserId
            var customers = await _customerRepository.GetListAsync(cancellationToken, c => c.UserId == userId && c.ClientId == clientId.Value);
            var currentCustomer = customers.FirstOrDefault();
            
            if (currentCustomer == null)
            {
                return new List<GetMyWorkOrdersResponse>();
            }

            // Get work orders for this customer
            var workOrders = await _workOrderRepository.GetByCustomerIdAsync(currentCustomer.Id, cancellationToken);

            // Filter by active status if requested
            if (request.ActiveOnly == true)
            {
                workOrders = workOrders.Where(wo => 
                    wo.Status != WorkOrderStatus.Delivered && 
                    wo.Status != WorkOrderStatus.Cancelled).ToList();
            }

            return workOrders.Select(wo => new GetMyWorkOrdersResponse
            {
                Id = wo.Id,
                WorkOrderNumber = wo.WorkOrderNumber,
                VehicleId = wo.VehicleId,
                VehicleLicensePlate = wo.Vehicle?.LicensePlate ?? "",
                VehicleBrand = wo.Vehicle?.Brand ?? "",
                VehicleModel = wo.Vehicle?.Model ?? "",
                EntryDate = wo.EntryDate,
                EstimatedDeliveryDate = wo.EstimatedDeliveryDate,
                ActualDeliveryDate = wo.ActualDeliveryDate,
                Status = wo.Status.ToString(),
                StatusName = wo.Status.ToString(),
                Priority = wo.Priority.ToString(),
                TotalAmount = wo.TotalAmount,
                PaymentStatus = wo.PaymentStatus.ToString()
            }).OrderByDescending(wo => wo.EntryDate).ToList();
        }
    }
}

