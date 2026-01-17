using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Common.Services.Payment.Dtos
{
    /// <summary>
    /// Ödeme durumu yanıtı
    /// </summary>
    public class PaymentStatusResponse
    {
        /// <summary>
        /// Ödeme durumu
        /// </summary>
        public PaymentStatus PaymentStatus { get; set; }

        /// <summary>
        /// Ödenen tutar
        /// </summary>
        public decimal PaidAmount { get; set; }

        /// <summary>
        /// Ödeme tarihi
        /// </summary>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// Gateway ödeme ID'si
        /// </summary>
        public string? GatewayPaymentId { get; set; }
    }
}





