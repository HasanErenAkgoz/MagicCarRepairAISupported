namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Stok hareket tipi
    /// </summary>
    public enum StockMovementType
    {
        /// <summary>
        /// Giriş (Alım, İade, vb.)
        /// </summary>
        In = 1,

        /// <summary>
        /// Çıkış (Satış, Kullanım, İade, vb.)
        /// </summary>
        Out = 2,

        /// <summary>
        /// Sayım Düzeltmesi
        /// </summary>
        Adjustment = 3,

        /// <summary>
        /// Transfer (Başka depoya)
        /// </summary>
        Transfer = 4
    }
}

