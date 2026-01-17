using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class NotificationRepository : EfEntityRepository<Notification, BaseDbContext>, INotificationRepository
    {
        public NotificationRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public new async Task<Notification?> GetByIdAsync(int id)
        {
            return await Context.Set<Notification>()
                .Include(n => n.User)
                .Include(n => n.Client)
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<List<Notification>> GetNotificationsByUserAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Notification>()
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Notification>> GetUnreadNotificationsByUserAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Notification>()
                .Where(n => n.UserId == userId && 
                           (n.Status == NotificationStatus.Sent || n.Status == NotificationStatus.Pending))
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Notification>> GetNotificationsByStatusAsync(NotificationStatus status, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Notification>()
                .Where(n => n.Status == status)
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Notification>> GetPendingNotificationsAsync(CancellationToken cancellationToken = default)
        {
            return await Context.Set<Notification>()
                .Where(n => n.Status == NotificationStatus.Pending)
                .OrderBy(n => n.CreatedDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Notification>> GetNotificationsByRelatedEntityAsync(string entityType, int entityId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Notification>()
                .Where(n => n.RelatedEntityType == entityType && n.RelatedEntityId == entityId)
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync(cancellationToken);
        }
    }
}

