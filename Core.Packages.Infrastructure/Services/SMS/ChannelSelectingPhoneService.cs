using MagicCarRepairAISupported.Application.Common.Services.SMS;
using MagicCarRepairAISupported.Application.Common.Services.WhatsApp;
using MagicCarRepairAISupported.Infrastructure.Configurations.Messaging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MagicCarRepairAISupported.Infrastructure.Services.SMS
{
    /// <summary>
    /// <see cref="MessagingOptions.PhoneChannel"/> değerine göre Netgsm/Twilio SMS veya WhatsApp Cloud API ile gönderir.
    /// </summary>
    public class ChannelSelectingPhoneService : ISmsService
    {
        private readonly SmsServiceWrapper _smsInner;
        private readonly IWhatsAppService _whatsAppService;
        private readonly IOptions<MessagingOptions> _messagingOptions;
        private readonly ILogger<ChannelSelectingPhoneService> _logger;

        public ChannelSelectingPhoneService(
            SmsServiceWrapper smsInner,
            IWhatsAppService whatsAppService,
            IOptions<MessagingOptions> messagingOptions,
            ILogger<ChannelSelectingPhoneService> logger)
        {
            _smsInner = smsInner;
            _whatsAppService = whatsAppService;
            _messagingOptions = messagingOptions;
            _logger = logger;
        }

        private bool UseWhatsApp =>
            string.Equals(_messagingOptions.Value.PhoneChannel, "WhatsApp", StringComparison.OrdinalIgnoreCase);

        public async Task<bool> SendSmsAsync(string phoneNumber, string message)
        {
            if (UseWhatsApp)
            {
                _logger.LogDebug("Phone channel: WhatsApp (ISmsService.SendSmsAsync)");
                return await _whatsAppService.SendMessageAsync(phoneNumber, message);
            }

            return await _smsInner.SendSmsAsync(phoneNumber, message);
        }

        public async Task<int> SendBulkSmsAsync(List<string> phoneNumbers, string message)
        {
            if (UseWhatsApp)
            {
                var ok = 0;
                foreach (var p in phoneNumbers)
                {
                    if (await _whatsAppService.SendMessageAsync(p, message))
                        ok++;
                }
                return ok;
            }

            return await _smsInner.SendBulkSmsAsync(phoneNumbers, message);
        }
    }
}
