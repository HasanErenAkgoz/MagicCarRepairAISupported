namespace MagicCarRepairAISupported.Application.Common.Services.Payment.Dtos
{
    /// <summary>
    /// İade yanıtı
    /// </summary>
    public class RefundResponse
    {
        /// <summary>
        /// İşlem başarılı mı?
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// İade tutarı
        /// </summary>
        public decimal RefundAmount { get; set; }

        /// <summary>
        /// Gateway iade ID'si
        /// </summary>
        public string? GatewayRefundId { get; set; }

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





