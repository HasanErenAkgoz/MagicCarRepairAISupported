using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Notifications.Commands.SendPushNotification
{
    public class SendPushNotificationCommand : IRequest<IDataResult<SendPushNotificationResponse>>
    {
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public Dictionary<string, object>? Data { get; set; }
        public string? RelatedEntityType { get; set; }
        public int? RelatedEntityId { get; set; }
    }

    public class SendPushNotificationResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
