namespace MagicCarRepairAISupported.Application.Common.Services.Payment
{
    /// <summary>
    /// Mobil ödeme servisi (İyzico Mobile SDK için)
    /// </summary>
    public interface IMobilePaymentService
    {
        /// <summary>
        /// Mobil ödeme oturumu başlatır ve checkout form URL'i döner
        /// </summary>
        Task<MobilePaymentInitResponse> InitializeMobilePaymentAsync(MobilePaymentInitRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Ödeme sonucunu doğrular
        /// </summary>
        Task<MobilePaymentVerifyResponse> VerifyPaymentAsync(string conversationId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Taksit seçeneklerini getirir
        /// </summary>
        Task<List<MobileInstallmentOption>> GetInstallmentOptionsAsync(decimal amount, string binNumber, CancellationToken cancellationToken = default);
    }

    public class MobilePaymentInitRequest
    {
        public int InvoiceId { get; set; }
        public int? WorkOrderId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerSurname { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string? CustomerIdentityNumber { get; set; }
        public string? CustomerAddress { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "TRY";
        public int? InstallmentCount { get; set; }
        public string? Description { get; set; }
    }

    public class MobilePaymentInitResponse
    {
        public bool Success { get; set; }
        public string? CheckoutFormContent { get; set; } // HTML content for mobile SDK
        public string? Token { get; set; }
        public string? ConversationId { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class MobilePaymentVerifyResponse
    {
        public bool Success { get; set; }
        public string? PaymentStatus { get; set; }
        public string? ErrorMessage { get; set; }
        public int? PaymentId { get; set; }
    }

    public class MobileInstallmentOption
    {
        public int InstallmentNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal MonthlyAmount { get; set; }
    }
}
