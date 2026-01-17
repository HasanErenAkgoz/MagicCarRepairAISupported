namespace MagicCarRepairAISupported.Application.Common.Services.Payment.Dtos
{
    /// <summary>
    /// Ödeme callback isteği (3D Secure sonrası)
    /// </summary>
    public class PaymentCallbackRequest
    {
        /// <summary>
        /// Gateway ödeme ID'si
        /// </summary>
        public string GatewayPaymentId { get; set; }

        /// <summary>
        /// Gateway conversation ID'si
        /// </summary>
        public string? GatewayConversationId { get; set; }

        /// <summary>
        /// 3D Secure durumu
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Gateway'den gelen ek parametreler
        /// </summary>
        public Dictionary<string, string>? AdditionalParameters { get; set; }
    }
}





