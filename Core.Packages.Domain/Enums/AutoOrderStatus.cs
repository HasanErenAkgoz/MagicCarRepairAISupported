namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Otomatik sipariş durumları
    /// </summary>
    public enum AutoOrderStatus
    {
        /// <summary>
        /// Beklemede
        /// </summary>
        Pending = 1,

        /// <summary>
        /// Onaylandı
        /// </summary>
        Approved = 2,

        /// <summary>
        /// Sipariş verildi
        /// </summary>
        Ordered = 3,

        /// <summary>
        /// Teslim edildi
        /// </summary>
        Delivered = 4,

        /// <summary>
        /// İptal edildi
        /// </summary>
        Cancelled = 5
    }
}

