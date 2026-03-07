using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Fatura kalemi entity'si
    /// </summary>
    public class InvoiceItem : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Fatura ID
        /// </summary>
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Miktar
        /// </summary>
        public decimal Quantity { get; set; } = 1;

        /// <summary>
        /// Birim fiyat
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// KDV oranı (%)
        /// </summary>
        public decimal TaxRate { get; set; } = 20; // Varsayılan %20 KDV

        /// <summary>
        /// KDV tutarı
        /// </summary>
        public decimal TaxAmount { get; private set; }

        /// <summary>
        /// Toplam tutar (KDV dahil)
        /// </summary>
        public decimal TotalAmount { get; private set; }

        // Multi-tenant support
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        /// <summary>
        /// Toplam tutarı hesaplar
        /// </summary>
        public void CalculateTotal()
        {
            var subtotal = Quantity * UnitPrice;
            TaxAmount = subtotal * (TaxRate / 100);
            TotalAmount = subtotal + TaxAmount;
        }
    }
}

