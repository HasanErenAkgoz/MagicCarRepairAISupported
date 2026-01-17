using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    /// <summary>
    /// Notification repository interface
    /// </summary>
    public interface INotificationRepository : IEntityRepository<Notification, int>
    {
        /// <summary>
        /// Get notifications by user
        /// </summary>
        Task<List<Notification>> GetNotificationsByUserAsync(int userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get unread notifications by user
        /// </summary>
        Task<List<Notification>> GetUnreadNotificationsByUserAsync(int userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get notifications by status
        /// </summary>
        Task<List<Notification>> GetNotificationsByStatusAsync(NotificationStatus status, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get pending notifications (to be sent)
        /// </summary>
        Task<List<Notification>> GetPendingNotificationsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get notifications by related entity
        /// </summary>
        Task<List<Notification>> GetNotificationsByRelatedEntityAsync(string entityType, int entityId, CancellationToken cancellationToken = default);
    }
}

