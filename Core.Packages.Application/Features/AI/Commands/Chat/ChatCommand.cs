using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Commands.Chat
{
    public class ChatCommand : IRequest<ChatResponse>
    {
        public string Message { get; set; } = string.Empty;
        public int? CustomerId { get; set; }
        public List<ChatMessageDto>? ConversationHistory { get; set; }
        public string Language { get; set; } = "tr";
    }
}
