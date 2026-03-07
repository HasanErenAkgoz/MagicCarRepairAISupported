using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Komisyon entity'si
    /// </summary>
    public class Commission : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Ödeme ID (FK)
        /// </summary>
        public int PaymentId { get; set; }
        public virtual Payment Payment { get; set; }

        /// <summary>
        /// Komisyon tutarı
        /// </summary>
        public decimal CommissionAmount { get; set; }

        /// <summary>
        /// Komisyon oranı (%)
        /// </summary>
        public decimal CommissionRate { get; set; }

        /// <summary>
        /// Komisyon durumu
        /// </summary>
        public new CommissionStatus Status { get; set; } = CommissionStatus.Pending;

        /// <summary>
        /// Ödeme tarihi (komisyon ödemesi)
        /// </summary>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// İade tarihi (eğer iade edildiyse)
        /// </summary>
        public DateTime? RefundDate { get; set; }

        /// <summary>
        /// İade nedeni
        /// </summary>
        public string? RefundReason { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string? Description { get; set; }

        // Multi-tenant support
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
