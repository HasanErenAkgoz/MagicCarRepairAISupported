using MagicCarRepairAISupported.Application.Common.Messages;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Accounting.Income.Commands.Create;
using MagicCarRepairAISupported.Application.Features.Loyalty.Commands.EarnPoints;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.UpdateStatus;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.Deliver
{
    public class DeliverWorkOrderCommandHandler : IRequestHandler<DeliverWorkOrderCommand, DeliverWorkOrderResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IMediator _mediator;
        private readonly ITenantService _tenantService;
        private readonly ILogger<DeliverWorkOrderCommandHandler> _logger;

        public DeliverWorkOrderCommandHandler(
            IWorkOrderRepository workOrderRepository,
            IMediator mediator,
            ITenantService tenantService,
            ILogger<DeliverWorkOrderCommandHandler> logger)
        {
            _workOrderRepository = workOrderRepository;
            _mediator = mediator;
            _tenantService = tenantService;
            _logger = logger;
        }

        public async Task<DeliverWorkOrderResponse> Handle(DeliverWorkOrderCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

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
                throw new DomainException("WORKORDER_NOT_READY_FOR_DELIVERY", new { Status = workOrder.Status });
            }

            // Müşteri onayı kontrolü
            if (request.RequireCustomerApproval || workOrder.CustomerApprovalStatus == Domain.Enums.CustomerApprovalStatus.Pending)
            {
                if (workOrder.CustomerApprovalStatus != Domain.Enums.CustomerApprovalStatus.Approved)
                {
                    throw new DomainException("WORKORDER_CUSTOMER_APPROVAL_REQUIRED", new { 
                        Status = workOrder.CustomerApprovalStatus,
                        WorkOrderId = request.WorkOrderId 
                    });
                }
            }

            // Ödeme tutarını güncelle
            if (request.PaidAmount.HasValue)
            {
                workOrder.PaidAmount = request.PaidAmount.Value;
                workOrder.UpdatePaymentStatus();
            }

            // Notes güncelle
            if (!string.IsNullOrEmpty(request.Notes))
            {
                workOrder.Notes = request.Notes;
            }

            // Durumu "Teslim Edildi" olarak güncelle
            var updateStatusCommand = new UpdateWorkOrderStatusCommand
            {
                WorkOrderId = request.WorkOrderId,
                NewStatus = WorkOrderStatus.Delivered,
                Description = request.Notes ?? "İş emri teslim edildi.",
                EmployeeId = request.EmployeeId
            };

            await _mediator.Send(updateStatusCommand, cancellationToken);

            // Müşteri onayını temizle (teslim edildi)
            if (workOrder.CustomerApprovalStatus.HasValue)
            {
                workOrder.CustomerApprovalStatus = null;
            }

            // WorkOrder'ı güncelle
            _workOrderRepository.Update(workOrder);
            await _workOrderRepository.SaveChangesAsync();

            // Otomatik gelir kaydı oluştur (eğer ödeme yapıldıysa)
            if (request.PaidAmount.HasValue && request.PaidAmount.Value > 0)
            {
                var createIncomeCommand = new CreateIncomeCommand
                {
                    WorkOrderId = workOrder.Id,
                    IncomeType = IncomeType.WorkOrder,
                    Amount = request.PaidAmount.Value,
                    PaymentMethod = request.PaymentMethod ?? PaymentMethod.Cash,
                    TransactionDate = DateTime.UtcNow,
                    Description = $"İş Emri #{workOrder.WorkOrderNumber} geliri",
                    CustomerId = workOrder.CustomerId
                };

                await _mediator.Send(createIncomeCommand, cancellationToken);
            }

            // Sadakat puanı kazandır (teslim edilen iş emri için)
            if (workOrder.CustomerId > 0)
            {
                try
                {
                    var earnCommand = new EarnPointsCommand
                    {
                        CustomerId = workOrder.CustomerId,
                        Points = Math.Max(1, (int)(workOrder.TotalAmount / 10)),
                        Description = $"İş emri #{workOrder.WorkOrderNumber} tamamlandı",
                        WorkOrderId = workOrder.Id,
                        ExpiryDate = DateTime.UtcNow.AddYears(1)
                    };
                    await _mediator.Send(earnCommand, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Loyalty points could not be earned for work order {WorkOrderId}", workOrder.Id);
                }
            }

            return new DeliverWorkOrderResponse
            {
                WorkOrderId = workOrder.Id,
                Status = WorkOrderStatus.Delivered,
                TotalAmount = workOrder.TotalAmount,
                PaidAmount = workOrder.PaidAmount,
                PaymentStatus = workOrder.PaymentStatus,
                DeliveredDate = DateTime.UtcNow
            };
        }
    }
}

