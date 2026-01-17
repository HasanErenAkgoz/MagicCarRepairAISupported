using MagicCarRepairAISupported.Application.Common.Messages;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.RequestCustomerApproval
{
    public class RequestCustomerApprovalCommandHandler : IRequestHandler<RequestCustomerApprovalCommand, RequestCustomerApprovalResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;
        private readonly ISignalRNotificationService _signalRNotificationService;

        public RequestCustomerApprovalCommandHandler(
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService,
            ISignalRNotificationService signalRNotificationService)
        {
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
            _signalRNotificationService = signalRNotificationService;
        }

        public async Task<RequestCustomerApprovalResponse> Handle(RequestCustomerApprovalCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId);
            if (workOrder == null)
            {
                throw new DomainException(Messages.NotFound, new { Entity = "WorkOrder", Id = request.WorkOrderId });
            }

            if (workOrder.ClientId != clientId)
            {
                throw new DomainException("WORKORDER_NOT_BELONG_TO_CLIENT", new { WorkOrderId = request.WorkOrderId });
            }

            // İş emri teslime hazır değilse
            if (workOrder.Status != WorkOrderStatus.ReadyForDelivery)
            {
                throw new DomainException("WORKORDER_NOT_READY_FOR_APPROVAL", new { Status = workOrder.Status });
            }

            // Müşteri onayı bekleme durumuna geç
            workOrder.CustomerApprovalStatus = Domain.Enums.CustomerApprovalStatus.Pending;
            workOrder.CustomerApprovalDate = null;
            workOrder.CustomerRejectionReason = null;

            _workOrderRepository.Update(workOrder);
            await _workOrderRepository.SaveChangesAsync();

            // Timeline kaydı oluştur
            var timeline = new WorkOrderTimeline
            {
                WorkOrderId = workOrder.Id,
                EventDate = DateTime.UtcNow,
                StatusChange = workOrder.Status,
                Description = request.Message ?? "Müşteri onayı beklendi.",
                EventType = "CustomerApprovalRequest"
            };

            await _workOrderRepository.AddTimelineAsync(timeline, cancellationToken);

            // Müşteriye bildirim gönder
            await _signalRNotificationService.SendNotificationToUserAsync(
                workOrder.CustomerId,
                "İş Emri Onayı Bekliyor",
                $"İş emri #{workOrder.WorkOrderNumber} için onayınız bekleniyor.",
                "WorkOrder",
                workOrder.Id);

            // Real-time WorkOrder güncellemesi
            await _signalRNotificationService.SendWorkOrderUpdateAsync(
                workOrder.Id,
                workOrder.Status.ToString(),
                "Müşteri onayı beklendi.",
                workOrder.CustomerId);

            return new RequestCustomerApprovalResponse
            {
                WorkOrderId = workOrder.Id,
                Success = true,
                Message = "Müşteri onayı talebi başarıyla oluşturuldu."
            };
        }
    }
}
