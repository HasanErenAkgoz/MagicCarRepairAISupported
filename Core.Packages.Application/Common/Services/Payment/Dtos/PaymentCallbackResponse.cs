using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Common.Services.Payment.Dtos
{
    /// <summary>
    /// Ödeme callback yanıtı
    /// </summary>
    public class PaymentCallbackResponse
    {
        /// <summary>
        /// İşlem başarılı mı?
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Ödeme ID (bizim sistemdeki)
        /// </summary>
        public int PaymentId { get; set; }

        /// <summary>
        /// Ödeme durumu
        /// </summary>
        public PaymentStatus PaymentStatus { get; set; }

        /// <summary>
        /// Ödenen tutar
        /// </summary>
        public decimal PaidAmount { get; set; }

        /// <summary>
        /// Hata mesajı
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Hata kodu
        /// </summary>
        public string? ErrorCode { get; set; }

        /// <summary>
        /// Kart son 4 hanesi
        /// </summary>
        public string? CardLastFourDigits { get; set; }

        /// <summary>
        /// Banka adı
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// Taksit sayısı
        /// </summary>
        public int? InstallmentCount { get; set; }
    }
}





