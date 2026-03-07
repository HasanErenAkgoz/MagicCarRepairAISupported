using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Application.Common.Services.WhatsApp;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Notifications.Commands.SendWhatsAppMessage
{
    public class SendWhatsAppMessageCommandHandler : IRequestHandler<SendWhatsAppMessageCommand, IDataResult<SendWhatsAppMessageResponse>>
    {
        private readonly IWhatsAppService _whatsAppService;
        private readonly INotificationService _notificationService;

        public SendWhatsAppMessageCommandHandler(
            IWhatsAppService whatsAppService,
            INotificationService notificationService)
        {
            _whatsAppService = whatsAppService;
            _notificationService = notificationService;
        }

        public async Task<IDataResult<SendWhatsAppMessageResponse>> Handle(SendWhatsAppMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // WhatsApp mesajı gönder
                var success = await _whatsAppService.SendMessageAsync(request.PhoneNumber, request.Message);

                if (success)
                {
                    // Notification kaydı oluştur
                    await _notificationService.SendWhatsAppNotificationAsync(
                        request.UserId,
                        request.PhoneNumber,
                        request.Message,
                        request.RelatedEntityType,
                        request.RelatedEntityId
                    );

                    return new SuccessDataResult<SendWhatsAppMessageResponse>(
                        new SendWhatsAppMessageResponse
                        {
                            Success = true,
                            Message = "WhatsApp mesajı başarıyla gönderildi."
                        },
                        "WhatsApp mesajı başarıyla gönderildi."
                    );
                }
                else
                {
                    return new ErrorDataResult<SendWhatsAppMessageResponse>(
                        new SendWhatsAppMessageResponse
                        {
                            Success = false,
                            Message = "WhatsApp mesajı gönderilemedi."
                        },
                        "WhatsApp mesajı gönderilemedi."
                    );
                }
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<SendWhatsAppMessageResponse>(
                    new SendWhatsAppMessageResponse
                    {
                        Success = false,
                        Message = $"WhatsApp mesajı gönderilirken hata oluştu: {ex.Message}"
                    },
                    $"WhatsApp mesajı gönderilirken hata oluştu: {ex.Message}"
                );
            }
        }
    }
}
