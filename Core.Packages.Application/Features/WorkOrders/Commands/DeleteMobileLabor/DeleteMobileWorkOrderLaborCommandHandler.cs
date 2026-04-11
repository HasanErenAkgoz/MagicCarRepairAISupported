using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddMobilePart;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.DeleteMobileLabor
{
    public class DeleteMobileWorkOrderLaborCommandHandler : IRequestHandler<DeleteMobileWorkOrderLaborCommand, DeleteMobileWorkOrderLaborResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IEntityRepository<WorkOrderLabor, int> _workOrderLaborRepository;
        private readonly ITenantService _tenantService;

        public DeleteMobileWorkOrderLaborCommandHandler(
            IWorkOrderRepository workOrderRepository,
            IEntityRepository<WorkOrderLabor, int> workOrderLaborRepository,
            ITenantService tenantService)
        {
            _workOrderRepository = workOrderRepository;
            _workOrderLaborRepository = workOrderLaborRepository;
            _tenantService = tenantService;
        }

        public async Task<DeleteMobileWorkOrderLaborResponse> Handle(DeleteMobileWorkOrderLaborCommand request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.WorkOrderId, out var workOrderId))
            {
                throw new DomainException("INVALID_WORK_ORDER_ID", new { Id = request.WorkOrderId });
            }

            if (!int.TryParse(request.LaborId.Replace("l", ""), out var laborId))
            {
                throw new DomainException("INVALID_LABOR_ID", new { Id = request.LaborId });
            }

            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // WorkOrder kontrolü
            var workOrder = await _workOrderRepository.Query()
                .Include(wo => wo.Items)
                .Include(wo => wo.Labors)
                .FirstOrDefaultAsync(wo => wo.Id == workOrderId && wo.ClientId == clientId, cancellationToken);

            if (workOrder == null)
            {
                throw new DomainException("WORKORDER_NOT_FOUND", new { Id = request.WorkOrderId });
            }

            // İş emri teslim edilmiş veya iptal edilmişse işçilik silinemez
            if (workOrder.Status == WorkOrderStatus.Delivered || workOrder.Status == WorkOrderStatus.Cancelled)
            {
                throw new DomainException("WORKORDER_CANNOT_DELETE_LABOR", new { Status = workOrder.Status.ToString() });
            }

            // Labor'ı bul
            var labor = await _workOrderLaborRepository.GetByIdAsync(laborId);
            if (labor == null || labor.WorkOrderId != workOrderId || labor.ClientId != clientId)
            {
                throw new DomainException("LABOR_NOT_FOUND", new { LaborId = request.LaborId });
            }

            // Labor'ı sil
            _workOrderLaborRepository.Delete(labor);
            await _workOrderLaborRepository.SaveChangesAsync();

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

            return new DeleteMobileWorkOrderLaborResponse
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
