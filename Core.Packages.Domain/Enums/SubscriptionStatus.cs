namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Abonelik durumları
    /// </summary>
    public enum SubscriptionStatus
    {
        /// <summary>
        /// Aktif
        /// </summary>
        Active = 1,

        /// <summary>
        /// Süresi dolmuş
        /// </summary>
        Expired = 2,

        /// <summary>
        /// İptal edilmiş
        /// </summary>
        Cancelled = 3,

        /// <summary>
        /// Beklemede (ödeme bekleniyor)
        /// </summary>
        Pending = 4
    }
}
