using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Chat.Queries.GetConversation
{
    public class GetConversationResponse
    {
        public List<ChatMessageDto> Messages { get; set; } = new();
        public int TotalCount { get; set; }
    }

    public class ChatMessageDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string? SenderName { get; set; }
        public int? ReceiverId { get; set; }
        public string? ReceiverName { get; set; }
        public string Message { get; set; } = string.Empty;
        public ChatMessageType MessageType { get; set; }
        public int? WorkOrderId { get; set; }
        public int? CustomerId { get; set; }
        public string? FilePath { get; set; }
        public string? FileName { get; set; }
        public long? FileSize { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadDate { get; set; }
        public DateTime SentDate { get; set; }
    }
}
