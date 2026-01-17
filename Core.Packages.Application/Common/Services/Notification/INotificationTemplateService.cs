namespace MagicCarRepairAISupported.Application.Common.Services.Notification
{
    /// <summary>
    /// Bildirim şablon servisi
    /// </summary>
    public interface INotificationTemplateService
    {
        /// <summary>
        /// Şablon adına göre bildirim gönderir
        /// </summary>
        Task<bool> SendNotificationByTemplateAsync(
            string templateName,
            int? userId,
            string? email,
            string? phoneNumber,
            Dictionary<string, object> variables,
            string? relatedEntityType = null,
            int? relatedEntityId = null);
    }
}

