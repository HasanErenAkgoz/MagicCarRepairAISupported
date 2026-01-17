using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Payments.Commands.Callback
{
    /// <summary>
    /// Ödeme callback yanıtı
    /// </summary>
    public class HandlePaymentCallbackResponse
    {
        public int PaymentId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public decimal PaidAmount { get; set; }
        public string? CardLastFourDigits { get; set; }
        public string? BankName { get; set; }
        public int? InstallmentCount { get; set; }
    }
}





