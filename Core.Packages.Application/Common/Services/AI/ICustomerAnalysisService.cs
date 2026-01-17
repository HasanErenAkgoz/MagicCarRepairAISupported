using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;

namespace MagicCarRepairAISupported.Application.Common.Services.AI
{
    /// <summary>
    /// AI destekli müşteri analizi servisi
    /// </summary>
    public interface ICustomerAnalysisService
    {
        /// <summary>
        /// Müşteri davranışını ve değerini analiz eder
        /// </summary>
        Task<CustomerAnalysisResponseDto> AnalyzeCustomersAsync(
            CustomerAnalysisRequestDto request,
            CancellationToken cancellationToken = default);
    }
}
