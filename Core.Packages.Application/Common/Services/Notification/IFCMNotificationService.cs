namespace MagicCarRepairAISupported.Application.Common.Services.Notification
{
    /// <summary>
    /// Firebase Cloud Messaging (FCM) servisi
    /// </summary>
    public interface IFCMNotificationService
    {
        /// <summary>
        /// Tek bir token'a push notification gönderir
        /// </summary>
        Task<bool> SendToTokenAsync(string token, string title, string body, Dictionary<string, object>? data = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Birden fazla token'a push notification gönderir
        /// </summary>
        Task<Dictionary<string, bool>> SendToTokensAsync(List<string> tokens, string title, string body, Dictionary<string, object>? data = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Kullanıcıya push notification gönderir (tüm aktif token'larına)
        /// </summary>
        Task<bool> SendToUserAsync(int userId, string title, string body, Dictionary<string, object>? data = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Topic'e push notification gönderir
        /// </summary>
        Task<bool> SendToTopicAsync(string topic, string title, string body, Dictionary<string, object>? data = null, CancellationToken cancellationToken = default);
    }
}
