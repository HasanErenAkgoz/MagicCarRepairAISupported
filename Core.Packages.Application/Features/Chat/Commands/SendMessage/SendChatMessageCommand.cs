using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Chat.Commands.SendMessage
{
    public class SendChatMessageCommand : IRequest<SendChatMessageResponse>
    {
        public int ReceiverId { get; set; }
        public string Message { get; set; } = string.Empty;
        public ChatMessageType MessageType { get; set; } = ChatMessageType.Text;
        public int? WorkOrderId { get; set; }
        public int? CustomerId { get; set; }
        public string? FilePath { get; set; }
        public string? FileName { get; set; }
        public long? FileSize { get; set; }
    }
}
