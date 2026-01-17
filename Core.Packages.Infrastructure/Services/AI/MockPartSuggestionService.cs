using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;

namespace MagicCarRepairAISupported.Infrastructure.Services.AI
{
    /// <summary>
    /// Mock parça önerisi servisi (test ve geliştirme için)
    /// </summary>
    public class MockPartSuggestionService : IPartSuggestionService
    {
        public async Task<PartSuggestionResponseDto> SuggestPartsAsync(
            PartSuggestionRequestDto request,
            CancellationToken cancellationToken = default)
        {
            await Task.Delay(300, cancellationToken); // Simulate processing

            return new PartSuggestionResponseDto
            {
                Explanation = "Mock parça önerisi: Basit öneriler oluşturuldu.",
                AnalysisDate = DateTime.UtcNow
            };
        }
    }
}
