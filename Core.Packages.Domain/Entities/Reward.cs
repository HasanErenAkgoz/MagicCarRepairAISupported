using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Ödül entity'si (Loyalty program için)
    /// </summary>
    public class Reward : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Ödül adı
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Ödül açıklaması
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gerekli puan miktarı
        /// </summary>
        public int RequiredPoints { get; set; }

        /// <summary>
        /// Ödül tipi (Discount, FreeService, Gift, vb.)
        /// </summary>
        public RewardType Type { get; set; }

        /// <summary>
        /// İndirim yüzdesi (Type = Discount ise)
        /// </summary>
        public decimal? DiscountPercentage { get; set; }

        /// <summary>
        /// Sabit indirim tutarı (Type = Discount ise)
        /// </summary>
        public decimal? DiscountAmount { get; set; }

        /// <summary>
        /// Ödül görseli URL
        /// </summary>
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Aktif mi?
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Stok miktarı (sınırsız ise null)
        /// </summary>
        public int? StockQuantity { get; set; }

        /// <summary>
        /// Kullanılan miktar
        /// </summary>
        public int UsedQuantity { get; set; } = 0;

        /// <summary>
        /// Geçerlilik başlangıç tarihi
        /// </summary>
        public DateTime? ValidFrom { get; set; }

        /// <summary>
        /// Geçerlilik bitiş tarihi
        /// </summary>
        public DateTime? ValidTo { get; set; }

        /// <summary>
        /// Client ID (Multi-tenant)
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }

    public enum RewardType
    {
        Discount = 1,      // İndirim
        FreeService = 2,  // Ücretsiz servis
        Gift = 3,         // Hediye
        Cashback = 4      // Nakit iade
    }
}
