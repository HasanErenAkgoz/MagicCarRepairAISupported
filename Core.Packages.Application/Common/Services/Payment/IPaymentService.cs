using MagicCarRepairAISupported.Application.Common.Services.Payment.Dtos;
using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Common.Services.Payment
{
    /// <summary>
    /// Ödeme servisi interface'i
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Ödeme başlatır
        /// </summary>
        Task<PaymentInitResponse> InitializePaymentAsync(PaymentInitRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Ödeme callback'ini işler (3D Secure sonrası)
        /// </summary>
        Task<PaymentCallbackResponse> HandlePaymentCallbackAsync(PaymentCallbackRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Ödeme durumunu kontrol eder
        /// </summary>
        Task<PaymentStatusResponse> CheckPaymentStatusAsync(string paymentId, CancellationToken cancellationToken = default);

        /// <summary>
        /// İade işlemi yapar
        /// </summary>
        Task<RefundResponse> RefundPaymentAsync(RefundRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Taksit seçeneklerini getirir
        /// </summary>
        Task<List<InstallmentOption>> GetInstallmentOptionsAsync(decimal amount, CancellationToken cancellationToken = default);
    }
}





