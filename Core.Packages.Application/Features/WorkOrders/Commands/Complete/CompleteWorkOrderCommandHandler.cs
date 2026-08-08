using MagicCarRepairAISupported.Application.Common.Messages;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Accounting.Income.Commands.Create;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.UpdateStatus;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.Complete
{
    public class CompleteWorkOrderCommandHandler : IRequestHandler<CompleteWorkOrderCommand, CompleteWorkOrderResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IMediator _mediator;
        private readonly ITenantService _tenantService;

        public CompleteWorkOrderCommandHandler(
            IWorkOrderRepository workOrderRepository,
            IMediator mediator,
            ITenantService tenantService)
        {
            _workOrderRepository = workOrderRepository;
            _mediator = mediator;
            _tenantService = tenantService;
        }

        public async Task<CompleteWorkOrderResponse> Handle(CompleteWorkOrderCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId);
            if (workOrder == null)
            {
                throw new DomainException(Messages.NotFound, new { Entity = "WorkOrder", Id = request.WorkOrderId });
            }

            if (workOrder.ClientId != clientId)
            {
                throw new DomainException("WORKORDER_NOT_BELONG_TO_CLIENT", new { WorkOrderId = request.WorkOrderId });
            }

            // İş emri zaten tamamlanmış veya teslim edilmişse
            if (workOrder.Status == WorkOrderStatus.ReadyForDelivery || 
                workOrder.Status == WorkOrderStatus.Delivered ||
                workOrder.Status == WorkOrderStatus.Cancelled)
            {
                throw new DomainException("WORKORDER_ALREADY_COMPLETED", new { Status = workOrder.Status });
            }

            // Toplam tutarı hesapla
            workOrder.CalculateTotal();

            // Durumu "Teslime Hazır" olarak güncelle
            var updateStatusCommand = new UpdateWorkOrderStatusCommand
            {
                WorkOrderId = request.WorkOrderId,
                NewStatus = WorkOrderStatus.ReadyForDelivery,
                Description = request.Notes ?? "İş emri tamamlandı ve teslime hazır hale getirildi.",
                EmployeeId = request.EmployeeId
            };

            var statusResponse = await _mediator.Send(updateStatusCommand, cancellationToken);

            // Notes güncelle
            if (!string.IsNullOrEmpty(request.Notes))
            {
                workOrder.Notes = request.Notes;
                _workOrderRepository.Update(workOrder);
                await _workOrderRepository.SaveChangesAsync();
            }

            // Business Rule: İş emri tamamlandığında otomatik gelir kaydı oluştur
            // Not: Eğer ödeme yapılmışsa DeliverWorkOrder'da zaten gelir kaydı oluşturulur
            // Burada sadece tamamlanma kaydı yapılır, gelir kaydı Deliver'da yapılır
            // Ancak eğer önceden ödeme yapıldıysa burada da kayıt oluşturulabilir
            // Şimdilik Deliver'da yapıldığı için burada yapmıyoruz

            return new CompleteWorkOrderResponse
            {
                WorkOrderId = workOrder.Id,
                Status = WorkOrderStatus.ReadyForDelivery,
                TotalAmount = workOrder.TotalAmount,
                CompletedDate = DateTime.UtcNow
            };
        }
    }
}

