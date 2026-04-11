using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetMobileDetail;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.UpdateMobileStatus
{
    public class UpdateMobileWorkOrderStatusCommandHandler : IRequestHandler<UpdateMobileWorkOrderStatusCommand, UpdateMobileWorkOrderStatusResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;
        private readonly IMediator _mediator;

        public UpdateMobileWorkOrderStatusCommandHandler(
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService,
            IMediator mediator)
        {
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
            _mediator = mediator;
        }

        public async Task<UpdateMobileWorkOrderStatusResponse> Handle(UpdateMobileWorkOrderStatusCommand request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.Id, out var workOrderId))
            {
                throw new DomainException("INVALID_WORK_ORDER_ID", new { Id = request.Id });
            }

            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            var workOrder = await _workOrderRepository.GetByIdAsync(workOrderId);
            if (workOrder == null)
            {
                throw new DomainException("WORKORDER_NOT_FOUND", new { Id = request.Id });
            }

            // Client kontrolü
            if (workOrder.ClientId != clientId)
            {
                throw new DomainException("WORKORDER_NOT_BELONG_TO_CLIENT", new { WorkOrderId = request.Id });
            }

            var oldStatus = workOrder.Status;

            // String status'u enum'a çevir ve durum geçişini belirle
            WorkOrderStatus targetStatus;
            
            if (request.Status.ToLower() == "pending")
            {
                // Pending durumuna geçiş yapılamaz (zaten pending kategorisindeyse hata)
                var isPending = IsPendingStatus(oldStatus);
                if (!isPending)
                {
                    throw new DomainException("INVALID_STATUS_TRANSITION", new 
                    { 
                        Message = "Pending durumuna geçiş yapılamaz. Sadece inProgress, completed veya cancelled durumlarına geçiş yapılabilir."
                    });
                }
                throw new DomainException("INVALID_STATUS_TRANSITION", new 
                { 
                    Message = "Zaten pending durumundasınız. inProgress veya cancelled durumuna geçiş yapabilirsiniz."
                });
            }
            else if (request.Status.ToLower() == "inprogress")
            {
                // Pending'den inProgress'e geçiş
                if (!IsPendingStatus(oldStatus))
                {
                    throw new DomainException("INVALID_STATUS_TRANSITION", new 
                    { 
                        Message = "Sadece pending durumundan inProgress durumuna geçiş yapılabilir."
                    });
                }
                targetStatus = WorkOrderStatus.InProgress;
            }
            else if (request.Status.ToLower() == "completed")
            {
                // inProgress'ten completed'e geçiş
                if (!IsInProgressStatus(oldStatus))
                {
                    throw new DomainException("INVALID_STATUS_TRANSITION", new 
                    { 
                        Message = "Sadece inProgress durumundan completed durumuna geçiş yapılabilir."
                    });
                }
                targetStatus = WorkOrderStatus.Delivered;
            }
            else if (request.Status.ToLower() == "cancelled")
            {
                // Pending veya inProgress'ten cancelled'e geçiş
                if (!IsPendingStatus(oldStatus) && !IsInProgressStatus(oldStatus))
                {
                    throw new DomainException("INVALID_STATUS_TRANSITION", new 
                    { 
                        Message = "Sadece pending veya inProgress durumundan cancelled durumuna geçiş yapılabilir."
                    });
                }
                targetStatus = WorkOrderStatus.Cancelled;
            }
            else
            {
                throw new DomainException("INVALID_STATUS", new { Status = request.Status });
            }

            // Durum değiştir
            workOrder.ChangeStatus(targetStatus, null, $"Durum {oldStatus} → {targetStatus} olarak güncellendi");

            _workOrderRepository.Update(workOrder);
            await _workOrderRepository.SaveChangesAsync();

            // Timeline kaydı oluştur
            var timeline = new Domain.Entities.WorkOrderTimeline
            {
                WorkOrderId = workOrder.Id,
                EventDate = DateTime.UtcNow,
                StatusChange = targetStatus,
                OldStatus = oldStatus,
                NewStatus = targetStatus,
                Description = $"Durum değiştirildi: {oldStatus} → {targetStatus}",
                EventType = "StatusChange"
            };

            await _workOrderRepository.AddTimelineAsync(timeline, cancellationToken);

            // Tam detay response'u getir
            var detailQuery = new GetMobileWorkOrderDetailQuery { Id = request.Id };
            var detailResponse = await _mediator.Send(detailQuery, cancellationToken);

            return new UpdateMobileWorkOrderStatusResponse
            {
                Data = detailResponse
            };
        }

        private WorkOrderStatus? MapStringToStatus(string status)
        {
            // String status'u enum'a çevir
            // Not: "pending" ve "inProgress" için mevcut duruma göre uygun enum seçilir
            return status.ToLower() switch
            {
                "pending" => null, // Mevcut duruma göre belirlenir
                "inprogress" => WorkOrderStatus.InProgress,
                "completed" => WorkOrderStatus.Delivered,
                "cancelled" => WorkOrderStatus.Cancelled,
                _ => null
            };
        }

        private bool IsPendingStatus(WorkOrderStatus status)
        {
            return status == WorkOrderStatus.AppointmentScheduled ||
                   status == WorkOrderStatus.VehicleEntered ||
                   status == WorkOrderStatus.WaitingForParts ||
                   status == WorkOrderStatus.ReadyForDelivery;
        }

        private bool IsInProgressStatus(WorkOrderStatus status)
        {
            return status == WorkOrderStatus.InProgress ||
                   status == WorkOrderStatus.InRepair ||
                   status == WorkOrderStatus.DiagnosisCompleted ||
                   status == WorkOrderStatus.QualityControl ||
                   status == WorkOrderStatus.Washing;
        }
    }
}
