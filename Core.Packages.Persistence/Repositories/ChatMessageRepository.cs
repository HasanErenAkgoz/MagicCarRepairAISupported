using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class ChatMessageRepository : EfEntityRepository<ChatMessage, BaseDbContext>, IChatMessageRepository
    {
        public ChatMessageRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<List<ChatMessage>> GetConversationAsync(int senderId, int receiverId, int? skip = null, int? take = null, CancellationToken cancellationToken = default)
        {
            IQueryable<ChatMessage> query = Context.Set<ChatMessage>()
                .Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) ||
                           (m.SenderId == receiverId && m.ReceiverId == senderId))
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .OrderByDescending(m => m.SentDate);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<List<ChatMessage>> GetMessagesByWorkOrderAsync(int workOrderId, int? skip = null, int? take = null, CancellationToken cancellationToken = default)
        {
            IQueryable<ChatMessage> query = Context.Set<ChatMessage>()
                .Where(m => m.WorkOrderId == workOrderId)
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .OrderByDescending(m => m.SentDate);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<int> GetUnreadMessageCountAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<ChatMessage>()
                .CountAsync(m => m.ReceiverId == userId && !m.IsRead, cancellationToken);
        }

        public async Task<List<ChatMessage>> GetUserConversationsAsync(int userId, CancellationToken cancellationToken = default)
        {
            // Her konuşma için son mesajı getir
            var conversations = await Context.Set<ChatMessage>()
                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .GroupBy(m => m.SenderId == userId ? m.ReceiverId : m.SenderId)
                .Select(g => g.OrderByDescending(m => m.SentDate).First())
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .OrderByDescending(m => m.SentDate)
                .ToListAsync(cancellationToken);

            return conversations;
        }

        public async Task MarkMessagesAsReadAsync(int userId, int? senderId = null, int? workOrderId = null, CancellationToken cancellationToken = default)
        {
            var query = Context.Set<ChatMessage>()
                .Where(m => m.ReceiverId == userId && !m.IsRead);

            if (senderId.HasValue)
                query = query.Where(m => m.SenderId == senderId.Value);

            if (workOrderId.HasValue)
                query = query.Where(m => m.WorkOrderId == workOrderId.Value);

            var messages = await query.ToListAsync(cancellationToken);
            foreach (var message in messages)
            {
                message.MarkAsRead();
            }

            await Context.SaveChangesAsync();
        }

        public async Task DeleteAllInvolvingUserAsync(int userId, CancellationToken cancellationToken = default)
        {
            await Context.Set<ChatMessage>()
                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .ExecuteDeleteAsync(cancellationToken);
        }

        public async Task CreateWithAttachmentsAsync(ChatMessage message, IReadOnlyCollection<int> attachmentIds, int ownerUserId, DateTime now, CancellationToken cancellationToken = default)
        {
            await using var transaction = await Context.Database.BeginTransactionAsync(cancellationToken);
            var attachments = await Context.Set<ChatAttachment>()
                .Where(x => attachmentIds.Contains(x.Id) && x.ClientId == message.ClientId && x.WorkOrderId == message.WorkOrderId && x.OwnerUserId == ownerUserId && x.ChatMessageId == null)
                .ToListAsync(cancellationToken);
            if (attachments.Count != attachmentIds.Count || attachments.Any(x => x.ExpiresAt <= now))
                throw new Domain.Exceptions.DomainException("CHAT_ATTACHMENT_ACCESS_DENIED");

            await Context.Set<ChatMessage>().AddAsync(message, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
            foreach (var attachment in attachments) attachment.ChatMessageId = message.Id;
            await Context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }

    }
}
