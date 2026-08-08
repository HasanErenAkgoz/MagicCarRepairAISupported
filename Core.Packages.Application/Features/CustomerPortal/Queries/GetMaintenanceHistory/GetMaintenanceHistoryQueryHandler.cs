using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Queries.GetMaintenanceHistory
{
    public class GetMaintenanceHistoryQueryHandler : IRequestHandler<GetMaintenanceHistoryQuery, List<GetMaintenanceHistoryResponse>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenantService _tenantService;

        public GetMaintenanceHistoryQueryHandler(
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

        public async Task<List<GetMaintenanceHistoryResponse>> Handle(GetMaintenanceHistoryQuery request, CancellationToken cancellationToken)
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
            var currentCustomer = await _customerRepository.GetByUserIdForTenantAsync(
                userId, clientId.Value, cancellationToken);
            
            if (currentCustomer == null)
            {
                return new List<GetMaintenanceHistoryResponse>();
            }

            // Get completed work orders for this customer
            var workOrders = await _workOrderRepository.GetByCustomerIdAsync(currentCustomer.Id, cancellationToken);

            // Filter by vehicle if specified
            if (request.VehicleId.HasValue)
            {
                workOrders = workOrders.Where(wo => wo.VehicleId == request.VehicleId.Value).ToList();
            }

            // Only return delivered work orders
            workOrders = workOrders.Where(wo => wo.Status == WorkOrderStatus.Delivered).ToList();

            return workOrders.OrderByDescending(wo => wo.EntryDate).Select(wo => new GetMaintenanceHistoryResponse
            {
                WorkOrderId = wo.Id,
                WorkOrderNumber = wo.WorkOrderNumber,
                VehicleId = wo.VehicleId,
                VehicleLicensePlate = wo.Vehicle?.LicensePlate ?? "",
                VehicleBrand = wo.Vehicle?.Brand ?? "",
                VehicleModel = wo.Vehicle?.Model ?? "",
                EntryDate = wo.EntryDate,
                CompletedDate = wo.ActualDeliveryDate ?? wo.EntryDate,
                Status = wo.Status.ToString(),
                TotalAmount = wo.TotalAmount,
                Description = wo.CustomerComplaints
            }).ToList();
        }
    }
}

