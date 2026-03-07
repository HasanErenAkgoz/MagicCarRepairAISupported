using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Notifications.Commands.SendWhatsAppMessage
{
    public class SendWhatsAppMessageCommand : IRequest<IDataResult<SendWhatsAppMessageResponse>>
    {
        public int? UserId { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string? RelatedEntityType { get; set; }
        public int? RelatedEntityId { get; set; }
    }

    public class SendWhatsAppMessageResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
