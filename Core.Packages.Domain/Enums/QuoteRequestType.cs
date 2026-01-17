namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Teklif talebi tipleri
    /// </summary>
    public enum QuoteRequestType
    {
        /// <summary>
        /// Kaza hasarı
        /// </summary>
        Accident = 1,

        /// <summary>
        /// Arıza
        /// </summary>
        Malfunction = 2,

        /// <summary>
        /// Bakım
        /// </summary>
        Maintenance = 3,

        /// <summary>
        /// Revizyon
        /// </summary>
        Overhaul = 4,

        /// <summary>
        /// Boya
        /// </summary>
        Paint = 5,

        /// <summary>
        /// Diğer
        /// </summary>
        Other = 6
    }
}

