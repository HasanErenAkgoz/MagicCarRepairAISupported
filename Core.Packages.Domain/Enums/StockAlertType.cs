namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Stok alarm tipleri
    /// </summary>
    public enum StockAlertType
    {
        /// <summary>
        /// Minimum stok seviyesi altına düştü
        /// </summary>
        LowStock = 1,

        /// <summary>
        /// Stok tükendi
        /// </summary>
        OutOfStock = 2,

        /// <summary>
        /// Kritik seviye (çok düşük)
        /// </summary>
        Critical = 3,

        /// <summary>
        /// Maksimum stok seviyesini aştı
        /// </summary>
        OverStock = 4
    }
}

