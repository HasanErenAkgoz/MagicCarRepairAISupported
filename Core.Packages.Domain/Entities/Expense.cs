using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Gider entity'si
    /// </summary>
    public class Expense : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Gider türü
        /// </summary>
        public ExpenseType ExpenseType { get; set; }

        /// <summary>
        /// Tutar
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Ödeme yöntemi
        /// </summary>
        public PaymentMethod PaymentMethod { get; set; }

        /// <summary>
        /// İşlem tarihi
        /// </summary>
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Tedarikçi / Firma adı (opsiyonel)
        /// </summary>
        public string? SupplierName { get; set; }

        /// <summary>
        /// Fatura numarası (opsiyonel)
        /// </summary>
        public string? InvoiceNumber { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Personel ID (opsiyonel - maaş ödemeleri için)
        /// </summary>
        public int? EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }

        /// <summary>
        /// Parça Alım ID (opsiyonel - parça alımları için)
        /// </summary>
        public int? PartPurchaseId { get; set; }

        // Multi-tenant support
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}

