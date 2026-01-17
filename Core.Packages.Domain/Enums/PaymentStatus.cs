namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Ödeme durumları
    /// </summary>
    public enum PaymentStatus
    {
        /// <summary>
        /// Ödenmedi
        /// </summary>
        Unpaid = 1,

        /// <summary>
        /// Kısmen Ödendi
        /// </summary>
        PartiallyPaid = 2,

        /// <summary>
        /// Ödendi
        /// </summary>
        Paid = 3,

        /// <summary>
        /// İade Edildi
        /// </summary>
        Refunded = 4
    }
}

