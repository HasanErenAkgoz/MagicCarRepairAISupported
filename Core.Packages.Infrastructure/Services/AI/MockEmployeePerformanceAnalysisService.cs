using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;

namespace MagicCarRepairAISupported.Infrastructure.Services.AI
{
    /// <summary>
    /// Mock personel performans analizi servisi (test ve geliştirme için)
    /// </summary>
    public class MockEmployeePerformanceAnalysisService : IEmployeePerformanceAnalysisService
    {
        public async Task<EmployeePerformanceAnalysisResponseDto> AnalyzeEmployeePerformanceAsync(
            EmployeePerformanceAnalysisRequestDto request,
            CancellationToken cancellationToken = default)
        {
            await Task.Delay(300, cancellationToken); // Simulate processing

            return new EmployeePerformanceAnalysisResponseDto
            {
                Summary = "Mock personel performans analizi: Basit analiz tamamlandı.",
                AnalysisDate = DateTime.UtcNow
            };
        }
    }
}
