namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Vergi durumları
    /// </summary>
    public enum TaxStatus
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
        /// Vadesi Geçti
        /// </summary>
        Overdue = 3,

        /// <summary>
        /// İptal
        /// </summary>
        Cancelled = 4
    }
}
