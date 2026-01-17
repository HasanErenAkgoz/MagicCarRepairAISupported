using MagicCarRepairAISupported.Application.Common.Messages;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Stock;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddItem
{
    public class AddWorkOrderItemCommandHandler : IRequestHandler<AddWorkOrderItemCommand, AddWorkOrderItemResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IPartRepository _partRepository;
        private readonly IPartStockRepository _partStockRepository;
        private readonly IEntityRepository<WorkOrderItem, int> _workOrderItemRepository;
        private readonly IStockAlertService _stockAlertService;
        private readonly ITenantService _tenantService;

        public AddWorkOrderItemCommandHandler(
            IWorkOrderRepository workOrderRepository,
            IPartRepository partRepository,
            IPartStockRepository partStockRepository,
            IEntityRepository<WorkOrderItem, int> workOrderItemRepository,
            IStockAlertService stockAlertService,
            ITenantService tenantService)
        {
            _workOrderRepository = workOrderRepository;
            _partRepository = partRepository;
            _partStockRepository = partStockRepository;
            _workOrderItemRepository = workOrderItemRepository;
            _stockAlertService = stockAlertService;
            _tenantService = tenantService;
        }

        public async Task<AddWorkOrderItemResponse> Handle(AddWorkOrderItemCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // WorkOrder'ı bul
            var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId);
            if (workOrder == null)
            {
                throw new DomainException(Messages.NotFound, new { Entity = "WorkOrder", Id = request.WorkOrderId });
            }

            // Client kontrolü
            if (workOrder.ClientId != clientId)
            {
                throw new DomainException("WORKORDER_NOT_BELONG_TO_CLIENT", new { WorkOrderId = request.WorkOrderId });
            }

            // İş emri tamamlanmış veya teslim edilmişse item eklenemez
            if (workOrder.Status == WorkOrderStatus.Delivered || workOrder.Status == WorkOrderStatus.Cancelled)
            {
                throw new DomainException("WORKORDER_CANNOT_ADD_ITEM", new { Status = workOrder.Status });
            }

            // Part kontrolü ve stok kontrolü (ItemType = Part ise)
            if (request.ItemType == WorkOrderItemType.Part)
            {
                if (!request.PartId.HasValue)
                {
                    throw new DomainException("PART_ID_REQUIRED", new { ItemType = request.ItemType });
                }

                var part = await _partRepository.GetByIdAsync(request.PartId.Value);
                if (part == null || part.ClientId != clientId)
                {
                    throw new DomainException("PART_NOT_FOUND", new { PartId = request.PartId.Value });
                }

                // Stok kontrolü
                var stock = await _partStockRepository.GetByPartIdAsync(request.PartId.Value, cancellationToken);
                if (stock == null || stock.Quantity < (int)request.Quantity)
                {
                    throw new DomainException("PART_INSUFFICIENT_STOCK", new 
                    { 
                        PartId = request.PartId.Value,
                        PartName = part.Name,
                        RequestedQuantity = request.Quantity,
                        AvailableQuantity = stock?.Quantity ?? 0
                    });
                }

                // Stoktan düş (henüz commit edilmedi, transaction içinde)
                stock.SubtractQuantity((int)request.Quantity);
                _partStockRepository.Update(stock);
            }

            // WorkOrderItem oluştur
            var item = new WorkOrderItem
            {
                WorkOrderId = request.WorkOrderId,
                ItemType = request.ItemType,
                PartId = request.PartId,
                Description = request.Description,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice,
                DiscountPercentage = request.DiscountPercentage,
                TaxRate = request.TaxRate,
                BrandType = request.BrandType,
                WarrantyMonths = request.WarrantyMonths,
                Notes = request.Notes
            };

            // Toplam tutarı hesapla
            item.CalculateTotal();

            // Item'ı ekle
            await _workOrderItemRepository.AddAsync(item, cancellationToken);

            // WorkOrder toplamını güncelle
            workOrder.CalculateTotal();
            _workOrderRepository.Update(workOrder);
            await _workOrderRepository.SaveChangesAsync();

            // Stok düştüyse alarm kontrolü yap (Part ise)
            if (request.ItemType == WorkOrderItemType.Part && request.PartId.HasValue)
            {
                var stock = await _partStockRepository.GetByPartIdAsync(request.PartId.Value, cancellationToken);
                if (stock != null)
                {
                    // Arka planda alarm kontrolü yap (await etmeden)
                    _ = Task.Run(async () =>
                    {
                        try
                        {
                            await _stockAlertService.CheckAndCreateAlertAsync(
                                request.PartId.Value,
                                stock.Id,
                                cancellationToken);
                        }
                        catch
                        {
                            // Hata durumunda sessizce devam et
                        }
                    }, cancellationToken);
                }
            }

            // Response
            return new AddWorkOrderItemResponse
            {
                ItemId = item.Id,
                WorkOrderId = workOrder.Id,
                TotalAmount = item.TotalAmount,
                WorkOrderTotalAmount = workOrder.TotalAmount
            };
        }
    }
}

