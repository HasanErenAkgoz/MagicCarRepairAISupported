namespace MagicCarRepairAISupported.Application.Common.Services.AI.Dtos
{
    /// <summary>
    /// Chat isteği DTO
    /// </summary>
    public class ChatRequestDto
    {
        /// <summary>
        /// Kullanıcı mesajı
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Müşteri ID (opsiyonel - müşteri bilgilerine erişim için)
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Konuşma geçmişi (opsiyonel - context için)
        /// </summary>
        public List<ChatMessageDto>? ConversationHistory { get; set; }

        /// <summary>
        /// Dil kodu (tr, en, ar)
        /// </summary>
        public string Language { get; set; } = "tr";
    }

    /// <summary>
    /// Chat mesajı DTO
    /// </summary>
    public class ChatMessageDto
    {
        public string Role { get; set; } = string.Empty; // "user" or "assistant"
        public string Content { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
