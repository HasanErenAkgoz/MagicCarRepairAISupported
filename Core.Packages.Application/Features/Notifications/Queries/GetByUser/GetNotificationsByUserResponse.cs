using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Notifications.Queries.GetByUser
{
    public class GetNotificationsByUserResponse
    {
        public int Id { get; set; }
        public NotificationType Type { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public NotificationStatus Status { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public DateTime? SentDate { get; set; }
        public DateTime? ReadDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? RelatedEntityType { get; set; }
        public int? RelatedEntityId { get; set; }
    }
}

