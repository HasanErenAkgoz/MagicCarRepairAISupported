namespace MagicCarRepairAISupported.Application.Common.Services.Cache
{
    /// <summary>
    /// Cache invalidation servisi - Entity güncellemelerinde ilgili cache'leri temizler
    /// </summary>
    public interface ICacheInvalidationService
    {
        /// <summary>
        /// Part ile ilgili cache'leri temizle
        /// </summary>
        Task InvalidatePartCacheAsync(int? partId = null);

        /// <summary>
        /// WorkOrder ile ilgili cache'leri temizle
        /// </summary>
        Task InvalidateWorkOrderCacheAsync(int? workOrderId = null);

        /// <summary>
        /// Dashboard cache'lerini temizle
        /// </summary>
        Task InvalidateDashboardCacheAsync(int? clientId = null);

        /// <summary>
        /// Report cache'lerini temizle
        /// </summary>
        Task InvalidateReportCacheAsync(int? clientId = null);

        /// <summary>
        /// Supplier ile ilgili cache'leri temizle
        /// </summary>
        Task InvalidateSupplierCacheAsync(int? supplierId = null);

        /// <summary>
        /// Customer ile ilgili cache'leri temizle
        /// </summary>
        Task InvalidateCustomerCacheAsync(int? customerId = null);

        /// <summary>
        /// Belirli bir pattern'a göre cache'leri temizle
        /// </summary>
        Task InvalidateByPatternAsync(string pattern);
    }
}

