namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Komisyon durumları
    /// </summary>
    public enum CommissionStatus
    {
        /// <summary>
        /// Beklemede
        /// </summary>
        Pending = 1,

        /// <summary>
        /// Ödendi
        /// </summary>
        Paid = 2,

        /// <summary>
        /// İade edildi
        /// </summary>
        Refunded = 3
    }
}
