using MagicCarRepairAISupported.Application.Common.Messages;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.RejectByCustomer
{
    public class RejectWorkOrderByCustomerCommandHandler : IRequestHandler<RejectWorkOrderByCustomerCommand, RejectWorkOrderByCustomerResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;
        private readonly ISignalRNotificationService _signalRNotificationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RejectWorkOrderByCustomerCommandHandler(
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService,
            ISignalRNotificationService signalRNotificationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
            _signalRNotificationService = signalRNotificationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<RejectWorkOrderByCustomerResponse> Handle(RejectWorkOrderByCustomerCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            // Customer ID'yi HttpContext'ten al (Customer portal'dan geliyorsa)
            var customerIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("CustomerId")?.Value;
            int? customerId = null;
            if (!string.IsNullOrEmpty(customerIdClaim) && int.TryParse(customerIdClaim, out var parsedCustomerId))
            {
                customerId = parsedCustomerId;
            }

            var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId);
            if (workOrder == null)
            {
                throw new DomainException(Messages.NotFound, new { Entity = "WorkOrder", Id = request.WorkOrderId });
            }

            if (workOrder.ClientId != clientId)
            {
                throw new DomainException("WORKORDER_NOT_BELONG_TO_CLIENT", new { WorkOrderId = request.WorkOrderId });
            }

            // Müşteri kontrolü (eğer customer ID varsa)
            if (customerId.HasValue && workOrder.CustomerId != customerId.Value)
            {
                throw new DomainException("WORKORDER_NOT_BELONG_TO_CUSTOMER", new { WorkOrderId = request.WorkOrderId });
            }

            // Onay durumu kontrolü
            if (workOrder.CustomerApprovalStatus != Domain.Enums.CustomerApprovalStatus.Pending)
            {
                throw new DomainException("WORKORDER_APPROVAL_NOT_PENDING", new { Status = workOrder.CustomerApprovalStatus });
            }

            // Müşteri reddini kaydet
            workOrder.CustomerApprovalStatus = Domain.Enums.CustomerApprovalStatus.Rejected;
            workOrder.CustomerApprovalDate = DateTime.UtcNow;
            workOrder.CustomerRejectionReason = request.RejectionReason;

            _workOrderRepository.Update(workOrder);
            await _workOrderRepository.SaveChangesAsync();

            // Timeline kaydı oluştur
            var timeline = new WorkOrderTimeline
            {
                WorkOrderId = workOrder.Id,
                EventDate = DateTime.UtcNow,
                StatusChange = workOrder.Status,
                Description = $"Müşteri onayı reddedildi. Nedeni: {request.RejectionReason}",
                EventType = "CustomerRejection"
            };

            await _workOrderRepository.AddTimelineAsync(timeline, cancellationToken);

            // Real-time bildirim gönder
            await _signalRNotificationService.SendNotificationToClientAsync(
                clientId,
                "Müşteri Reddi",
                $"İş emri #{workOrder.WorkOrderNumber} müşteri tarafından reddedildi. Nedeni: {request.RejectionReason}",
                "WorkOrder",
                workOrder.Id);

            await _signalRNotificationService.SendWorkOrderUpdateAsync(
                workOrder.Id,
                workOrder.Status.ToString(),
                "Müşteri onayı reddedildi.",
                null);

            return new RejectWorkOrderByCustomerResponse
            {
                WorkOrderId = workOrder.Id,
                Success = true,
                RejectionDate = workOrder.CustomerApprovalDate.Value
            };
        }
    }
}
