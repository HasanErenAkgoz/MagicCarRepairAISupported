namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Randevu durumları
    /// </summary>
    public enum AppointmentStatus
    {
        /// <summary>
        /// Planlandı
        /// </summary>
        Scheduled = 1,

        /// <summary>
        /// Onaylandı
        /// </summary>
        Confirmed = 2,

        /// <summary>
        /// Tamamlandı
        /// </summary>
        Completed = 3,

        /// <summary>
        /// İptal Edildi
        /// </summary>
        Cancelled = 4,

        /// <summary>
        /// Gelmedi (No Show)
        /// </summary>
        NoShow = 5
    }
}






