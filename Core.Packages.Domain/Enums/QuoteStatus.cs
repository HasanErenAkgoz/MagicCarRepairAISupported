namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Teklif talebi durumları
    /// </summary>
    public enum QuoteStatus
    {
        /// <summary>
        /// Açık - Teklif bekleniyor
        /// </summary>
        Open = 1,

        /// <summary>
        /// Teklifler alındı - Müşteri seçim yapabilir
        /// </summary>
        QuotesReceived = 2,

        /// <summary>
        /// Teklif seçildi - Bir servis seçildi
        /// </summary>
        QuoteSelected = 3,

        /// <summary>
        /// İptal edildi
        /// </summary>
        Cancelled = 4,

        /// <summary>
        /// Süresi doldu - Son teklif tarihi geçti
        /// </summary>
        Expired = 5
    }
}

