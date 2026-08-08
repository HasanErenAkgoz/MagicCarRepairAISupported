namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// AI teşhis çıktısındaki parça adı ile envanter eşleşme kalitesini gösterir.
    /// </summary>
    public enum PartMatchScore
    {
        /// <summary>OEM kodu birebir eşleşti — en güvenilir eşleşme.</summary>
        Exact = 0,

        /// <summary>Araç markası + modeli + yıl aralığı uyumlu, isim benzer.</summary>
        Compatible = 1,

        /// <summary>Model uyumlu ancak yıl aralığı belirsiz ya da kısmi örtüşme.</summary>
        Risky = 2,

        /// <summary>Sadece parça adı benzerliği; araç uyumluluğu doğrulanmadı.</summary>
        Unknown = 3
    }
}
