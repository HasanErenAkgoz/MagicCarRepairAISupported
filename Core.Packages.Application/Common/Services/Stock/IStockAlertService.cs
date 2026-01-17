using MagicCarRepairAISupported.Domain.Entities;

namespace MagicCarRepairAISupported.Application.Common.Services.Stock
{
    /// <summary>
    /// Stok alarm servisi
    /// </summary>
    public interface IStockAlertService
    {
        /// <summary>
        /// Belirli bir part için stok kontrolü yapar ve gerekirse alarm oluşturur
        /// </summary>
        Task<StockAlert?> CheckAndCreateAlertAsync(int partId, int partStockId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Tüm part'lar için stok kontrolü yapar
        /// </summary>
        Task<List<StockAlert>> CheckAllStocksAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Aktif alarmları getirir
        /// </summary>
        Task<List<StockAlert>> GetActiveAlertsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Alarmı çözüldü olarak işaretler
        /// </summary>
        Task<bool> ResolveAlertAsync(int alertId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Part için önerilen sipariş miktarını hesaplar
        /// </summary>
        Task<decimal> CalculateRecommendedOrderQuantityAsync(int partId, CancellationToken cancellationToken = default);
    }
}

