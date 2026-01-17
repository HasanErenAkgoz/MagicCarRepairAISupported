namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Aciliyet seviyeleri
    /// </summary>
    public enum UrgencyLevel
    {
        /// <summary>
        /// Düşük - Esnek tarih
        /// </summary>
        Low = 1,

        /// <summary>
        /// Normal - Standart süre
        /// </summary>
        Normal = 2,

        /// <summary>
        /// Yüksek - Acil
        /// </summary>
        High = 3,

        /// <summary>
        /// Çok acil - Mümkün olan en kısa süre
        /// </summary>
        Urgent = 4
    }
}

