using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Gelir entity'si
    /// </summary>
    public class Income : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// İş Emri ID (opsiyonel - iş emri geliri için)
        /// </summary>
        public int? WorkOrderId { get; set; }
        public virtual WorkOrder? WorkOrder { get; set; }

        /// <summary>
        /// Gelir türü
        /// </summary>
        public IncomeType IncomeType { get; set; }

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
        /// Açıklama
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Fatura numarası (opsiyonel)
        /// </summary>
        public string? InvoiceNumber { get; set; }

        /// <summary>
        /// Müşteri ID (opsiyonel - iş emri dışı gelirler için)
        /// </summary>
        public int? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        // Multi-tenant support
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}

