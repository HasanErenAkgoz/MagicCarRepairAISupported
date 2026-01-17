namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Stok alarm durumları
    /// </summary>
    public enum StockAlertStatus
    {
        /// <summary>
        /// Aktif alarm
        /// </summary>
        Active = 1,

        /// <summary>
        /// Çözüldü (stok yenilendi)
        /// </summary>
        Resolved = 2,

        /// <summary>
        /// İptal edildi
        /// </summary>
        Cancelled = 3,

        /// <summary>
        /// Sipariş oluşturuldu
        /// </summary>
        OrderCreated = 4
    }
}

