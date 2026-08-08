using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetMobileDetail
{
    public class GetMobileWorkOrderDetailQueryHandler : IRequestHandler<GetMobileWorkOrderDetailQuery, GetMobileWorkOrderDetailResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IEmployeeRepository _employeeRepository;

        public GetMobileWorkOrderDetailQueryHandler(
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService,
            UserManager<UserEntity> userManager,
            IEmployeeRepository employeeRepository)
        {
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
            _userManager = userManager;
            _employeeRepository = employeeRepository;
        }

        public async Task<GetMobileWorkOrderDetailResponse> Handle(GetMobileWorkOrderDetailQuery request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.Id, out var workOrderId))
            {
                throw new DomainException("INVALID_WORK_ORDER_ID", new { Id = request.Id });
            }

            var clientId = _tenantService.GetRequiredClientId();

            // AsSplitQuery: büyük Include zincirinde kartezyen çarpım önlenir
            // AsNoTracking: read-only sorgu, change tracking overhead'i yok
            var workOrder = await _workOrderRepository.Query()
                .AsNoTracking()
                .Include(wo => wo.Vehicle)
                    .ThenInclude(v => v.Photos)
                        .ThenInclude(p => p.UploadedFile)
                .Include(wo => wo.Photos)
                .Include(wo => wo.Customer)
                .Include(wo => wo.AssignedEmployee)
                .Include(wo => wo.Items)
                    .ThenInclude(i => i.Part)
                .Include(wo => wo.Labors)
                    .ThenInclude(l => l.Employee)
                .Include(wo => wo.Timeline)
                    .ThenInclude(t => t.Employee)
                .AsSplitQuery()
                .FirstOrDefaultAsync(wo => wo.Id == workOrderId && wo.ClientId == clientId, cancellationToken);

            if (workOrder == null)
            {
                throw new DomainException("WORKORDER_NOT_FOUND", new { Id = request.Id });
            }

            // Parts (ItemType = Part olanlar)
            var parts = workOrder.Items?
                .Where(i => i.ItemType == Domain.Enums.WorkOrderItemType.Part)
                .Select(i => new PartDto
                {
                    Id = $"p{i.Id}",
                    Name = i.Part?.Name ?? i.Description,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Total = i.TotalAmount
                })
                .ToList() ?? new List<PartDto>();

            // Labor
            var labors = workOrder.Labors?
                .Select(l => new LaborDto
                {
                    Id = $"l{l.Id}",
                    Description = l.OperationName,
                    Hours = l.DurationHours ?? 0,
                    HourlyRate = l.HourlyRate,
                    Total = l.TotalAmount
                })
                .ToList() ?? new List<LaborDto>();

            // Timeline: Employee'si olmayan kayıtlar için CreatedBy user adlarını
            // TEK bir toplu sorguyla önceden yükle (N+1 önlemi)
            var missingUserIds = workOrder.Timeline?
                .Where(t => t.Employee == null && t.CreatedBy > 0)
                .Select(t => t.CreatedBy)
                .Distinct()
                .ToList() ?? new List<int>();

            Dictionary<int, string> userNameMap = new();
            if (missingUserIds.Count > 0)
            {
                var users = await _userManager.Users
                    .Where(u => missingUserIds.Contains(u.Id))
                    .Select(u => new { u.Id, u.FirstName, u.LastName, u.UserName })
                    .ToListAsync(cancellationToken);

                foreach (var u in users)
                {
                    var fullName = $"{u.FirstName} {u.LastName}".Trim();
                    userNameMap[u.Id] = !string.IsNullOrEmpty(fullName) ? fullName : (u.UserName ?? "Sistem");
                }
            }

            // Timeline
            var timeline = workOrder.Timeline?
                .OrderBy(t => t.EventDate)
                .Select(t => new TimelineItemDto
                {
                    Id = $"t{t.Id}",
                    Status = MapTimelineStatus(t),
                    Note = t.Description ?? "",
                    CreatedAt = t.EventDate.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    CreatedBy = GetCreatedByName(t, userNameMap)
                })
                .ToList() ?? new List<TimelineItemDto>();

            // Work order photos
            var workOrderPhotos = workOrder.Photos?
                .OrderBy(p => p.UploadDate)
                .Select(p => new WorkOrderPhotoDto
                {
                    Id = p.Id,
                    Url = GetWorkOrderPhotoUrl(p),
                    MediaUrl = $"/api/media/work-orders/{workOrder.Id}/photos/{p.Id}",
                    PhotoType = (int)p.PhotoType,
                    PhotoTypeName = p.PhotoType.ToString(),
                    Description = p.Description,
                    UploadedAt = p.UploadDate.ToString("yyyy-MM-ddTHH:mm:ssZ")
                })
                .Where(p => !string.IsNullOrWhiteSpace(p.Url))
                .ToList() ?? new List<WorkOrderPhotoDto>();

            // Vehicle photos
            var vehiclePhotos = workOrder.Vehicle?.Photos?
                .OrderBy(p => p.DisplayOrder)
                .ThenBy(p => p.UploadDate)
                .Select(GetPhotoUrl)
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .ToList() ?? new List<string>();

            // Service title ve description
            var serviceTitle = GetServiceTitle(workOrder);
            var serviceDescription = GetServiceDescription(workOrder);

            var response = new GetMobileWorkOrderDetailResponse
            {
                Id = workOrder.Id.ToString(),
                OrderNo = workOrder.WorkOrderNumber,
                Status = MapStatusEnumToString(workOrder.Status),
                CreatedAt = workOrder.CreatedDate?.ToString("yyyy-MM-ddTHH:mm:ssZ") ?? workOrder.EntryDate.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                UpdatedAt = workOrder.ModifiedDate?.ToString("yyyy-MM-ddTHH:mm:ssZ") ?? workOrder.EntryDate.ToString("yyyy-MM-ddTHH:mm:ssZ"),

                Customer = workOrder.Customer != null ? new CustomerDto
                {
                    Id = workOrder.Customer.Id,
                    Name = workOrder.Customer.FullName,
                    Phone = workOrder.Customer.PhoneNumber,
                    Email = workOrder.Customer.Email
                } : null,

                Vehicle = workOrder.Vehicle != null ? new VehicleDto
                {
                    Id = workOrder.Vehicle.Id,
                    Brand = workOrder.Vehicle.Brand,
                    Model = workOrder.Vehicle.Model,
                    Year = workOrder.Vehicle.Year,
                    Plate = workOrder.Vehicle.LicensePlate,
                    Vin = workOrder.Vehicle.Vin,
                    FuelType = workOrder.Vehicle?.FuelType ?? string.Empty,
                    Trim = workOrder.Vehicle.Trim ?? "",
                    Photos = vehiclePhotos
                } : null,

                ServiceTitle = serviceTitle,
                ServiceDescription = serviceDescription,
                TechnicianNotes = workOrder.Notes,

                Technician = workOrder.AssignedEmployee != null ? new TechnicianDto
                {
                    Id = workOrder.AssignedEmployee.Id,
                    Name = workOrder.AssignedEmployee.FullName
                } : null,

                Parts = parts,
                Labor = labors,

                PartsSubtotal = parts.Sum(p => p.Total),
                LaborSubtotal = labors.Sum(l => l.Total),
                TaxRate = workOrder.TaxAmount > 0 && workOrder.SubTotal > 0 
                    ? Math.Round((workOrder.TaxAmount / workOrder.SubTotal) * 100, 2)
                    : 20, // Varsayılan %20
                TaxAmount = workOrder.TaxAmount,
                Total = workOrder.TotalAmount,

                Timeline = timeline,
                Photos = workOrderPhotos
            };

            return response;
        }

        private string MapStatusEnumToString(Domain.Enums.WorkOrderStatus status)
        {
            return status switch
            {
                Domain.Enums.WorkOrderStatus.AppointmentScheduled or
                Domain.Enums.WorkOrderStatus.VehicleEntered or
                Domain.Enums.WorkOrderStatus.WaitingForParts or
                Domain.Enums.WorkOrderStatus.ReadyForDelivery => "pending",
                
                Domain.Enums.WorkOrderStatus.InProgress or
                Domain.Enums.WorkOrderStatus.InRepair or
                Domain.Enums.WorkOrderStatus.DiagnosisCompleted or
                Domain.Enums.WorkOrderStatus.QualityControl or
                Domain.Enums.WorkOrderStatus.Washing => "inProgress",
                
                Domain.Enums.WorkOrderStatus.Delivered => "completed",
                
                Domain.Enums.WorkOrderStatus.Cancelled => "cancelled",
                
                _ => "pending"
            };
        }

        private string MapTimelineStatus(Domain.Entities.WorkOrderTimeline timeline)
        {
            // EventType'a göre mapping
            if (!string.IsNullOrEmpty(timeline.EventType))
            {
                return timeline.EventType.ToLower() switch
                {
                    "created" => "created",
                    "assigned" => "assigned",
                    "statuschange" => timeline.NewStatus.HasValue
                        ? MapTimelineStatusFromWorkOrderStatus(timeline.NewStatus.Value)
                        : "inProgress",
                    _ => "inProgress"
                };
            }

            // Status değişikliğine göre
            if (timeline.NewStatus.HasValue)
            {
                return MapStatusEnumToString(timeline.NewStatus.Value);
            }

            return "inProgress";
        }

        private string MapTimelineStatusFromWorkOrderStatus(Domain.Enums.WorkOrderStatus status)
        {
            return status switch
            {
                Domain.Enums.WorkOrderStatus.Delivered => "completed",
                Domain.Enums.WorkOrderStatus.Cancelled => "cancelled",
                _ => "inProgress"
            };
        }

        private string GetServiceTitle(Domain.Entities.WorkOrder workOrder)
        {
            if (workOrder.Items != null && workOrder.Items.Any())
            {
                var firstItem = workOrder.Items.FirstOrDefault(i => !string.IsNullOrEmpty(i.Description));
                if (firstItem != null)
                {
                    return firstItem.Description;
                }
            }

            if (!string.IsNullOrEmpty(workOrder.SpecialRequests))
            {
                return "Özel İstek";
            }

            if (!string.IsNullOrEmpty(workOrder.CustomerComplaints))
            {
                return "Müşteri Şikayeti";
            }

            return "Genel Bakım";
        }

        private string GetServiceDescription(Domain.Entities.WorkOrder workOrder)
        {
            var descriptions = new List<string>();

            if (!string.IsNullOrEmpty(workOrder.CustomerComplaints))
            {
                descriptions.Add($"Müşteri Şikayeti: {workOrder.CustomerComplaints}");
            }

            if (!string.IsNullOrEmpty(workOrder.SpecialRequests))
            {
                descriptions.Add($"Özel İstekler: {workOrder.SpecialRequests}");
            }

            if (workOrder.Items != null && workOrder.Items.Any())
            {
                var itemDescriptions = workOrder.Items
                    .Where(i => !string.IsNullOrEmpty(i.Description))
                    .Select(i => i.Description)
                    .Distinct();
                descriptions.AddRange(itemDescriptions);
            }

            if (descriptions.Any())
            {
                return string.Join(" — ", descriptions);
            }

            return "Standart bakım ve onarım işlemleri.";
        }

        private string GetWorkOrderPhotoUrl(Domain.Entities.WorkOrderPhoto photo)
        {
            var filePath = !string.IsNullOrWhiteSpace(photo.FilePath)
                ? photo.FilePath
                : photo.UploadedFile?.FilePath;

            if (string.IsNullOrEmpty(filePath)) return string.Empty;
            if (filePath.StartsWith("http://") || filePath.StartsWith("https://")) return filePath;
            return filePath.StartsWith("/") ? filePath : $"/{filePath}";
        }

        private string GetPhotoUrl(Domain.Entities.VehiclePhoto photo)
        {
            var filePath = !string.IsNullOrWhiteSpace(photo.FilePath)
                ? photo.FilePath
                : photo.UploadedFile?.FilePath;

            if (string.IsNullOrEmpty(filePath))
            {
                return string.Empty;
            }

            // Eğer zaten absolute URL ise
            if (filePath.StartsWith("http://") || filePath.StartsWith("https://"))
            {
                return filePath;
            }

            // Relative path ise / ile başlamalı
            if (!filePath.StartsWith("/"))
            {
                return $"/{filePath}";
            }

            return filePath;
        }

        private static string GetCreatedByName(
            Domain.Entities.WorkOrderTimeline timeline,
            Dictionary<int, string> userNameMap)
        {
            if (timeline.Employee != null)
                return timeline.Employee.FullName;

            if (timeline.CreatedBy > 0 && userNameMap.TryGetValue(timeline.CreatedBy, out var name))
                return name;

            return "Sistem";
        }
    }
}
