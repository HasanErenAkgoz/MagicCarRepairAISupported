using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;

namespace MagicCarRepairAISupported.Application.Common.Services.AI
{
    /// <summary>
    /// AI destekli chat servisi (Müşteri desteği chatbot'u)
    /// </summary>
    public interface IAIChatService
    {
        /// <summary>
        /// Kullanıcı mesajına yanıt verir
        /// </summary>
        Task<ChatResponseDto> GetChatResponseAsync(ChatRequestDto request, CancellationToken cancellationToken = default);
    }
}
