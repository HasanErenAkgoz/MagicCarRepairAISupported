namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Teklif talebi fotoğraf tipleri
    /// </summary>
    public enum QuoteRequestPhotoType
    {
        /// <summary>
        /// Hasar fotoğrafı
        /// </summary>
        Damage = 1,

        /// <summary>
        /// Arıza fotoğrafı
        /// </summary>
        Malfunction = 2,

        /// <summary>
        /// Genel görünüm
        /// </summary>
        General = 3,

        /// <summary>
        /// İç mekan
        /// </summary>
        Interior = 4,

        /// <summary>
        /// Motor bölümü
        /// </summary>
        Engine = 5
    }
}

