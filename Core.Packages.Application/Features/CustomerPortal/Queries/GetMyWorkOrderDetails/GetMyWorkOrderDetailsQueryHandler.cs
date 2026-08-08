using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Queries.GetMyWorkOrderDetails
{
    public class GetMyWorkOrderDetailsQueryHandler : IRequestHandler<GetMyWorkOrderDetailsQuery, GetMyWorkOrderDetailsResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenantService _tenantService;

        public GetMyWorkOrderDetailsQueryHandler(
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

        public async Task<GetMyWorkOrderDetailsResponse> Handle(GetMyWorkOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                throw new UnauthorizedAccessException("User not authenticated");

            var clientId = _tenantService.GetCurrentClientId();
            if (!clientId.HasValue)
                throw new UnauthorizedAccessException("Client ID not found");

            var currentCustomer = await _customerRepository.GetByUserIdForTenantAsync(
                userId, clientId.Value, cancellationToken);
            if (currentCustomer == null)
                throw new UnauthorizedAccessException("Customer not found");

            var workOrder = await _workOrderRepository.GetWithDetailsAsync(request.WorkOrderId, cancellationToken);
            if (workOrder == null)
                throw new InvalidOperationException("Work order not found");

            if (workOrder.CustomerId != currentCustomer.Id)
                throw new UnauthorizedAccessException("You don't have permission to view this work order");

            var client = await _clientRepository.GetByIdAsync(clientId.Value, cancellationToken);

            var serviceTitle = !string.IsNullOrWhiteSpace(workOrder.CustomerComplaints)
                ? (workOrder.CustomerComplaints.Length > 100 ? workOrder.CustomerComplaints.Substring(0, 100) + "..." : workOrder.CustomerComplaints)
                : workOrder.WorkOrderNumber;

            var partsSubtotal = workOrder.Items?.Sum(i => i.TotalAmount) ?? 0;
            var laborSubtotal = workOrder.Labors?.Sum(l => l.TotalAmount) ?? 0;

            var response = new GetMyWorkOrderDetailsResponse
            {
                Id = workOrder.Id,
                OrderNo = workOrder.WorkOrderNumber,
                Status = workOrder.Status.ToString(),
                StatusName = workOrder.Status.ToString(),
                CreatedAt = workOrder.EntryDate,
                EstimatedDeliveryDate = workOrder.EstimatedDeliveryDate,
                ActualDeliveryDate = workOrder.ActualDeliveryDate,
                Vehicle = workOrder.Vehicle != null ? new WorkOrderVehicleDto
                {
                    Id = workOrder.Vehicle.Id,
                    Brand = workOrder.Vehicle.Brand,
                    Model = workOrder.Vehicle.Model,
                    Year = workOrder.Vehicle.Year,
                    Plate = workOrder.Vehicle.LicensePlate
                } : new WorkOrderVehicleDto(),
                ServiceTitle = serviceTitle,
                ServiceDescription = workOrder.SpecialRequests,
                TechnicianNotes = workOrder.Notes,
                ShopName = client?.Name,
                ShopUserId = workOrder.AssignedEmployee?.UserId,
                ShopUserName = workOrder.AssignedEmployee?.FullName,
                PartsSubtotal = partsSubtotal,
                LaborSubtotal = laborSubtotal,
                DiscountAmount = workOrder.DiscountAmount,
                TaxAmount = workOrder.TaxAmount,
                Total = workOrder.TotalAmount,
                PaymentStatus = workOrder.PaymentStatus.ToString(),
                RequiresCustomerApproval = workOrder.CustomerApprovalStatus == null ||
                    workOrder.CustomerApprovalStatus == CustomerApprovalStatus.Pending,
                Parts = workOrder.Items?.Select(i => new WorkOrderPartDto
                {
                    Id = i.Id.ToString(),
                    Name = i.Part?.Name ?? i.Description ?? "Part",
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Total = i.TotalAmount
                }).ToList() ?? new List<WorkOrderPartDto>(),
                Labor = workOrder.Labors?.Select(l => new WorkOrderLaborDto
                {
                    Id = l.Id.ToString(),
                    Description = l.OperationName,
                    Hours = l.DurationHours ?? 0,
                    HourlyRate = l.HourlyRate,
                    Total = l.TotalAmount
                }).ToList() ?? new List<WorkOrderLaborDto>(),
                Timeline = workOrder.Timeline?.OrderBy(t => t.EventDate).Select(t => new WorkOrderTimelineDto
                {
                    Id = t.Id.ToString(),
                    Status = t.NewStatus?.ToString() ?? t.EventType?.ToString() ?? "",
                    Note = t.Description,
                    CreatedAt = t.EventDate,
                    CreatedBy = t.Employee?.FullName ?? "System"
                }).ToList() ?? new List<WorkOrderTimelineDto>(),
                Photos = workOrder.Photos?.Select(p => new WorkOrderPhotoDto
                {
                    Id = p.Id,
                    MediaUrl = $"/api/customer-media/customers/{currentCustomer.Id}/work-orders/{workOrder.Id}/photos/{p.Id}",
                    Url = p.FilePath,
                    PhotoType = (int)p.PhotoType,
                    Description = p.Description,
                    UploadedAt = p.UploadDate
                }).ToList() ?? new List<WorkOrderPhotoDto>()
            };

            return response;
        }
    }
}
