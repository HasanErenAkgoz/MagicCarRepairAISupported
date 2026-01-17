namespace MagicCarRepairAISupported.Infrastructure.Configurations.SMS
{
    /// <summary>
    /// SMS ayarları
    /// </summary>
    public class SmsSettings
    {
        /// <summary>
        /// SMS Gateway Provider (Netgsm, IletiMerkezi, Mutlucell, vb.)
        /// </summary>
        public string Provider { get; set; } = "Netgsm";

        /// <summary>
        /// API Username
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// API Password
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// API Endpoint URL
        /// </summary>
        public string ApiUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gönderen numara (başlık)
        /// </summary>
        public string SenderNumber { get; set; } = string.Empty;

        /// <summary>
        /// SMS aktif mi?
        /// </summary>
        public bool IsEnabled { get; set; } = false;
    }
}

