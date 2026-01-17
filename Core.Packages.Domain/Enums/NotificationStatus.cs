namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Bildirim durumları
    /// </summary>
    public enum NotificationStatus
    {
        /// <summary>
        /// Beklemede - Henüz gönderilmedi
        /// </summary>
        Pending = 1,

        /// <summary>
        /// Gönderiliyor
        /// </summary>
        Sending = 2,

        /// <summary>
        /// Gönderildi
        /// </summary>
        Sent = 3,

        /// <summary>
        /// Başarısız
        /// </summary>
        Failed = 4,

        /// <summary>
        /// Okundu
        /// </summary>
        Read = 5,

        /// <summary>
        /// İptal edildi
        /// </summary>
        Cancelled = 6
    }
}

