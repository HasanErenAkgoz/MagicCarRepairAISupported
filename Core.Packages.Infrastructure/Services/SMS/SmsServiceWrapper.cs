using MagicCarRepairAISupported.Application.Common.Services.SMS;
using MagicCarRepairAISupported.Infrastructure.Configurations.SMS;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MagicCarRepairAISupported.Infrastructure.Services.SMS
{
    /// <summary>
    /// SMS Service Wrapper - Configuration'a göre doğru SMS servisini kullanır
    /// </summary>
    public class SmsServiceWrapper : ISmsService
    {
        private readonly ISmsService _smsService;
        private readonly ILogger<SmsServiceWrapper> _logger;

        public SmsServiceWrapper(
            NetgsmSmsService netgsmSmsService,
            TwilioSmsService twilioSmsService,
            IOptions<SmsSettings> smsSettings,
            ILogger<SmsServiceWrapper> logger)
        {
            _logger = logger;
            var provider = smsSettings.Value.Provider;
            
            _smsService = provider switch
            {
                SmsProvider.Netgsm => netgsmSmsService,
                SmsProvider.Twilio => twilioSmsService,
                _ => throw new NotSupportedException($"SMS Provider '{provider}' is not supported.")
            };
            
            _logger.LogInformation($"SMS Service initialized with provider: {provider}");
        }

        public async Task<bool> SendSmsAsync(string phoneNumber, string message)
        {
            return await _smsService.SendSmsAsync(phoneNumber, message);
        }

        public async Task<int> SendBulkSmsAsync(List<string> phoneNumbers, string message)
        {
            return await _smsService.SendBulkSmsAsync(phoneNumbers, message);
        }
    }
}
