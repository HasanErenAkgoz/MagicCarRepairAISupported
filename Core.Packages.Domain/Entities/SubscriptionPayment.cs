using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Abonelik ödemesi entity'si
    /// </summary>
    public class SubscriptionPayment : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Abonelik ID (FK)
        /// </summary>
        public int SubscriptionId { get; set; }
        public virtual Subscription Subscription { get; set; }

        /// <summary>
        /// Ödeme numarası (Otomatik oluşturulur: SUB-PAY-YYYYMMDD-XXXX)
        /// </summary>
        public string PaymentNumber { get; set; }

        /// <summary>
        /// Ödeme tutarı
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Ödeme durumu
        /// </summary>
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;

        /// <summary>
        /// Ödeme yöntemi
        /// </summary>
        public PaymentMethod PaymentMethod { get; set; }

        /// <summary>
        /// Ödeme tarihi
        /// </summary>
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Ödeme gateway'i (İyzico, PayTR, vb.)
        /// </summary>
        public PaymentGateway? PaymentGateway { get; set; }

        /// <summary>
        /// Gateway ödeme ID'si
        /// </summary>
        public string? GatewayPaymentId { get; set; }

        /// <summary>
        /// Gateway conversation ID'si
        /// </summary>
        public string? GatewayConversationId { get; set; }

        /// <summary>
        /// Ödeme periyodu (Monthly, Yearly)
        /// </summary>
        public string PaymentPeriod { get; set; } = "Monthly";

        /// <summary>
        /// Ödeme açıklaması
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gateway yanıt mesajı (hata durumunda)
        /// </summary>
        public string? GatewayResponseMessage { get; set; }

        /// <summary>
        /// Gateway yanıt kodu
        /// </summary>
        public string? GatewayResponseCode { get; set; }

        // Multi-tenant support
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        /// <summary>
        /// Ödeme numarası oluşturur
        /// </summary>
        public static string GeneratePaymentNumber()
        {
            var prefix = "SUB-PAY";
            var date = DateTime.UtcNow.ToString("yyyyMMdd");
            var random = new Random().Next(1000, 9999);
            return $"{prefix}-{date}-{random}";
        }
    }
}
