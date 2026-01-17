namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Rating (Değerlendirme) durumları
    /// </summary>
    public enum RatingStatus
    {
        /// <summary>
        /// Beklemede (Admin onayı bekliyor)
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
