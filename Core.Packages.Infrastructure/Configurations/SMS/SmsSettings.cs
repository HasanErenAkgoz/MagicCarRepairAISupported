namespace MagicCarRepairAISupported.Infrastructure.Configurations.SMS
{
    /// <summary>
    /// SMS ayarları
    /// </summary>
    public class SmsSettings
    {
        /// <summary>
        /// SMS Gateway Provider (Netgsm, Twilio)
        /// </summary>
        public SmsProvider Provider { get; set; } = SmsProvider.Netgsm;

        // Netgsm Settings
        /// <summary>
        /// API Username (Netgsm için)
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// API Password (Netgsm için)
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// API Endpoint URL (Netgsm için)
        /// </summary>
        public string ApiUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gönderen numara (başlık) - Netgsm için
        /// </summary>
        public string SenderNumber { get; set; } = string.Empty;

        // Twilio Settings
        /// <summary>
        /// Twilio Account SID
        /// </summary>
        public string TwilioAccountSid { get; set; } = string.Empty;

        /// <summary>
        /// Twilio Auth Token
        /// </summary>
        public string TwilioAuthToken { get; set; } = string.Empty;

        /// <summary>
        /// Twilio Phone Number (From number)
        /// </summary>
        public string TwilioPhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// SMS aktif mi?
        /// </summary>
        public bool IsEnabled { get; set; } = false;
    }
}

