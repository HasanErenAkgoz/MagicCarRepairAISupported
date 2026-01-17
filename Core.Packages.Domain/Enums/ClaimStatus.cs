namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Sigorta hasar durumları
    /// </summary>
    public enum ClaimStatus
    {
        /// <summary>
        /// Başvuru
        /// </summary>
        Applied = 1,

        /// <summary>
        /// İnceleme
        /// </summary>
        UnderReview = 2,

        /// <summary>
        /// Onaylandı
        /// </summary>
        Approved = 3,

        /// <summary>
        /// Reddedildi
        /// </summary>
        Rejected = 4,

        /// <summary>
        /// Ödendi
        /// </summary>
        Paid = 5,

        /// <summary>
        /// İptal
        /// </summary>
        Cancelled = 6
    }
}

