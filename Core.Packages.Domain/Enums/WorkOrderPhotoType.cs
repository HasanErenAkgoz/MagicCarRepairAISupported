namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// İş emri fotoğraf tipleri
    /// </summary>
    public enum WorkOrderPhotoType
    {
        /// <summary>
        /// Giriş Fotoğrafları
        /// </summary>
        Entry = 1,

        /// <summary>
        /// İşlem Fotoğrafları
        /// </summary>
        Process = 2,

        /// <summary>
        /// Çıkış Fotoğrafları
        /// </summary>
        Exit = 3,

        /// <summary>
        /// Arıza Fotoğrafları
        /// </summary>
        Damage = 4,

        /// <summary>
        /// Diğer
        /// </summary>
        Other = 5
    }
}

