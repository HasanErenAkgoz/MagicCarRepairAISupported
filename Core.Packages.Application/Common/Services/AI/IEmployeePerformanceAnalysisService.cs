using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;

namespace MagicCarRepairAISupported.Application.Common.Services.AI
{
    /// <summary>
    /// AI destekli personel performans analizi servisi
    /// </summary>
    public interface IEmployeePerformanceAnalysisService
    {
        /// <summary>
        /// Personel performansını analiz eder
        /// </summary>
        Task<EmployeePerformanceAnalysisResponseDto> AnalyzeEmployeePerformanceAsync(
            EmployeePerformanceAnalysisRequestDto request,
            CancellationToken cancellationToken = default);
    }
}
