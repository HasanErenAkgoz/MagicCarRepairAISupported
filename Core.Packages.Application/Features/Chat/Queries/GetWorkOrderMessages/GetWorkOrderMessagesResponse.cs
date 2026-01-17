using MagicCarRepairAISupported.Application.Features.Chat.Queries.GetConversation;

namespace MagicCarRepairAISupported.Application.Features.Chat.Queries.GetWorkOrderMessages
{
    public class GetWorkOrderMessagesResponse
    {
        public List<ChatMessageDto> Messages { get; set; } = new();
        public int TotalCount { get; set; }
    }
}
