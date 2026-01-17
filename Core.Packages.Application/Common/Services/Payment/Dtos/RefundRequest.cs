namespace MagicCarRepairAISupported.Application.Common.Services.Payment.Dtos
{
    /// <summary>
    /// İade isteği
    /// </summary>
    public class RefundRequest
    {
        /// <summary>
        /// Ödeme ID (bizim sistemdeki)
        /// </summary>
        public int PaymentId { get; set; }

        /// <summary>
        /// Gateway ödeme ID'si
        /// </summary>
        public string GatewayPaymentId { get; set; }

        /// <summary>
        /// İade tutarı (tam iade için null)
        /// </summary>
        public decimal? RefundAmount { get; set; }

        /// <summary>
        /// İade açıklaması
        /// </summary>
        public string? Description { get; set; }
    }
}





