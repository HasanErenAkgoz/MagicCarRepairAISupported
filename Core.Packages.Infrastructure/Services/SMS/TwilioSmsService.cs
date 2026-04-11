using MagicCarRepairAISupported.Application.Common.Services.SMS;
using MagicCarRepairAISupported.Infrastructure.Configurations.SMS;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace MagicCarRepairAISupported.Infrastructure.Services.SMS
{
    /// <summary>
    /// Twilio SMS Gateway implementasyonu
    /// </summary>
    public class TwilioSmsService : ISmsService
    {
        private readonly SmsSettings _smsSettings;
        private readonly ILogger<TwilioSmsService> _logger;
        private bool _initialized = false;

        public TwilioSmsService(
            IOptions<SmsSettings> smsSettings,
            ILogger<TwilioSmsService> logger)
        {
            _smsSettings = smsSettings.Value;
            _logger = logger;
            InitializeTwilio();
        }

        private void InitializeTwilio()
        {
            if (!_smsSettings.IsEnabled)
            {
                _logger.LogWarning("SMS service is disabled");
                return;
            }

            if (string.IsNullOrWhiteSpace(_smsSettings.TwilioAccountSid) || 
                string.IsNullOrWhiteSpace(_smsSettings.TwilioAuthToken))
            {
                _logger.LogWarning("Twilio credentials not configured. SMS service will not work.");
                return;
            }

            try
            {
                TwilioClient.Init(_smsSettings.TwilioAccountSid, _smsSettings.TwilioAuthToken);
                _initialized = true;
                _logger.LogInformation("Twilio SMS service initialized successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize Twilio SMS service");
                _initialized = false;
            }
        }

        public async Task<bool> SendSmsAsync(string phoneNumber, string message)
        {
            if (!_smsSettings.IsEnabled)
            {
                _logger.LogWarning("SMS service is disabled");
                return false;
            }

            if (!_initialized)
            {
                _logger.LogError("Twilio not initialized. Cannot send SMS.");
                return false;
            }

            try
            {
                // Telefon numarasını temizle ve formatla
                var cleanPhone = CleanPhoneNumber(phoneNumber);

                // Twilio Phone Number kontrolü
                if (string.IsNullOrWhiteSpace(_smsSettings.TwilioPhoneNumber))
                {
                    _logger.LogError("Twilio phone number not configured");
                    return false;
                }

                // SMS gönder
                var messageResource = await MessageResource.CreateAsync(
                    body: message,
                    from: new PhoneNumber(_smsSettings.TwilioPhoneNumber),
                    to: new PhoneNumber(cleanPhone)
                );

                // Twilio response kontrolü
                if (messageResource.Status == MessageResource.StatusEnum.Queued ||
                    messageResource.Status == MessageResource.StatusEnum.Sent ||
                    messageResource.Status == MessageResource.StatusEnum.Sending)
                {
                    _logger.LogInformation($"SMS sent successfully to {cleanPhone}. Status: {messageResource.Status}, SID: {messageResource.Sid}");
                    return true;
                }
                else
                {
                    _logger.LogError($"SMS sending failed to {cleanPhone}. Status: {messageResource.Status}, Error: {messageResource.ErrorMessage}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending SMS to {phoneNumber}");
                return false;
            }
        }

        public async Task<int> SendBulkSmsAsync(List<string> phoneNumbers, string message)
        {
            if (!_smsSettings.IsEnabled || !_initialized)
            {
                _logger.LogWarning("SMS service is disabled or not initialized");
                return 0;
            }

            int successCount = 0;
            foreach (var phoneNumber in phoneNumbers)
            {
                if (await SendSmsAsync(phoneNumber, message))
                {
                    successCount++;
                }
                // Rate limiting için kısa bekleme (Twilio rate limit: ~1 SMS/second)
                await Task.Delay(1000);
            }

            return successCount;
        }

        /// <summary>
        /// Telefon numarasını temizle ve formatla (E.164 format)
        /// </summary>
        private string CleanPhoneNumber(string phoneNumber)
        {
            // Boşluk, tire, parantez gibi karakterleri kaldır
            var cleaned = phoneNumber.Replace(" ", "")
                                    .Replace("-", "")
                                    .Replace("(", "")
                                    .Replace(")", "")
                                    .Replace("+", "");

            // 0 ile başlıyorsa kaldır
            if (cleaned.StartsWith("0"))
            {
                cleaned = cleaned.Substring(1);
            }

            // 90 ile başlamıyorsa ekle (Türkiye için)
            if (!cleaned.StartsWith("90"))
            {
                cleaned = "90" + cleaned;
            }

            // E.164 format: + ile başlamalı
            return "+" + cleaned;
        }
    }
}
