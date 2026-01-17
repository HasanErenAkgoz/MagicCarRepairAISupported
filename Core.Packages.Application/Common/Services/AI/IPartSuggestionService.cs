using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;

namespace MagicCarRepairAISupported.Application.Common.Services.AI
{
    /// <summary>
    /// AI destekli parça önerisi servisi
    /// </summary>
    public interface IPartSuggestionService
    {
        /// <summary>
        /// İş emri veya araç bilgisine göre parça önerir
        /// </summary>
        Task<PartSuggestionResponseDto> SuggestPartsAsync(
            PartSuggestionRequestDto request,
            CancellationToken cancellationToken = default);
    }
}
