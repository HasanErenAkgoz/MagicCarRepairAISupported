namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Sigorta/Kasko poliçesi durumları
    /// </summary>
    public enum InsuranceStatus
    {
        /// <summary>
        /// Aktif
        /// </summary>
        Active = 1,

        /// <summary>
        /// Süresi Dolmuş
        /// </summary>
        Expired = 2,

        /// <summary>
        /// İptal
        /// </summary>
        Cancelled = 3,

        /// <summary>
        /// Beklemede
        /// </summary>
        Pending = 4
    }
}

