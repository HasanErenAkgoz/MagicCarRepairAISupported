using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Notifications.Commands.SendPushNotification
{
    public class SendPushNotificationCommandHandler : IRequestHandler<SendPushNotificationCommand, IDataResult<SendPushNotificationResponse>>
    {
        private readonly IFCMNotificationService _fcmService;
        private readonly IUserDeviceTokenRepository _deviceTokenRepository;
        private readonly INotificationService _notificationService;

        public SendPushNotificationCommandHandler(
            IFCMNotificationService fcmService,
            IUserDeviceTokenRepository deviceTokenRepository,
            INotificationService notificationService)
        {
            _fcmService = fcmService;
            _deviceTokenRepository = deviceTokenRepository;
            _notificationService = notificationService;
        }

        public async Task<IDataResult<SendPushNotificationResponse>> Handle(SendPushNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Kullanıcının aktif token'larını al
                var deviceTokens = await _deviceTokenRepository.GetActiveTokensByUserIdAsync(request.UserId, cancellationToken);

                if (deviceTokens == null || deviceTokens.Count == 0)
                {
                    return new ErrorDataResult<SendPushNotificationResponse>("Kullanıcının aktif cihaz token'ı bulunamadı.");
                }

                // Notification kaydı oluştur (INotificationService üzerinden)
                var notificationData = new Dictionary<string, object>();
                if (request.Data != null)
                {
                    foreach (var item in request.Data)
                    {
                        notificationData[item.Key] = item.Value ?? string.Empty;
                    }
                }

                // FCM ile push notification gönder
                var tokens = deviceTokens.Select(t => t.Token).ToList();
                var results = await _fcmService.SendToTokensAsync(tokens, request.Title, request.Body, notificationData, cancellationToken);

                var successCount = results.Values.Count(r => r);
                var failureCount = results.Values.Count(r => !r);

                // Notification kaydı oluştur
                await _notificationService.SendPushNotificationAsync(
                    request.UserId,
                    request.Title,
                    request.Body,
                    request.RelatedEntityType,
                    request.RelatedEntityId,
                    request.Data
                );

                var response = new SendPushNotificationResponse
                {
                    Success = successCount > 0,
                    Message = $"{successCount} cihaza bildirim gönderildi. {failureCount} cihaza gönderilemedi."
                };

                if (successCount > 0)
                {
                    return new SuccessDataResult<SendPushNotificationResponse>(response, response.Message);
                }
                else
                {
                    return new ErrorDataResult<SendPushNotificationResponse>(response, "Bildirim gönderilemedi.");
                }
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<SendPushNotificationResponse>($"Bildirim gönderilirken hata oluştu: {ex.Message}");
            }
        }
    }
}
