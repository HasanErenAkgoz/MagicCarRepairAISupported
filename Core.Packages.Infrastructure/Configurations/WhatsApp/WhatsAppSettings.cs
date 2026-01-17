namespace MagicCarRepairAISupported.Infrastructure.Configurations.WhatsApp
{
    /// <summary>
    /// WhatsApp Business API ayarları
    /// </summary>
    public class WhatsAppSettings
    {
        /// <summary>
        /// API Endpoint URL
        /// </summary>
        public string ApiUrl { get; set; } = string.Empty;

        /// <summary>
        /// API Token / Access Token
        /// </summary>
        public string ApiToken { get; set; } = string.Empty;

        /// <summary>
        /// Phone Number ID (WhatsApp Business API)
        /// </summary>
        public string PhoneNumberId { get; set; } = string.Empty;

        /// <summary>
        /// Business Account ID
        /// </summary>
        public string BusinessAccountId { get; set; } = string.Empty;

        /// <summary>
        /// Webhook Verify Token
        /// </summary>
        public string WebhookVerifyToken { get; set; } = string.Empty;

        /// <summary>
        /// WhatsApp aktif mi?
        /// </summary>
        public bool IsEnabled { get; set; } = false;
    }
}

