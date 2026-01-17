namespace MagicCarRepairAISupported.Application.Common.Services.Notification
{
    /// <summary>
    /// SignalR ile real-time bildirim servisi
    /// </summary>
    public interface ISignalRNotificationService
    {
        /// <summary>
        /// Kullanıcıya real-time bildirim gönderir
        /// </summary>
        Task SendNotificationToUserAsync(int userId, string title, string content, string? relatedEntityType = null, int? relatedEntityId = null);

        /// <summary>
        /// Client'e real-time bildirim gönderir
        /// </summary>
        Task SendNotificationToClientAsync(int clientId, string title, string content, string? relatedEntityType = null, int? relatedEntityId = null);

        /// <summary>
        /// WorkOrder durum güncellemesi gönderir
        /// </summary>
        Task SendWorkOrderUpdateAsync(int workOrderId, string status, string message, int? userId = null);
    }
}

