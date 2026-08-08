namespace MagicCarRepairAISupported.Infrastructure.Configurations.Messaging
{
    /// <summary>
    /// Telefon üzerinden giden bildirimler (ve ISmsService üzerinden OTP) için kanal seçimi.
    /// </summary>
    public class MessagingOptions
    {
        /// <summary>
        /// "Sms" veya "WhatsApp". WhatsApp için Meta WhatsApp Business API (WhatsAppSettings) kullanılır.
        /// </summary>
        public string PhoneChannel { get; set; } = "Sms";
    }
}
