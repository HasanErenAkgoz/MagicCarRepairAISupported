using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Messages;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetById
{
    public class GetWorkOrderByIdQueryHandler : IRequestHandler<GetWorkOrderByIdQuery, GetWorkOrderByIdResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetWorkOrderByIdQueryHandler(
            IWorkOrderRepository workOrderRepository,
            IMapper mapper,
            ITenantService tenantService)
        {
            _workOrderRepository = workOrderRepository;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<GetWorkOrderByIdResponse> Handle(GetWorkOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            var workOrder = await _workOrderRepository.GetWithDetailsAsync(request.Id, cancellationToken);
            if (workOrder == null)
            {
                throw new DomainException(Messages.NotFound, new { Entity = "WorkOrder", Id = request.Id });
            }

            // Client kontrolü
            if (workOrder.ClientId != clientId)
            {
                throw new DomainException("WORKORDER_NOT_BELONG_TO_CLIENT", new { WorkOrderId = request.Id });
            }

            var response = _mapper.Map<GetWorkOrderByIdResponse>(workOrder);
            
            // Enum isimleri
            response.StatusName = workOrder.Status.ToString();
            response.PriorityName = workOrder.Priority.ToString();
            response.PaymentStatusName = workOrder.PaymentStatus.ToString();

            // Vehicle bilgileri
            if (workOrder.Vehicle != null)
            {
                response.Vehicle = new VehicleDetailDto
                {
                    Id = workOrder.Vehicle.Id,
                    LicensePlate = workOrder.Vehicle.LicensePlate,
                    Brand = workOrder.Vehicle.Brand,
                    Model = workOrder.Vehicle.Model,
                    Year = workOrder.Vehicle.Year,
                    Vin = workOrder.Vehicle.Vin,
                    ModelVariant = workOrder.Vehicle.ModelVariant,
                    Trim = workOrder.Vehicle.Trim,
                    Photos = workOrder.Vehicle.Photos?
                        .OrderBy(p => p.DisplayOrder)
                        .ThenBy(p => p.UploadDate)
                        .Select(MapVehiclePhoto)
                        .Where(p => !string.IsNullOrWhiteSpace(p.FilePath))
                        .ToList() ?? new List<VehiclePhotoDto>()
                };
            }

            // Customer bilgileri
            if (workOrder.Customer != null)
            {
                response.Customer = new CustomerDetailDto
                {
                    Id = workOrder.Customer.Id,
                    FirstName = workOrder.Customer.FirstName,
                    LastName = workOrder.Customer.LastName,
                    FullName = workOrder.Customer.FullName,
                    Phone = workOrder.Customer.PhoneNumber,
                    Avatar = workOrder.Customer.Avatar
                };
            }

            // AssignedEmployee bilgileri
            if (workOrder.AssignedEmployee != null)
            {
                response.AssignedEmployeeName = workOrder.AssignedEmployee.FullName;
            }

            // Items
            if (workOrder.Items != null)
            {
                response.Items = workOrder.Items.Select(i => new WorkOrderItemDto
                {
                    Id = i.Id,
                    ItemType = i.ItemType,
                    ItemTypeName = i.ItemType.ToString(),
                    PartId = i.PartId,
                    PartName = i.Part?.Name,
                    Description = i.Description,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalAmount = i.TotalAmount,
                    BrandType = i.BrandType
                }).ToList();
            }

            // Labors
            if (workOrder.Labors != null)
            {
                response.Labors = workOrder.Labors.Select(l => new WorkOrderLaborDto
                {
                    Id = l.Id,
                    EmployeeId = l.EmployeeId,
                    EmployeeName = l.Employee?.FullName ?? "",
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
                    OldStatus = t.OldStatus,
                    NewStatus = t.NewStatus,
                    StatusChangeText = t.OldStatus.HasValue && t.NewStatus.HasValue 
                        ? $"{t.OldStatus} → {t.NewStatus}" 
                        : t.Description,
                    EmployeeId = t.EmployeeId,
                    EmployeeName = t.Employee?.FullName,
                    Description = t.Description,
                    EventType = t.EventType
                }).ToList();
            }

            // Photos
            if (workOrder.Photos != null)
            {
                response.Photos = workOrder.Photos
                    .OrderBy(p => p.UploadDate)
                    .Select(MapWorkOrderPhoto)
                    .Where(p => !string.IsNullOrWhiteSpace(p.FilePath))
                    .ToList();
            }

            return response;
        }

        private static VehiclePhotoDto MapVehiclePhoto(Domain.Entities.VehiclePhoto photo)
        {
            return new VehiclePhotoDto
            {
                Id = photo.Id,
                FilePath = NormalizeFilePath(GetFilePath(photo.FilePath, photo.UploadedFile?.FilePath)),
                Description = photo.Description,
                PhotoType = photo.PhotoType,
                UploadDate = photo.UploadDate,
                DisplayOrder = photo.DisplayOrder
            };
        }

        private static WorkOrderPhotoDto MapWorkOrderPhoto(Domain.Entities.WorkOrderPhoto photo)
        {
            return new WorkOrderPhotoDto
            {
                Id = photo.Id,
                FilePath = NormalizeFilePath(GetFilePath(photo.FilePath, photo.UploadedFile?.FilePath)),
                Description = photo.Description,
                PhotoType = photo.PhotoType,
                PhotoTypeName = photo.PhotoType.ToString(),
                UploadDate = photo.UploadDate
            };
        }

        private static string? GetFilePath(string? primaryFilePath, string? fallbackFilePath)
        {
            return !string.IsNullOrWhiteSpace(primaryFilePath) ? primaryFilePath : fallbackFilePath;
        }

        private static string NormalizeFilePath(string? filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return string.Empty;
            }

            if (filePath.StartsWith("http://") || filePath.StartsWith("https://"))
            {
                return filePath;
            }

            return filePath.StartsWith("/") ? filePath : $"/{filePath}";
        }
    }
}

