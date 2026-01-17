using MagicCarRepairAISupported.Application.Common.Services.SMS;
using MagicCarRepairAISupported.Infrastructure.Configurations.SMS;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Web;

namespace MagicCarRepairAISupported.Infrastructure.Services.SMS
{
    /// <summary>
    /// Netgsm SMS Gateway implementasyonu
    /// </summary>
    public class NetgsmSmsService : ISmsService
    {
        private readonly SmsSettings _smsSettings;
        private readonly ILogger<NetgsmSmsService> _logger;
        private readonly HttpClient _httpClient;

        public NetgsmSmsService(
            IOptions<SmsSettings> smsSettings,
            ILogger<NetgsmSmsService> logger,
            HttpClient httpClient)
        {
            _smsSettings = smsSettings.Value;
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<bool> SendSmsAsync(string phoneNumber, string message)
        {
            if (!_smsSettings.IsEnabled)
            {
                _logger.LogWarning("SMS service is disabled");
                return false;
            }

            try
            {
                // Telefon numarasını temizle (0 ile başlıyorsa kaldır, +90 ekle)
                var cleanPhone = CleanPhoneNumber(phoneNumber);

                // Netgsm API endpoint
                var apiUrl = _smsSettings.ApiUrl ?? "https://api.netgsm.com.tr/sms/send/get";

                // Query parameters
                var parameters = new Dictionary<string, string>
                {
                    { "usercode", _smsSettings.Username },
                    { "password", _smsSettings.Password },
                    { "gsmno", cleanPhone },
                    { "message", message },
                    { "msgheader", _smsSettings.SenderNumber }
                };

                var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={HttpUtility.UrlEncode(p.Value)}"));
                var fullUrl = $"{apiUrl}?{queryString}";

                var response = await _httpClient.GetAsync(fullUrl);
                var responseContent = await response.Content.ReadAsStringAsync();

                // Netgsm başarılı yanıt: "00" ile başlar
                if (responseContent.StartsWith("00"))
                {
                    _logger.LogInformation($"SMS sent successfully to {cleanPhone}");
                    return true;
                }
                else
                {
                    _logger.LogError($"SMS sending failed to {cleanPhone}. Response: {responseContent}");
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
            if (!_smsSettings.IsEnabled)
            {
                _logger.LogWarning("SMS service is disabled");
                return 0;
            }

            int successCount = 0;
            foreach (var phoneNumber in phoneNumbers)
            {
                if (await SendSmsAsync(phoneNumber, message))
                {
                    successCount++;
                }
                // Rate limiting için kısa bekleme
                await Task.Delay(100);
            }

            return successCount;
        }

        /// <summary>
        /// Telefon numarasını temizle ve formatla
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

            // 90 ile başlamıyorsa ekle
            if (!cleaned.StartsWith("90"))
            {
                cleaned = "90" + cleaned;
            }

            return cleaned;
        }
    }
}

