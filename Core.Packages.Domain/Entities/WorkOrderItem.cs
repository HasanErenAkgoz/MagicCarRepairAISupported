using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// İş emri kalem entity'si (Parça, İşçilik, Dış Hizmet)
    /// </summary>
    public class WorkOrderItem : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// İş emri ID
        /// </summary>
        public int WorkOrderId { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }

        /// <summary>
        /// Kalem Tipi
        /// </summary>
        public WorkOrderItemType ItemType { get; set; }

        /// <summary>
        /// Parça ID (ItemType = Part ise)
        /// </summary>
        public int? PartId { get; set; }
        public virtual Part? Part { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Miktar
        /// </summary>
        public decimal Quantity { get; set; } = 1;

        /// <summary>
        /// Birim Fiyat
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// İndirim Tutarı
        /// </summary>
        public decimal DiscountAmount { get; set; } = 0;

        /// <summary>
        /// İndirim Yüzdesi
        /// </summary>
        public decimal DiscountPercentage { get; set; } = 0;

        /// <summary>
        /// KDV Oranı
        /// </summary>
        public decimal TaxRate { get; set; } = 20; // %20 KDV

        /// <summary>
        /// KDV Tutarı
        /// </summary>
        public decimal TaxAmount { get; set; } = 0;

        /// <summary>
        /// Toplam Tutar (Miktar * Birim Fiyat - İndirim + KDV)
        /// </summary>
        public decimal TotalAmount { get; set; } = 0;

        /// <summary>
        /// Marka (Orijinal/Emsal)
        /// </summary>
        public PartBrandType? BrandType { get; set; }

        /// <summary>
        /// Garanti Süresi (Ay)
        /// </summary>
        public int? WarrantyMonths { get; set; }

        /// <summary>
        /// Notlar
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Multi-tenant support
        /// </summary>
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        /// <summary>
        /// Toplam tutarı hesaplar
        /// </summary>
        public void CalculateTotal()
        {
            // Ara toplam
            var subtotal = Quantity * UnitPrice;

            // İndirim uygula
            if (DiscountPercentage > 0)
            {
                DiscountAmount = subtotal * (DiscountPercentage / 100);
            }

            // İndirim sonrası tutar
            var amountAfterDiscount = subtotal - DiscountAmount;

            // KDV hesapla
            TaxAmount = amountAfterDiscount * (TaxRate / 100);

            // Toplam tutar
            TotalAmount = amountAfterDiscount + TaxAmount;
        }
    }
}

