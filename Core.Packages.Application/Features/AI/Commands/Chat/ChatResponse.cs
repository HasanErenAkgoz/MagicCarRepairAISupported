namespace MagicCarRepairAISupported.Application.Features.AI.Commands.Chat
{
    public class ChatResponse
    {
        public string Response { get; set; } = string.Empty;
        public string ResponseType { get; set; } = "General";
        public int? RelatedEntityId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public Dictionary<string, object>? AdditionalData { get; set; }
        public List<string>? SuggestedActions { get; set; }
    }
}
