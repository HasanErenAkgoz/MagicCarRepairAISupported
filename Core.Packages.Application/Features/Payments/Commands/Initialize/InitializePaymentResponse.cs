namespace MagicCarRepairAISupported.Application.Features.Payments.Commands.Initialize
{
    /// <summary>
    /// Ödeme başlatma yanıtı
    /// </summary>
    public class InitializePaymentResponse
    {
        public int PaymentId { get; set; }
        public string PaymentNumber { get; set; }
        public string? GatewayPaymentId { get; set; }
        public string? GatewayConversationId { get; set; }
        public string? HtmlContent { get; set; }
        public string? RedirectUrl { get; set; }
        public bool Requires3DSecure { get; set; }
    }
}





