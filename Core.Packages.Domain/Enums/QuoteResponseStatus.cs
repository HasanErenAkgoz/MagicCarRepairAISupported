namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Teklif yanıt durumları
    /// </summary>
    public enum QuoteResponseStatus
    {
        /// <summary>
        /// Beklemede - Müşteri henüz karar vermedi
        /// </summary>
        Pending = 1,

        /// <summary>
        /// Kabul edildi - Müşteri bu teklifi seçti
        /// </summary>
        Accepted = 2,

        /// <summary>
        /// Reddedildi - Müşteri bu teklifi reddetti
        /// </summary>
        Rejected = 3,

        /// <summary>
        /// İptal edildi - Servis teklifi geri çekti
        /// </summary>
        Withdrawn = 4,

        /// <summary>
        /// Süresi doldu - Geçerlilik tarihi geçti
        /// </summary>
        Expired = 5
    }
}

