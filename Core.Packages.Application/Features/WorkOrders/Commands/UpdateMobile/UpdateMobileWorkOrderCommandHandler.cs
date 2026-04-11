using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetMobileDetail;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.UpdateMobile
{
    public class UpdateMobileWorkOrderCommandHandler : IRequestHandler<UpdateMobileWorkOrderCommand, UpdateMobileWorkOrderResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IEntityRepository<WorkOrderItem, int> _workOrderItemRepository;
        private readonly IEntityRepository<WorkOrderLabor, int> _workOrderLaborRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<UserEntity> _userManager;

        public UpdateMobileWorkOrderCommandHandler(
            IWorkOrderRepository workOrderRepository,
            IEntityRepository<WorkOrderItem, int> workOrderItemRepository,
            IEntityRepository<WorkOrderLabor, int> workOrderLaborRepository,
            IEmployeeRepository employeeRepository,
            ITenantService tenantService,
            IMediator mediator,
            IHttpContextAccessor httpContextAccessor,
            UserManager<UserEntity> userManager)
        {
            _workOrderRepository = workOrderRepository;
            _workOrderItemRepository = workOrderItemRepository;
            _workOrderLaborRepository = workOrderLaborRepository;
            _employeeRepository = employeeRepository;
            _tenantService = tenantService;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<UpdateMobileWorkOrderResponse> Handle(UpdateMobileWorkOrderCommand request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.Id, out var workOrderId))
            {
                throw new DomainException("INVALID_WORK_ORDER_ID", new { Id = request.Id });
            }

            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // WorkOrder'ı bul
            var workOrder = await _workOrderRepository.Query()
                .Include(wo => wo.Items)
                .Include(wo => wo.Labors)
                .Include(wo => wo.AssignedEmployee)
                .FirstOrDefaultAsync(wo => wo.Id == workOrderId && wo.ClientId == clientId, cancellationToken);

            if (workOrder == null)
            {
                throw new DomainException("WORKORDER_NOT_FOUND", new { Id = request.Id });
            }

            // İş emri teslim edilmiş veya iptal edilmişse güncellenemez
            if (workOrder.Status == WorkOrderStatus.Delivered || workOrder.Status == WorkOrderStatus.Cancelled)
            {
                throw new DomainException("WORKORDER_CANNOT_BE_UPDATED", new { Status = workOrder.Status.ToString() });
            }

            // Validation
            if (string.IsNullOrWhiteSpace(request.ServiceTitle))
            {
                throw new DomainException("SERVICE_TITLE_REQUIRED", new { Message = "Hizmet başlığı boş olamaz." });
            }

            // Status string'den enum'a çevir
            var newStatus = MapStringToStatus(request.Status);
            if (newStatus == null)
            {
                throw new DomainException("INVALID_STATUS", new { Status = request.Status });
            }

            var oldStatus = workOrder.Status;
            var oldAssignedEmployeeId = workOrder.AssignedEmployeeId;

            // Technician kontrolü
            if (request.TechnicianId.HasValue)
            {
                var employee = await _employeeRepository.GetByIdAsync(request.TechnicianId.Value);
                if (employee == null || employee.ClientId != clientId)
                {
                    throw new DomainException("EMPLOYEE_NOT_FOUND", new { EmployeeId = request.TechnicianId.Value });
                }
            }

            // ServiceTitle'ı Items'ın ilk item'ına kaydet (veya özel bir alan varsa)
            // Not: WorkOrder entity'sinde serviceTitle diye bir alan yok, bu yüzden ilk item'ın description'ına kaydediyoruz
            // Veya SpecialRequests/CustomerComplaints alanlarını kullanabiliriz

            // Mevcut Items ve Labors'ı sil (replace semantiği)
            var existingItems = workOrder.Items.ToList();
            var existingLabors = workOrder.Labors.ToList();

            foreach (var item in existingItems)
            {
                // Eğer Part ise, stoka geri ekle
                if (item.ItemType == WorkOrderItemType.Part && item.PartId.HasValue)
                {
                    // Stock geri ekleme mantığı burada olabilir, şimdilik sadece sil
                }
                _workOrderItemRepository.Delete(item);
            }

            foreach (var labor in existingLabors)
            {
                _workOrderLaborRepository.Delete(labor);
            }

            // Yeni Items ekle
            foreach (var partDto in request.Parts)
            {
                var item = new WorkOrderItem
                {
                    WorkOrderId = workOrder.Id,
                    ItemType = WorkOrderItemType.Part,
                    Description = partDto.Name,
                    Quantity = partDto.Quantity,
                    UnitPrice = partDto.UnitPrice,
                    TaxRate = request.TaxRate,
                    ClientId = clientId,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = GetCurrentUserId(),
                    Status = Domain.Enums.Status.Active
                };

                // TotalAmount hesapla
                item.CalculateTotal();

                await _workOrderItemRepository.AddAsync(item, cancellationToken);
            }

            // Yeni Labors ekle
            foreach (var laborDto in request.Labor)
            {
                // EmployeeId zorunlu - TechnicianId veya mevcut assigned employee kullan
                var employeeId = request.TechnicianId ?? workOrder.AssignedEmployeeId;
                if (!employeeId.HasValue)
                {
                    throw new DomainException("TECHNICIAN_REQUIRED_FOR_LABOR", new { Message = "İş gücü eklemek için teknisyen atanmalıdır." });
                }

                var labor = new WorkOrderLabor
                {
                    WorkOrderId = workOrder.Id,
                    EmployeeId = employeeId.Value,
                    OperationName = laborDto.Description,
                    DurationHours = laborDto.Hours,
                    HourlyRate = laborDto.HourlyRate,
                    ClientId = clientId,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = GetCurrentUserId(),
                    Status = Domain.Enums.Status.Active
                };

                // TotalAmount hesapla
                labor.CalculateTotal();

                await _workOrderLaborRepository.AddAsync(labor, cancellationToken);
            }

            // WorkOrder alanlarını güncelle
            workOrder.Notes = request.TechnicianNotes;
            workOrder.AssignedEmployeeId = request.TechnicianId;
            workOrder.Status = newStatus.Value;

            // SpecialRequests'e serviceTitle'ı kaydet (geçici çözüm)
            workOrder.SpecialRequests = request.ServiceTitle;
            if (!string.IsNullOrEmpty(request.ServiceDescription))
            {
                workOrder.CustomerComplaints = request.ServiceDescription;
            }

            // TaxRate'i WorkOrder'a kaydet (Items'da zaten var ama genel taxRate için)
            // Not: WorkOrder entity'sinde TaxRate property'si yok, bu yüzden CalculateTotal kullanıyoruz

            // Toplam tutarı yeniden hesapla
            workOrder.CalculateTotal();

            // TaxAmount'ı hesapla (SubTotal üzerinden)
            var partsSubtotal = request.Parts.Sum(p => p.Quantity * p.UnitPrice);
            var laborSubtotal = request.Labor.Sum(l => l.Hours * l.HourlyRate);
            var subtotal = partsSubtotal + laborSubtotal;
            workOrder.TaxAmount = subtotal * (request.TaxRate / 100);
            workOrder.TotalAmount = subtotal + workOrder.TaxAmount;

            _workOrderRepository.Update(workOrder);
            await _workOrderRepository.SaveChangesAsync();

            // Güncelleyen kullanıcının çalışan kaydını bul (manager/owner için null olabilir)
            var currentUserId = GetCurrentUserId();
            int? currentUserEmployeeId = null;
            if (currentUserId > 0)
            {
                var currentEmployee = await _employeeRepository.Query()
                    .FirstOrDefaultAsync(e => e.UserId == currentUserId, cancellationToken);
                currentUserEmployeeId = currentEmployee?.Id;
            }

            // Timeline kaydı oluştur
            var timeline = new WorkOrderTimeline
            {
                WorkOrderId = workOrder.Id,
                EventDate = DateTime.UtcNow,
                EventType = "updated",
                Description = "İş emri güncellendi",
                EmployeeId = currentUserEmployeeId,
                ClientId = clientId,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = currentUserId,
                Status = Domain.Enums.Status.Active
            };

            await _workOrderRepository.AddTimelineAsync(timeline, cancellationToken);

            // Status değiştiyse timeline'a ekle
            if (oldStatus != newStatus.Value)
            {
                var statusTimeline = new WorkOrderTimeline
                {
                    WorkOrderId = workOrder.Id,
                    EventDate = DateTime.UtcNow,
                    EventType = "StatusChange",
                    OldStatus = oldStatus,
                    NewStatus = newStatus.Value,
                    Description = GetStatusChangeDescription(oldStatus, newStatus.Value),
                    EmployeeId = currentUserEmployeeId,
                    ClientId = clientId,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = currentUserId,
                    Status = Domain.Enums.Status.Active
                };
                await _workOrderRepository.AddTimelineAsync(statusTimeline, cancellationToken);
            }

            // Technician değiştiyse timeline'a ekle
            if (oldAssignedEmployeeId != request.TechnicianId)
            {
                var assignedEmployee = request.TechnicianId.HasValue
                    ? await _employeeRepository.GetByIdAsync(request.TechnicianId.Value)
                    : null;

                var assignedTimeline = new WorkOrderTimeline
                {
                    WorkOrderId = workOrder.Id,
                    EventDate = DateTime.UtcNow,
                    EventType = "assigned",
                    Description = assignedEmployee != null
                        ? $"{assignedEmployee.FullName}'e atandı"
                        : "Teknisyen ataması kaldırıldı",
                    EmployeeId = request.TechnicianId,
                    ClientId = clientId,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = currentUserId,
                    Status = Domain.Enums.Status.Active
                };
                await _workOrderRepository.AddTimelineAsync(assignedTimeline, cancellationToken);
            }

            // Tam detay response'u getir
            var detailQuery = new GetMobileWorkOrderDetailQuery { Id = request.Id };
            var detailResponse = await _mediator.Send(detailQuery, cancellationToken);

            return new UpdateMobileWorkOrderResponse
            {
                Data = detailResponse
            };
        }

        private WorkOrderStatus? MapStringToStatus(string status)
        {
            return status.ToLower() switch
            {
                "pending" => WorkOrderStatus.VehicleEntered, // Varsayılan pending durumu
                "inprogress" => WorkOrderStatus.InProgress,
                "completed" => WorkOrderStatus.Delivered,
                "cancelled" => WorkOrderStatus.Cancelled,
                _ => null
            };
        }

        private string GetStatusChangeDescription(WorkOrderStatus oldStatus, WorkOrderStatus newStatus)
        {
            return newStatus switch
            {
                WorkOrderStatus.InProgress => "Çalışma başladı",
                WorkOrderStatus.Delivered => "İş tamamlandı",
                WorkOrderStatus.Cancelled => "İş emri iptal edildi",
                _ => $"Durum değiştirildi: {oldStatus} → {newStatus}"
            };
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out var userId))
            {
                return userId;
            }
            return 0; // System user
        }

        private async Task<Domain.Entities.User?> GetCurrentUserAsync()
        {
            var userId = GetCurrentUserId();
            if (userId == 0)
            {
                return null;
            }
            return await _userManager.FindByIdAsync(userId.ToString());
        }
    }
}
