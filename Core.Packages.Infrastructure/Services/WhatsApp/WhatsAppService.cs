using MagicCarRepairAISupported.Application.Common.Services.WhatsApp;
using MagicCarRepairAISupported.Infrastructure.Configurations.WhatsApp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace MagicCarRepairAISupported.Infrastructure.Services.WhatsApp
{
    /// <summary>
    /// WhatsApp Business API implementasyonu
    /// </summary>
    public class WhatsAppService : IWhatsAppService
    {
        private readonly WhatsAppSettings _whatsAppSettings;
        private readonly ILogger<WhatsAppService> _logger;
        private readonly HttpClient _httpClient;

        public WhatsAppService(
            IOptions<WhatsAppSettings> whatsAppSettings,
            ILogger<WhatsAppService> logger,
            HttpClient httpClient)
        {
            _whatsAppSettings = whatsAppSettings.Value;
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<bool> SendMessageAsync(string phoneNumber, string message)
        {
            if (!_whatsAppSettings.IsEnabled)
            {
                _logger.LogWarning("WhatsApp service is disabled");
                return false;
            }

            try
            {
                // Telefon numarasını temizle ve formatla
                var cleanPhone = CleanPhoneNumber(phoneNumber);

                // WhatsApp Business API endpoint
                var apiUrl = $"{_whatsAppSettings.ApiUrl}/{_whatsAppSettings.PhoneNumberId}/messages";

                // Request body
                var requestBody = new
                {
                    messaging_product = "whatsapp",
                    to = cleanPhone,
                    type = "text",
                    text = new
                    {
                        body = message
                    }
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Authorization header
                _httpClient.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _whatsAppSettings.ApiToken);

                var response = await _httpClient.PostAsync(apiUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"WhatsApp message sent successfully to {cleanPhone}");
                    return true;
                }
                else
                {
                    _logger.LogError($"WhatsApp message sending failed to {cleanPhone}. Response: {responseContent}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending WhatsApp message to {phoneNumber}");
                return false;
            }
        }

        public async Task<bool> SendMediaAsync(string phoneNumber, string mediaUrl, string mediaType, string? caption = null)
        {
            if (!_whatsAppSettings.IsEnabled)
            {
                _logger.LogWarning("WhatsApp service is disabled");
                return false;
            }

            try
            {
                var cleanPhone = CleanPhoneNumber(phoneNumber);
                var apiUrl = $"{_whatsAppSettings.ApiUrl}/{_whatsAppSettings.PhoneNumberId}/messages";

                // Dynamic object oluştur (mediaType'e göre)
                var requestBodyDict = new Dictionary<string, object>
                {
                    { "messaging_product", "whatsapp" },
                    { "to", cleanPhone },
                    { "type", mediaType },
                    { mediaType, new { link = mediaUrl, caption = caption } }
                };

                var requestBody = JsonSerializer.Serialize(requestBodyDict);

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _whatsAppSettings.ApiToken);

                var response = await _httpClient.PostAsync(apiUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"WhatsApp media sent successfully to {cleanPhone}");
                    return true;
                }
                else
                {
                    _logger.LogError($"WhatsApp media sending failed to {cleanPhone}. Response: {responseContent}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending WhatsApp media to {phoneNumber}");
                return false;
            }
        }

        public async Task<bool> SendInteractiveMessageAsync(string phoneNumber, string message, List<WhatsAppButton> buttons)
        {
            if (!_whatsAppSettings.IsEnabled)
            {
                _logger.LogWarning("WhatsApp service is disabled");
                return false;
            }

            if (buttons == null || buttons.Count == 0 || buttons.Count > 3)
            {
                _logger.LogError("WhatsApp buttons must be between 1 and 3");
                return false;
            }

            try
            {
                var cleanPhone = CleanPhoneNumber(phoneNumber);
                var apiUrl = $"{_whatsAppSettings.ApiUrl}/{_whatsAppSettings.PhoneNumberId}/messages";

                var requestBody = new
                {
                    messaging_product = "whatsapp",
                    to = cleanPhone,
                    type = "interactive",
                    interactive = new
                    {
                        type = "button",
                        body = new { text = message },
                        action = new
                        {
                            buttons = buttons.Select(b => new
                            {
                                type = "reply",
                                reply = new
                                {
                                    id = b.Id,
                                    title = b.Title
                                }
                            }).ToList()
                        }
                    }
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Authorization = 
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _whatsAppSettings.ApiToken);

                var response = await _httpClient.PostAsync(apiUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"WhatsApp interactive message sent successfully to {cleanPhone}");
                    return true;
                }
                else
                {
                    _logger.LogError($"WhatsApp interactive message sending failed to {cleanPhone}. Response: {responseContent}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending WhatsApp interactive message to {phoneNumber}");
                return false;
            }
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

