using MagicCarRepairAISupported.Application.Common.Services;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenantService _tenantService;

        public GetMyWorkOrderDetailsQueryHandler(
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

        public async Task<GetMyWorkOrderDetailsResponse> Handle(GetMyWorkOrderDetailsQuery request, CancellationToken cancellationToken)
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
                throw new UnauthorizedAccessException("Customer not found");
            }

            // Get work order with details
            var workOrder = await _workOrderRepository.GetWithDetailsAsync(request.WorkOrderId, cancellationToken);
            if (workOrder == null)
            {
                throw new InvalidOperationException("Work order not found");
            }

            // Verify that the work order belongs to the current customer
            if (workOrder.CustomerId != currentCustomer.Id)
            {
                throw new UnauthorizedAccessException("You don't have permission to view this work order");
            }

            var response = new GetMyWorkOrderDetailsResponse
            {
                Id = workOrder.Id,
                WorkOrderNumber = workOrder.WorkOrderNumber,
                VehicleId = workOrder.VehicleId,
                VehicleLicensePlate = workOrder.Vehicle?.LicensePlate ?? "",
                VehicleBrand = workOrder.Vehicle?.Brand ?? "",
                VehicleModel = workOrder.Vehicle?.Model ?? "",
                Year = workOrder.Vehicle?.Year ?? 0,
                Color = workOrder.Vehicle?.Color ?? "",
                Kilometers = workOrder.Kilometers,
                FuelLevel = workOrder.FuelLevel,
                EntryDate = workOrder.EntryDate,
                EstimatedDeliveryDate = workOrder.EstimatedDeliveryDate,
                ActualDeliveryDate = workOrder.ActualDeliveryDate,
                Status = workOrder.Status.ToString(),
                StatusName = workOrder.Status.ToString(),
                Priority = workOrder.Priority.ToString(),
                CustomerComplaints = workOrder.CustomerComplaints,
                SpecialRequests = workOrder.SpecialRequests,
                SubTotal = workOrder.SubTotal,
                DiscountAmount = workOrder.DiscountAmount,
                TaxAmount = workOrder.TaxAmount,
                TotalAmount = workOrder.TotalAmount,
                PaymentStatus = workOrder.PaymentStatus.ToString()
            };

            // Items
            if (workOrder.Items != null)
            {
                response.Items = workOrder.Items.Select(i => new WorkOrderItemDto
                {
                    Id = i.Id,
                    ItemType = i.ItemType.ToString(),
                    ItemTypeName = i.ItemType.ToString(),
                    PartId = i.PartId,
                    PartName = i.Part?.Name,
                    Description = i.Description,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalAmount = i.TotalAmount,
                    BrandType = i.BrandType?.ToString()
                }).ToList();
            }

            // Labors
            if (workOrder.Labors != null)
            {
                response.Labors = workOrder.Labors.Select(l => new WorkOrderLaborDto
                {
                    Id = l.Id,
                    EmployeeId = l.EmployeeId,
                    EmployeeName = l.Employee?.FullName,
                    OperationName = l.OperationName,
                    StartTime = l.StartTime,
                    EndTime = l.EndTime,
                    DurationHours = l.DurationHours,
                    HourlyRate = l.HourlyRate,
                    TotalAmount = l.TotalAmount
                }).ToList();
            }

            // Timeline
            if (workOrder.Timeline != null)
            {
                response.Timeline = workOrder.Timeline.OrderBy(t => t.EventDate).Select(t => new WorkOrderTimelineDto
                {
                    Id = t.Id,
                    EventDate = t.EventDate,
                    OldStatus = t.OldStatus?.ToString(),
                    NewStatus = t.NewStatus?.ToString(),
                    StatusChangeText = t.OldStatus.HasValue && t.NewStatus.HasValue 
                        ? $"{t.OldStatus} → {t.NewStatus}" 
                        : t.Description,
                    EmployeeId = t.EmployeeId,
                    EmployeeName = t.Employee?.FullName,
                    Description = t.Description,
                    EventType = t.EventType?.ToString()
                }).ToList();
            }

            // Photos
            if (workOrder.Photos != null)
            {
                response.Photos = workOrder.Photos.Select(p => new WorkOrderPhotoDto
                {
                    Id = p.Id,
                    FilePath = p.FilePath,
                    Description = p.Description,
                    PhotoType = p.PhotoType.ToString(),
                    PhotoTypeName = p.PhotoType.ToString(),
                    UploadDate = p.UploadDate
                }).ToList();
            }

            return response;
        }
    }
}

