using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Stock;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Infrastructure.Services.Stock
{
    public class AutoOrderService : IAutoOrderService
    {
        private readonly IEntityRepository<AutoOrder, int> _autoOrderRepository;
        private readonly IEntityRepository<StockAlert, int> _stockAlertRepository;
        private readonly IEntityRepository<Part, int> _partRepository;
        private readonly IEntityRepository<PartSupplier, int> _partSupplierRepository;
        private readonly ITenantService _tenantService;
        private readonly ILogger<AutoOrderService> _logger;

        public AutoOrderService(
            IEntityRepository<AutoOrder, int> autoOrderRepository,
            IEntityRepository<StockAlert, int> stockAlertRepository,
            IEntityRepository<Part, int> partRepository,
            IEntityRepository<PartSupplier, int> partSupplierRepository,
            ITenantService tenantService,
            ILogger<AutoOrderService> logger)
        {
            _autoOrderRepository = autoOrderRepository;
            _stockAlertRepository = stockAlertRepository;
            _partRepository = partRepository;
            _partSupplierRepository = partSupplierRepository;
            _tenantService = tenantService;
            _logger = logger;
        }

        public async Task<AutoOrder?> CreateAutoOrderFromAlertAsync(int stockAlertId, int? supplierId = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId() ?? 1;

                var alert = await _stockAlertRepository.GetByIdAsync(stockAlertId);
                if (alert == null || alert.ClientId != clientId)
                {
                    _logger.LogWarning($"StockAlert {stockAlertId} not found or doesn't belong to client {clientId}");
                    return null;
                }

                if (alert.AutoOrderCreated)
                {
                    _logger.LogWarning($"AutoOrder already created for StockAlert {stockAlertId}");
                    return null;
                }

                var part = await _partRepository.GetByIdAsync(alert.PartId);
                if (part == null)
                {
                    _logger.LogWarning($"Part {alert.PartId} not found");
                    return null;
                }

                // Supplier seçimi
                PartSupplier? supplier = null;
                if (supplierId.HasValue)
                {
                    supplier = await _partSupplierRepository.GetByIdAsync(supplierId.Value);
                    if (supplier == null || supplier.ClientId != clientId)
                    {
                        _logger.LogWarning($"PartSupplier {supplierId.Value} not found");
                        supplier = null;
                    }
                }

                // Eğer supplier belirtilmemişse, part'ın varsayılan supplier'ını kullan
                if (supplier == null && part.SupplierId.HasValue)
                {
                    supplier = await _partSupplierRepository.GetByIdAsync(part.SupplierId.Value);
                }

                // Sipariş miktarı
                var quantity = alert.RecommendedOrderQuantity ?? 
                              (part.MinimumStockLevel * 2 - alert.CurrentStock);

                if (quantity <= 0)
                {
                    quantity = part.MinimumStockLevel;
                }

                // Birim fiyat (part'ın alış fiyatından)
                var unitPrice = part.PurchasePrice;

                // AutoOrder oluştur
                var autoOrder = new AutoOrder
                {
                    OrderNumber = AutoOrder.GenerateOrderNumber(),
                    PartId = alert.PartId,
                    PartSupplierId = supplier?.Id,
                    StockAlertId = stockAlertId,
                    Quantity = quantity,
                    UnitPrice = unitPrice,
                    Status = AutoOrderStatus.Pending,
                    ExpectedDeliveryDate = DateTime.UtcNow.AddDays(7), // Varsayılan 7 gün
                    ClientId = clientId
                };

                autoOrder.CalculateTotal();

                await _autoOrderRepository.AddAsync(autoOrder, cancellationToken);

                // Alert'i güncelle
                alert.MarkAutoOrderCreated(autoOrder.Id);
                _stockAlertRepository.Update(alert);

                _logger.LogInformation($"AutoOrder created: {autoOrder.OrderNumber} for Part {part.Name}");

                return autoOrder;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error creating auto order from alert {stockAlertId}");
                return null;
            }
        }

        public async Task<bool> ApproveOrderAsync(int autoOrderId, int userId, CancellationToken cancellationToken = default)
        {
            try
            {
                var autoOrder = await _autoOrderRepository.GetByIdAsync(autoOrderId);
                if (autoOrder == null)
                {
                    return false;
                }

                autoOrder.Approve(userId);
                _autoOrderRepository.Update(autoOrder);

                _logger.LogInformation($"AutoOrder {autoOrder.OrderNumber} approved by user {userId}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error approving auto order {autoOrderId}");
                return false;
            }
        }

        public async Task<bool> CancelOrderAsync(int autoOrderId, CancellationToken cancellationToken = default)
        {
            try
            {
                var autoOrder = await _autoOrderRepository.GetByIdAsync(autoOrderId);
                if (autoOrder == null)
                {
                    return false;
                }

                autoOrder.Cancel();
                _autoOrderRepository.Update(autoOrder);

                _logger.LogInformation($"AutoOrder {autoOrder.OrderNumber} cancelled");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error cancelling auto order {autoOrderId}");
                return false;
            }
        }

        public async Task<bool> MarkAsDeliveredAsync(int autoOrderId, CancellationToken cancellationToken = default)
        {
            try
            {
                var autoOrder = await _autoOrderRepository.GetByIdAsync(autoOrderId);
                if (autoOrder == null)
                {
                    return false;
                }

                autoOrder.Status = AutoOrderStatus.Delivered;
                autoOrder.ActualDeliveryDate = DateTime.UtcNow;
                _autoOrderRepository.Update(autoOrder);

                // Stok güncellemesi yapılabilir (PartStock'a ekleme)
                // Bu işlem ayrı bir servis veya command ile yapılabilir

                _logger.LogInformation($"AutoOrder {autoOrder.OrderNumber} marked as delivered");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error marking auto order {autoOrderId} as delivered");
                return false;
            }
        }

        public async Task<List<AutoOrder>> GetPendingOrdersAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId() ?? 1;

                return await _autoOrderRepository.Query()
                    .Where(ao => ao.ClientId == clientId && ao.Status == AutoOrderStatus.Pending)
                    .Include(ao => ao.Part)
                    .Include(ao => ao.PartSupplier)
                    .Include(ao => ao.StockAlert)
                    .OrderBy(ao => ao.CreatedDate)
                    .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting pending orders");
                return new List<AutoOrder>();
            }
        }
    }
}

