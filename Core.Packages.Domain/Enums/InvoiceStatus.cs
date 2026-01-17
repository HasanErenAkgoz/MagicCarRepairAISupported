namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Fatura durumları
    /// </summary>
    public enum InvoiceStatus
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
        /// Kısmen Ödendi
        /// </summary>
        PartiallyPaid = 3,

        /// <summary>
        /// Vadesi Geçti
        /// </summary>
        Overdue = 4,

        /// <summary>
        /// İptal Edildi
        /// </summary>
        Cancelled = 5
    }
}

