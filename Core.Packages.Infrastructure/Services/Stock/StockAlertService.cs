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
    public class StockAlertService : IStockAlertService
    {
        private readonly IEntityRepository<StockAlert, int> _stockAlertRepository;
        private readonly IEntityRepository<Part, int> _partRepository;
        private readonly IEntityRepository<PartStock, int> _partStockRepository;
        private readonly ITenantService _tenantService;
        private readonly ILogger<StockAlertService> _logger;

        public StockAlertService(
            IEntityRepository<StockAlert, int> stockAlertRepository,
            IEntityRepository<Part, int> partRepository,
            IEntityRepository<PartStock, int> partStockRepository,
            ITenantService tenantService,
            ILogger<StockAlertService> logger)
        {
            _stockAlertRepository = stockAlertRepository;
            _partRepository = partRepository;
            _partStockRepository = partStockRepository;
            _tenantService = tenantService;
            _logger = logger;
        }

        public async Task<StockAlert?> CheckAndCreateAlertAsync(int partId, int partStockId, CancellationToken cancellationToken = default)
        {
            try
            {
                var clientId = _tenantService.GetRequiredClientId();

                var part = await _partRepository.GetByIdAsync(partId);
                if (part == null || part.ClientId != clientId)
                {
                    _logger.LogWarning($"Part {partId} not found or doesn't belong to client {clientId}");
                    return null;
                }

                var partStock = await _partStockRepository.GetByIdAsync(partStockId);
                if (partStock == null || partStock.ClientId != clientId || partStock.PartId != partId)
                {
                    _logger.LogWarning($"PartStock {partStockId} not found or doesn't match part {partId}");
                    return null;
                }

                // Alarm kontrolü sadece aktifse yapılır
                if (!part.IsLowStockAlertEnabled)
                {
                    return null;
                }

                var currentStock = partStock.Quantity;
                var minimumStock = part.MinimumStockLevel;

                // Mevcut aktif alarm var mı kontrol et
                var existingAlert = await _stockAlertRepository.Query()
                    .FirstOrDefaultAsync(sa => sa.PartId == partId && 
                                              sa.PartStockId == partStockId && 
                                              sa.Status == StockAlertStatus.Active &&
                                              sa.ClientId == clientId, cancellationToken);

                StockAlert? alert = null;

                // Stok kontrolü
                if (currentStock <= 0)
                {
                    // Stok tükendi
                    if (existingAlert == null || existingAlert.AlertType != StockAlertType.OutOfStock)
                    {
                        alert = await CreateOrUpdateAlertAsync(
                            existingAlert,
                            partId,
                            partStockId,
                            StockAlertType.OutOfStock,
                            currentStock,
                            minimumStock,
                            clientId,
                            $"Stok tükendi! Part: {part.Name} (Kod: {part.PartCode})",
                            cancellationToken);
                    }
                    else
                    {
                        // Mevcut alarmı güncelle
                        existingAlert.CurrentStock = currentStock;
                        existingAlert.LastUpdateDate = DateTime.UtcNow;
                        _stockAlertRepository.Update(existingAlert);
                        alert = existingAlert;
                    }
                }
                else if (currentStock < minimumStock * 0.3m)
                {
                    // Kritik seviye (%30'un altı)
                    if (existingAlert == null || existingAlert.AlertType != StockAlertType.Critical)
                    {
                        alert = await CreateOrUpdateAlertAsync(
                            existingAlert,
                            partId,
                            partStockId,
                            StockAlertType.Critical,
                            currentStock,
                            minimumStock,
                            clientId,
                            $"Kritik stok seviyesi! Part: {part.Name} (Kod: {part.PartCode}) - Mevcut: {currentStock}, Minimum: {minimumStock}",
                            cancellationToken);
                    }
                    else
                    {
                        existingAlert.CurrentStock = currentStock;
                        existingAlert.LastUpdateDate = DateTime.UtcNow;
                        _stockAlertRepository.Update(existingAlert);
                        alert = existingAlert;
                    }
                }
                else if (currentStock < minimumStock)
                {
                    // Düşük stok
                    if (existingAlert == null || existingAlert.AlertType != StockAlertType.LowStock)
                    {
                        alert = await CreateOrUpdateAlertAsync(
                            existingAlert,
                            partId,
                            partStockId,
                            StockAlertType.LowStock,
                            currentStock,
                            minimumStock,
                            clientId,
                            $"Düşük stok seviyesi! Part: {part.Name} (Kod: {part.PartCode}) - Mevcut: {currentStock}, Minimum: {minimumStock}",
                            cancellationToken);
                    }
                    else
                    {
                        existingAlert.CurrentStock = currentStock;
                        existingAlert.LastUpdateDate = DateTime.UtcNow;
                        _stockAlertRepository.Update(existingAlert);
                        alert = existingAlert;
                    }
                }
                else
                {
                    // Stok normal seviyede - mevcut alarm varsa çöz
                    if (existingAlert != null)
                    {
                        existingAlert.Resolve();
                        _stockAlertRepository.Update(existingAlert);
                    }
                }

                return alert;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error checking stock alert for part {partId}");
                return null;
            }
        }

        public async Task<List<StockAlert>> CheckAllStocksAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var clientId = _tenantService.GetRequiredClientId();

                var partStocks = await _partStockRepository.Query()
                    .Where(ps => ps.ClientId == clientId)
                    .Include(ps => ps.Part)
                    .ToListAsync(cancellationToken);

                var alerts = new List<StockAlert>();

                foreach (var partStock in partStocks)
                {
                    var alert = await CheckAndCreateAlertAsync(partStock.PartId, partStock.Id, cancellationToken);
                    if (alert != null)
                    {
                        alerts.Add(alert);
                    }
                }

                return alerts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking all stocks");
                return new List<StockAlert>();
            }
        }

        public async Task<List<StockAlert>> GetActiveAlertsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var clientId = _tenantService.GetRequiredClientId();

                return await _stockAlertRepository.Query()
                    .Where(sa => sa.ClientId == clientId && sa.Status == StockAlertStatus.Active)
                    .Include(sa => sa.Part)
                    .Include(sa => sa.PartStock)
                    .OrderByDescending(sa => sa.AlertType)
                    .ThenBy(sa => sa.FirstAlertDate)
                    .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active alerts");
                return new List<StockAlert>();
            }
        }

        public async Task<bool> ResolveAlertAsync(int alertId, CancellationToken cancellationToken = default)
        {
            try
            {
                var alert = await _stockAlertRepository.GetByIdAsync(alertId);
                if (alert == null)
                {
                    return false;
                }

                alert.Resolve();
                _stockAlertRepository.Update(alert);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error resolving alert {alertId}");
                return false;
            }
        }

        public async Task<decimal> CalculateRecommendedOrderQuantityAsync(int partId, CancellationToken cancellationToken = default)
        {
            try
            {
                var part = await _partRepository.GetByIdAsync(partId);
                if (part == null)
                {
                    return 0;
                }

                var partStock = await _partStockRepository.Query()
                    .FirstOrDefaultAsync(ps => ps.PartId == partId && ps.ClientId == part.ClientId, cancellationToken);

                if (partStock == null)
                {
                    return part.MinimumStockLevel * 2; // Minimum stok seviyesinin 2 katı
                }

                var currentStock = partStock.Quantity;
                var minimumStock = part.MinimumStockLevel;

                // Önerilen miktar: Minimum stok seviyesinin 2 katı - mevcut stok
                var recommended = (minimumStock * 2) - currentStock;

                return recommended > 0 ? recommended : minimumStock;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error calculating recommended order quantity for part {partId}");
                return 0;
            }
        }

        private async Task<StockAlert> CreateOrUpdateAlertAsync(
            StockAlert? existingAlert,
            int partId,
            int partStockId,
            StockAlertType alertType,
            decimal currentStock,
            decimal minimumStock,
            int clientId,
            string message,
            CancellationToken cancellationToken)
        {
            if (existingAlert != null)
            {
                // Mevcut alarmı güncelle
                existingAlert.AlertType = alertType;
                existingAlert.CurrentStock = currentStock;
                existingAlert.MinimumStock = minimumStock;
                existingAlert.Message = message;
                existingAlert.LastUpdateDate = DateTime.UtcNow;
                _stockAlertRepository.Update(existingAlert);
                return existingAlert;
            }
            else
            {
                // Yeni alarm oluştur
                var recommendedQuantity = await CalculateRecommendedOrderQuantityAsync(partId, cancellationToken);
                var alert = new StockAlert
                {
                    PartId = partId,
                    PartStockId = partStockId,
                    AlertType = alertType,
                    Status = StockAlertStatus.Active,
                    CurrentStock = currentStock,
                    MinimumStock = minimumStock,
                    RecommendedOrderQuantity = recommendedQuantity,
                    Message = message,
                    FirstAlertDate = DateTime.UtcNow,
                    LastUpdateDate = DateTime.UtcNow,
                    ClientId = clientId
                };

                await _stockAlertRepository.AddAsync(alert, cancellationToken);
                return alert;
            }
        }
    }
}

