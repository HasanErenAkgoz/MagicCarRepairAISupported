using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;

namespace MagicCarRepairAISupported.Infrastructure.Services.AI
{
    /// <summary>
    /// Mock müşteri analizi servisi (test ve geliştirme için)
    /// </summary>
    public class MockCustomerAnalysisService : ICustomerAnalysisService
    {
        public async Task<CustomerAnalysisResponseDto> AnalyzeCustomersAsync(
            CustomerAnalysisRequestDto request,
            CancellationToken cancellationToken = default)
        {
            await Task.Delay(300, cancellationToken); // Simulate processing

            return new CustomerAnalysisResponseDto
            {
                Summary = "Mock müşteri analizi: Basit analiz tamamlandı.",
                AnalysisDate = DateTime.UtcNow
            };
        }
    }
}
