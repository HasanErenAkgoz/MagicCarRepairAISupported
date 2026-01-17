using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Common.Services.Notification
{
    /// <summary>
    /// Bildirim servisi - Tüm bildirim tiplerini yönetir
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Email bildirimi gönderir
        /// </summary>
        Task<bool> SendEmailNotificationAsync(int? userId, string email, string title, string content, string? relatedEntityType = null, int? relatedEntityId = null, Dictionary<string, object>? extraData = null);

        /// <summary>
        /// SMS bildirimi gönderir
        /// </summary>
        Task<bool> SendSmsNotificationAsync(int? userId, string phoneNumber, string message, string? relatedEntityType = null, int? relatedEntityId = null, Dictionary<string, object>? extraData = null);

        /// <summary>
        /// Push bildirimi gönderir
        /// </summary>
        Task<bool> SendPushNotificationAsync(int userId, string title, string content, string? relatedEntityType = null, int? relatedEntityId = null, Dictionary<string, object>? extraData = null);

        /// <summary>
        /// WhatsApp bildirimi gönderir
        /// </summary>
        Task<bool> SendWhatsAppNotificationAsync(int? userId, string phoneNumber, string message, string? relatedEntityType = null, int? relatedEntityId = null, Dictionary<string, object>? extraData = null);

        /// <summary>
        /// Genel bildirim gönderir (tip'e göre otomatik yönlendirir)
        /// </summary>
        Task<bool> SendNotificationAsync(NotificationType type, int? userId, string? email, string? phoneNumber, string title, string content, string? relatedEntityType = null, int? relatedEntityId = null, Dictionary<string, object>? extraData = null);
    }
}

