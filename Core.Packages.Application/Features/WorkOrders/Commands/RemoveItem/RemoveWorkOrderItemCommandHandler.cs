using MagicCarRepairAISupported.Application.Common.Messages;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.RemoveItem
{
    public class RemoveWorkOrderItemCommandHandler : IRequestHandler<RemoveWorkOrderItemCommand, RemoveWorkOrderItemResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IEntityRepository<WorkOrderItem, int> _workOrderItemRepository;
        private readonly IPartStockRepository _partStockRepository;
        private readonly ITenantService _tenantService;

        public RemoveWorkOrderItemCommandHandler(
            IWorkOrderRepository workOrderRepository,
            IEntityRepository<WorkOrderItem, int> workOrderItemRepository,
            IPartStockRepository partStockRepository,
            ITenantService tenantService)
        {
            _workOrderRepository = workOrderRepository;
            _workOrderItemRepository = workOrderItemRepository;
            _partStockRepository = partStockRepository;
            _tenantService = tenantService;
        }

        public async Task<RemoveWorkOrderItemResponse> Handle(RemoveWorkOrderItemCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            // WorkOrder kontrolü
            var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId);
            if (workOrder == null)
            {
                throw new DomainException(Messages.NotFound, new { Entity = "WorkOrder", Id = request.WorkOrderId });
            }

            if (workOrder.ClientId != clientId)
            {
                throw new DomainException("WORKORDER_NOT_BELONG_TO_CLIENT", new { WorkOrderId = request.WorkOrderId });
            }

            // İş emri tamamlanmış veya teslim edilmişse item silinemez
            if (workOrder.Status == WorkOrderStatus.Delivered || workOrder.Status == WorkOrderStatus.Cancelled)
            {
                throw new DomainException("WORKORDER_CANNOT_REMOVE_ITEM", new { Status = workOrder.Status });
            }

            // Item'ı bul
            var item = await _workOrderItemRepository.GetByIdAsync(request.ItemId);
            if (item == null || item.WorkOrderId != request.WorkOrderId || item.ClientId != clientId)
            {
                throw new DomainException(Messages.NotFound, new { Entity = "WorkOrderItem", Id = request.ItemId });
            }

            // Eğer Part ise, stoka geri ekle
            if (item.ItemType == WorkOrderItemType.Part && item.PartId.HasValue)
            {
                var stock = await _partStockRepository.GetByPartIdAsync(item.PartId.Value, cancellationToken);
                if (stock != null)
                {
                    stock.AddQuantity((int)item.Quantity);
                    _partStockRepository.Update(stock);
                }
            }

            // Item'ı sil
            _workOrderItemRepository.Delete(item);
            await _workOrderItemRepository.SaveChangesAsync();

            // WorkOrder toplamını güncelle
            workOrder.CalculateTotal();
            _workOrderRepository.Update(workOrder);
            await _workOrderRepository.SaveChangesAsync();

            return new RemoveWorkOrderItemResponse
            {
                WorkOrderId = workOrder.Id,
                ItemId = item.Id,
                WorkOrderTotalAmount = workOrder.TotalAmount,
                Message = "Item başarıyla kaldırıldı."
            };
        }
    }
}

