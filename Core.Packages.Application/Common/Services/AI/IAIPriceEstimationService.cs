using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;

namespace MagicCarRepairAISupported.Application.Common.Services.AI
{
    /// <summary>
    /// AI destekli fiyat tahmini servisi
    /// </summary>
    public interface IAIPriceEstimationService
    {
        /// <summary>
        /// İş emri için fiyat tahmini yapar
        /// </summary>
        Task<PriceEstimationResultDto> EstimatePriceAsync(PriceEstimationRequestDto request, CancellationToken cancellationToken = default);
    }
}

