namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Müşteri onay durumları
    /// </summary>
    public enum CustomerApprovalStatus
    {
        /// <summary>
        /// Onay bekleniyor
        /// </summary>
        Pending = 1,

        /// <summary>
        /// Onaylandı
        /// </summary>
        Approved = 2,

        /// <summary>
        /// Reddedildi
        /// </summary>
        Rejected = 3
    }
}
