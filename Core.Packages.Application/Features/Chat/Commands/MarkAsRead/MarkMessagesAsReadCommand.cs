using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Chat.Commands.MarkAsRead
{
    public class MarkMessagesAsReadCommand : IRequest<MarkMessagesAsReadResponse>
    {
        public int? SenderId { get; set; }
        public int? WorkOrderId { get; set; }
    }
}
