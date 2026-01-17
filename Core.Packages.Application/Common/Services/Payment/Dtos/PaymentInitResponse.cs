namespace MagicCarRepairAISupported.Application.Common.Services.Payment.Dtos
{
    /// <summary>
    /// Ödeme başlatma yanıtı
    /// </summary>
    public class PaymentInitResponse
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
        /// Gateway ödeme ID'si
        /// </summary>
        public string? GatewayPaymentId { get; set; }

        /// <summary>
        /// Gateway conversation ID'si
        /// </summary>
        public string? GatewayConversationId { get; set; }

        /// <summary>
        /// 3D Secure HTML içeriği (eğer 3D Secure gerekiyorsa)
        /// </summary>
        public string? HtmlContent { get; set; }

        /// <summary>
        /// 3D Secure URL'i (eğer 3D Secure gerekiyorsa)
        /// </summary>
        public string? RedirectUrl { get; set; }

        /// <summary>
        /// Hata mesajı
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Hata kodu
        /// </summary>
        public string? ErrorCode { get; set; }
    }
}





