namespace MagicCarRepairAISupported.Application.Common.Services.AI.Dtos
{
    /// <summary>
    /// Chat yanıtı DTO
    /// </summary>
    public class ChatResponseDto
    {
        /// <summary>
        /// AI yanıtı
        /// </summary>
        public string Response { get; set; } = string.Empty;

        /// <summary>
        /// Yanıt tipi (General, WorkOrderStatus, Appointment, etc.)
        /// </summary>
        public string ResponseType { get; set; } = "General";

        /// <summary>
        /// İlgili entity ID (örneğin WorkOrder ID)
        /// </summary>
        public int? RelatedEntityId { get; set; }

        /// <summary>
        /// Yanıt zamanı
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// İlave bilgiler (JSON formatında)
        /// </summary>
        public Dictionary<string, object>? AdditionalData { get; set; }

        /// <summary>
        /// Önerilen aksiyonlar (örn: "İş emri detaylarını göster", "Randevu al")
        /// </summary>
        public List<string>? SuggestedActions { get; set; }
    }
}
