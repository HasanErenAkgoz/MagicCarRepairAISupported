using MagicCarRepairAISupported.Domain.Entities;

namespace MagicCarRepairAISupported.Application.Common.Services.Stock
{
    /// <summary>
    /// Otomatik sipariş servisi
    /// </summary>
    public interface IAutoOrderService
    {
        /// <summary>
        /// StockAlert'ten otomatik sipariş oluşturur
        /// </summary>
        Task<AutoOrder?> CreateAutoOrderFromAlertAsync(int stockAlertId, int? supplierId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Siparişi onaylar
        /// </summary>
        Task<bool> ApproveOrderAsync(int autoOrderId, int userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Siparişi iptal eder
        /// </summary>
        Task<bool> CancelOrderAsync(int autoOrderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Siparişi teslim edildi olarak işaretler
        /// </summary>
        Task<bool> MarkAsDeliveredAsync(int autoOrderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Bekleyen siparişleri getirir
        /// </summary>
        Task<List<AutoOrder>> GetPendingOrdersAsync(CancellationToken cancellationToken = default);
    }
}

