using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Ödeme entity'si
    /// </summary>
    public class Payment : BaseEntity<int>, IClientEntity
    {
        /// <summary>
        /// Ödeme numarası (Otomatik oluşturulur: PAY-YYYYMMDD-XXXX)
        /// </summary>
        public string PaymentNumber { get; set; }

        /// <summary>
        /// Fatura ID (opsiyonel - fatura ödemesi için)
        /// </summary>
        public int? InvoiceId { get; set; }
        public virtual Invoice? Invoice { get; set; }

        /// <summary>
        /// İş Emri ID (opsiyonel - iş emri ödemesi için)
        /// </summary>
        public int? WorkOrderId { get; set; }
        public virtual WorkOrder? WorkOrder { get; set; }

        /// <summary>
        /// Müşteri ID
        /// </summary>
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Ödeme tutarı
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Ödeme yöntemi
        /// </summary>
        public PaymentMethod PaymentMethod { get; set; }

        /// <summary>
        /// Ödeme durumu
        /// </summary>
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;

        /// <summary>
        /// Ödeme tarihi
        /// </summary>
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Ödeme gateway'i (İyzico, PayTR, vb.)
        /// </summary>
        public PaymentGateway? PaymentGateway { get; set; }

        /// <summary>
        /// Gateway ödeme ID'si (İyzico payment ID, vb.)
        /// </summary>
        public string? GatewayPaymentId { get; set; }

        /// <summary>
        /// Gateway conversation ID'si
        /// </summary>
        public string? GatewayConversationId { get; set; }

        /// <summary>
        /// Taksit sayısı (opsiyonel)
        /// </summary>
        public int? InstallmentCount { get; set; }

        /// <summary>
        /// Kart son 4 hanesi (opsiyonel)
        /// </summary>
        public string? CardLastFourDigits { get; set; }

        /// <summary>
        /// Kart sahibi adı (opsiyonel)
        /// </summary>
        public string? CardHolderName { get; set; }

        /// <summary>
        /// Banka adı (opsiyonel)
        /// </summary>
        public string? BankName { get; set; }

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

        /// <summary>
        /// İade durumu
        /// </summary>
        public bool IsRefunded { get; set; } = false;

        /// <summary>
        /// İade tarihi
        /// </summary>
        public DateTime? RefundDate { get; set; }

        /// <summary>
        /// İade tutarı
        /// </summary>
        public decimal? RefundAmount { get; set; }

        /// <summary>
        /// İade açıklaması
        /// </summary>
        public string? RefundDescription { get; set; }

        /// <summary>
        /// Gateway iade ID'si
        /// </summary>
        public string? GatewayRefundId { get; set; }

        // Multi-tenant support
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        /// <summary>
        /// Ödeme numarası oluşturur
        /// </summary>
        public static string GeneratePaymentNumber()
        {
            var prefix = "PAY";
            var date = DateTime.UtcNow.ToString("yyyyMMdd");
            var random = new Random().Next(1000, 9999);
            return $"{prefix}-{date}-{random}";
        }
    }
}





