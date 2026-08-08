using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddMobilePart;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.DeleteMobilePart
{
    public class DeleteMobileWorkOrderPartCommandHandler : IRequestHandler<DeleteMobileWorkOrderPartCommand, DeleteMobileWorkOrderPartResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IEntityRepository<WorkOrderItem, int> _workOrderItemRepository;
        private readonly IPartStockRepository _partStockRepository;
        private readonly ITenantService _tenantService;

        public DeleteMobileWorkOrderPartCommandHandler(
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

        public async Task<DeleteMobileWorkOrderPartResponse> Handle(DeleteMobileWorkOrderPartCommand request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.WorkOrderId, out var workOrderId))
            {
                throw new DomainException("INVALID_WORK_ORDER_ID", new { Id = request.WorkOrderId });
            }

            if (!int.TryParse(request.PartId.Replace("p", ""), out var partId))
            {
                throw new DomainException("INVALID_PART_ID", new { Id = request.PartId });
            }

            var clientId = _tenantService.GetRequiredClientId();

            // WorkOrder kontrolü
            var workOrder = await _workOrderRepository.Query()
                .Include(wo => wo.Items)
                .Include(wo => wo.Labors)
                .FirstOrDefaultAsync(wo => wo.Id == workOrderId && wo.ClientId == clientId, cancellationToken);

            if (workOrder == null)
            {
                throw new DomainException("WORKORDER_NOT_FOUND", new { Id = request.WorkOrderId });
            }

            // İş emri tamamlanmış veya teslim edilmişse parça silinemez
            if (workOrder.Status == WorkOrderStatus.Delivered || workOrder.Status == WorkOrderStatus.Cancelled)
            {
                throw new DomainException("WORKORDER_CANNOT_DELETE_PART", new { Status = workOrder.Status.ToString() });
            }

            // Item'ı bul
            var item = await _workOrderItemRepository.GetByIdAsync(partId);
            if (item == null || item.WorkOrderId != workOrderId || item.ClientId != clientId || item.ItemType != WorkOrderItemType.Part)
            {
                throw new DomainException("PART_NOT_FOUND", new { PartId = request.PartId });
            }

            // Eğer Part ise, stoka geri ekle
            if (item.PartId.HasValue)
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

            // Güncellenmiş maliyet özetini hesapla
            var partsSubtotal = workOrder.Items
                .Where(i => i.ItemType == WorkOrderItemType.Part)
                .Sum(i => i.TotalAmount);
            var laborSubtotal = workOrder.Labors.Sum(l => l.TotalAmount);
            var subtotal = partsSubtotal + laborSubtotal;
            var taxRate = workOrder.TaxAmount > 0 && workOrder.SubTotal > 0
                ? (workOrder.TaxAmount / workOrder.SubTotal) * 100
                : 20;
            var taxAmount = subtotal * (taxRate / 100);
            var total = subtotal + taxAmount;

            return new DeleteMobileWorkOrderPartResponse
            {
                UpdatedCosts = new UpdatedCostsDto
                {
                    PartsSubtotal = partsSubtotal,
                    LaborSubtotal = laborSubtotal,
                    TaxAmount = taxAmount,
                    Total = total
                }
            };
        }
    }
}
