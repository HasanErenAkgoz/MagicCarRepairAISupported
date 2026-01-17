using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;

namespace MagicCarRepairAISupported.Infrastructure.Services.AI
{
    /// <summary>
    /// Mock AI Chat servisi (test ve geliştirme için)
    /// </summary>
    public class MockAIChatService : IAIChatService
    {
        public async Task<ChatResponseDto> GetChatResponseAsync(ChatRequestDto request, CancellationToken cancellationToken = default)
        {
            await Task.Delay(500, cancellationToken); // Simulate API delay

            var message = request.Message.ToLower();

            string response;
            string responseType;

            if (message.Contains("iş emri") || message.Contains("work order") || message.Contains("durum"))
            {
                responseType = "WorkOrderStatus";
                response = request.Language == "tr"
                    ? "İş emri durumunu öğrenmek için lütfen iş emri numaranızı paylaşın."
                    : "To check your work order status, please provide your work order number.";
            }
            else if (message.Contains("randevu") || message.Contains("appointment") || message.Contains("tarih"))
            {
                responseType = "Appointment";
                response = request.Language == "tr"
                    ? "Randevu almak için lütfen tercih ettiğiniz tarih ve saat aralığını belirtin."
                    : "To schedule an appointment, please specify your preferred date and time.";
            }
            else
            {
                responseType = "General";
                response = request.Language == "tr"
                    ? "Size nasıl yardımcı olabilirim? İş emri durumu sorgulama, randevu alma gibi konularda yardımcı olabilirim."
                    : "How can I help you? I can assist you with work order status, appointments, and general questions.";
            }

            return new ChatResponseDto
            {
                Response = response,
                ResponseType = responseType,
                Timestamp = DateTime.UtcNow,
                SuggestedActions = GetSuggestedActions(responseType)
            };
        }

        private List<string> GetSuggestedActions(string responseType)
        {
            return responseType switch
            {
                "WorkOrderStatus" => new List<string> { "İş emri durumu sorgula", "İş emri detaylarını göster" },
                "Appointment" => new List<string> { "Randevu al", "Randevularımı görüntüle" },
                _ => new List<string> { "Yardım al", "İletişim bilgileri" }
            };
        }
    }
}
