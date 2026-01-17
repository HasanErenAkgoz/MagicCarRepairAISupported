using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Payments.Queries.GetPaymentHistory
{
    /// <summary>
    /// Ödeme geçmişi yanıtı
    /// </summary>
    public class GetPaymentHistoryResponse
    {
        public int Id { get; set; }
        public string PaymentNumber { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentGateway? PaymentGateway { get; set; }
        public DateTime PaymentDate { get; set; }
        public int? InvoiceId { get; set; }
        public int? WorkOrderId { get; set; }
        public string? CardLastFourDigits { get; set; }
        public int? InstallmentCount { get; set; }
        public bool IsRefunded { get; set; }
    }
}





