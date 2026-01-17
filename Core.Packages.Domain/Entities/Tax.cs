using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Vergi entity'si
    /// </summary>
    public class Tax : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Vergi türü
        /// </summary>
        public TaxType TaxType { get; set; }

        /// <summary>
        /// Dönem (Ay)
        /// </summary>
        public int? Month { get; set; }

        /// <summary>
        /// Dönem (Yıl)
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Tutar
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Son Ödeme Tarihi
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Ödeme Tarihi (opsiyonel - ödenmişse)
        /// </summary>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// Durum
        /// </summary>
        public TaxStatus Status { get; set; } = TaxStatus.Pending;

        /// <summary>
        /// Ödeme Yöntemi (opsiyonel)
        /// </summary>
        public PaymentMethod? PaymentMethod { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Ödeme Referans No (Banka işlem no, vb.)
        /// </summary>
        public string? PaymentReferenceNumber { get; set; }

        /// <summary>
        /// Vergi Dairesi
        /// </summary>
        public string? TaxOffice { get; set; }

        /// <summary>
        /// Vergi No / TC No
        /// </summary>
        public string? TaxNumber { get; set; }

        // Multi-tenant support
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        /// <summary>
        /// Vadesi geçmiş mi kontrol et
        /// </summary>
        public bool IsOverdue()
        {
            return Status == TaxStatus.Pending && DueDate < DateTime.UtcNow;
        }

        /// <summary>
        /// Ödeme yap
        /// </summary>
        public void MarkAsPaid(DateTime paymentDate, PaymentMethod paymentMethod, string? referenceNumber = null)
        {
            Status = TaxStatus.Paid;
            PaymentDate = paymentDate;
            PaymentMethod = paymentMethod;
            PaymentReferenceNumber = referenceNumber;
        }
    }
}
