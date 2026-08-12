using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IChatMessageRepository : IEntityRepository<ChatMessage, int>
    {
        /// <summary>
        /// İki kullanıcı arasındaki mesajları getirir
        /// </summary>
        Task<List<ChatMessage>> GetConversationAsync(int senderId, int receiverId, int? skip = null, int? take = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// WorkOrder'a ait mesajları getirir
        /// </summary>
        Task<List<ChatMessage>> GetMessagesByWorkOrderAsync(int workOrderId, int? skip = null, int? take = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Kullanıcının okunmamış mesaj sayısını getirir
        /// </summary>
        Task<int> GetUnreadMessageCountAsync(int userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Kullanıcının tüm konuşmalarını getirir (son mesaj ile birlikte)
        /// </summary>
        Task<List<ChatMessage>> GetUserConversationsAsync(int userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Mesajları okundu olarak işaretle
        /// </summary>
        Task MarkMessagesAsReadAsync(int userId, int? senderId = null, int? workOrderId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Kullanıcı silinmeden önce: gönderen veya alıcı olduğu tüm mesajları kaldırır (FK çakışmasını önler).
        /// </summary>
        Task DeleteAllInvolvingUserAsync(int userId, CancellationToken cancellationToken = default);
        Task CreateWithAttachmentsAsync(ChatMessage message, IReadOnlyCollection<int> attachmentIds, int ownerUserId, DateTime now, CancellationToken cancellationToken = default);
    }
}
