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
        private readonly IClientRepository _clientRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenantService _tenantService;

        public GetMyWorkOrdersQueryHandler(
            ICustomerRepository customerRepository,
            IWorkOrderRepository workOrderRepository,
            IClientRepository clientRepository,
            IHttpContextAccessor httpContextAccessor,
            ITenantService tenantService)
        {
            _customerRepository = customerRepository;
            _workOrderRepository = workOrderRepository;
            _clientRepository = clientRepository;
            _httpContextAccessor = httpContextAccessor;
            _tenantService = tenantService;
        }

        public async Task<List<GetMyWorkOrdersResponse>> Handle(GetMyWorkOrdersQuery request, CancellationToken cancellationToken)
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
                return new List<GetMyWorkOrdersResponse>();

            var client = await _clientRepository.GetByIdAsync(clientId.Value, cancellationToken);
            var shopName = client?.Name;

            var workOrders = await _workOrderRepository.GetByCustomerIdAsync(currentCustomer.Id, cancellationToken);

            if (request.ActiveOnly == true)
            {
                workOrders = workOrders.Where(wo =>
                    wo.Status != WorkOrderStatus.Delivered &&
                    wo.Status != WorkOrderStatus.Cancelled).ToList();
            }

            return workOrders.Select(wo => new GetMyWorkOrdersResponse
            {
                Id = wo.Id,
                OrderNo = wo.WorkOrderNumber,
                VehicleBrand = wo.Vehicle?.Brand ?? "",
                VehicleModel = wo.Vehicle?.Model ?? "",
                VehiclePlate = wo.Vehicle?.LicensePlate ?? "",
                ServiceTitle = !string.IsNullOrWhiteSpace(wo.CustomerComplaints)
                    ? (wo.CustomerComplaints.Length > 100 ? wo.CustomerComplaints.Substring(0, 100) + "..." : wo.CustomerComplaints)
                    : wo.WorkOrderNumber,
                ShopName = shopName,
                CreatedAt = wo.EntryDate,
                EstimatedDeliveryDate = wo.EstimatedDeliveryDate,
                ActualDeliveryDate = wo.ActualDeliveryDate,
                Status = wo.Status.ToString(),
                StatusName = wo.Status.ToString(),
                Total = wo.TotalAmount,
                PaymentStatus = wo.PaymentStatus.ToString(),
                RequiresCustomerApproval = wo.CustomerApprovalStatus == null ||
                    wo.CustomerApprovalStatus == CustomerApprovalStatus.Pending
            }).OrderByDescending(wo => wo.CreatedAt).ToList();
        }
    }
}
