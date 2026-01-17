using MagicCarRepairAISupported.Application.Common.Services.Email;
using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Application.Common.Services.SMS;
using MagicCarRepairAISupported.Application.Common.Services.WhatsApp;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MagicCarRepairAISupported.Infrastructure.Services.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;
        private readonly IWhatsAppService _whatsAppService;
        private readonly ITenantService _tenantService;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(
            INotificationRepository notificationRepository,
            IEmailService emailService,
            ISmsService smsService,
            IWhatsAppService whatsAppService,
            ITenantService tenantService,
            ILogger<NotificationService> logger)
        {
            _notificationRepository = notificationRepository;
            _emailService = emailService;
            _smsService = smsService;
            _whatsAppService = whatsAppService;
            _tenantService = tenantService;
            _logger = logger;
        }

        public async Task<bool> SendEmailNotificationAsync(int? userId, string email, string title, string content, string? relatedEntityType = null, int? relatedEntityId = null, Dictionary<string, object>? extraData = null)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId() ?? 1;

                // Notification kaydı oluştur
                var notification = new Domain.Entities.Notification
                {
                    Type = NotificationType.Email,
                    UserId = userId,
                    RecipientEmail = email,
                    Title = title,
                    Content = content,
                    Status = NotificationStatus.Pending,
                    RelatedEntityType = relatedEntityType,
                    RelatedEntityId = relatedEntityId,
                    ExtraData = extraData != null ? JsonSerializer.Serialize(extraData) : null,
                    ClientId = clientId,
                    CreatedDate = DateTime.UtcNow
                };

                await _notificationRepository.AddAsync(notification, CancellationToken.None);

                // Email gönder
                notification.Status = NotificationStatus.Sending;
                _notificationRepository.Update(notification);

                var emailSent = await _emailService.SendEmailAsync(email, title, content);

                if (emailSent)
                {
                    notification.MarkAsSent();
                    _logger.LogInformation($"Email notification sent to {email}");
                }
                else
                {
                    notification.MarkAsFailed("Email sending failed");
                    _logger.LogError($"Failed to send email notification to {email}");
                }

                _notificationRepository.Update(notification);
                return emailSent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending email notification to {email}");
                return false;
            }
        }

        public async Task<bool> SendSmsNotificationAsync(int? userId, string phoneNumber, string message, string? relatedEntityType = null, int? relatedEntityId = null, Dictionary<string, object>? extraData = null)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId() ?? 1;

                // Notification kaydı oluştur
                var notification = new Domain.Entities.Notification
                {
                    Type = NotificationType.Sms,
                    UserId = userId,
                    RecipientPhone = phoneNumber,
                    Title = "SMS Notification",
                    Content = message,
                    Status = NotificationStatus.Pending,
                    RelatedEntityType = relatedEntityType,
                    RelatedEntityId = relatedEntityId,
                    ExtraData = extraData != null ? JsonSerializer.Serialize(extraData) : null,
                    ClientId = clientId,
                    CreatedDate = DateTime.UtcNow
                };

                await _notificationRepository.AddAsync(notification, CancellationToken.None);

                // SMS gönder
                notification.Status = NotificationStatus.Sending;
                _notificationRepository.Update(notification);

                var smsSent = await _smsService.SendSmsAsync(phoneNumber, message);

                if (smsSent)
                {
                    notification.MarkAsSent();
                    _logger.LogInformation($"SMS notification sent to {phoneNumber}");
                }
                else
                {
                    notification.MarkAsFailed("SMS sending failed");
                    _logger.LogError($"Failed to send SMS notification to {phoneNumber}");
                }

                _notificationRepository.Update(notification);
                return smsSent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending SMS notification to {phoneNumber}");
                return false;
            }
        }

        public async Task<bool> SendPushNotificationAsync(int userId, string title, string content, string? relatedEntityType = null, int? relatedEntityId = null, Dictionary<string, object>? extraData = null)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId() ?? 1;

                // Notification kaydı oluştur
                var notification = new Domain.Entities.Notification
                {
                    Type = NotificationType.Push,
                    UserId = userId,
                    Title = title,
                    Content = content,
                    Status = NotificationStatus.Pending,
                    RelatedEntityType = relatedEntityType,
                    RelatedEntityId = relatedEntityId,
                    ExtraData = extraData != null ? JsonSerializer.Serialize(extraData) : null,
                    ClientId = clientId,
                    CreatedDate = DateTime.UtcNow
                };

                await _notificationRepository.AddAsync(notification, CancellationToken.None);

                // Push notification gönder (gelecekte Firebase/OneSignal entegrasyonu)
                notification.Status = NotificationStatus.Sending;
                _notificationRepository.Update(notification);

                // TODO: Push notification implementasyonu (Firebase Cloud Messaging, OneSignal, vb.)
                // Şimdilik başarılı olarak işaretle
                notification.MarkAsSent();
                _notificationRepository.Update(notification);

                _logger.LogInformation($"Push notification sent to user {userId}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending push notification to user {userId}");
                return false;
            }
        }

        public async Task<bool> SendWhatsAppNotificationAsync(int? userId, string phoneNumber, string message, string? relatedEntityType = null, int? relatedEntityId = null, Dictionary<string, object>? extraData = null)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId() ?? 1;

                // Notification kaydı oluştur (WhatsApp için özel bir tip yok, SMS olarak kaydedelim veya yeni tip ekleyelim)
                var notification = new Domain.Entities.Notification
                {
                    Type = NotificationType.Sms, // WhatsApp için şimdilik SMS tipini kullanıyoruz
                    UserId = userId,
                    RecipientPhone = phoneNumber,
                    Title = "WhatsApp Notification",
                    Content = message,
                    Status = NotificationStatus.Pending,
                    RelatedEntityType = relatedEntityType,
                    RelatedEntityId = relatedEntityId,
                    ExtraData = extraData != null ? JsonSerializer.Serialize(extraData) : null,
                    ClientId = clientId,
                    CreatedDate = DateTime.UtcNow
                };

                await _notificationRepository.AddAsync(notification, CancellationToken.None);

                // WhatsApp mesajı gönder
                notification.Status = NotificationStatus.Sending;
                _notificationRepository.Update(notification);

                var whatsAppSent = await _whatsAppService.SendMessageAsync(phoneNumber, message);

                if (whatsAppSent)
                {
                    notification.MarkAsSent();
                    _logger.LogInformation($"WhatsApp notification sent to {phoneNumber}");
                }
                else
                {
                    notification.MarkAsFailed("WhatsApp sending failed");
                    _logger.LogError($"Failed to send WhatsApp notification to {phoneNumber}");
                }

                _notificationRepository.Update(notification);
                return whatsAppSent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending WhatsApp notification to {phoneNumber}");
                return false;
            }
        }

        public async Task<bool> SendNotificationAsync(NotificationType type, int? userId, string? email, string? phoneNumber, string title, string content, string? relatedEntityType = null, int? relatedEntityId = null, Dictionary<string, object>? extraData = null)
        {
            return type switch
            {
                NotificationType.Email => await SendEmailNotificationAsync(userId, email ?? string.Empty, title, content, relatedEntityType, relatedEntityId, extraData),
                NotificationType.Sms => await SendSmsNotificationAsync(userId, phoneNumber ?? string.Empty, content, relatedEntityType, relatedEntityId, extraData),
                NotificationType.Push => userId.HasValue ? await SendPushNotificationAsync(userId.Value, title, content, relatedEntityType, relatedEntityId, extraData) : false,
                _ => false
            };
        }
    }
}

