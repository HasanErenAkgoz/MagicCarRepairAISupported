using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;

namespace MagicCarRepairAISupported.Application.Common.Services.AI
{
    /// <summary>
    /// AI destekli bakım önerileri servisi
    /// </summary>
    public interface IAIMaintenanceService
    {
        /// <summary>
        /// Araç için bakım önerileri getirir
        /// </summary>
        Task<List<MaintenanceSuggestionDto>> GetMaintenanceSuggestionsAsync(int vehicleId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Parça ömrü tahmini yapar
        /// </summary>
        Task<PartLifespanEstimateDto> EstimatePartLifespanAsync(int partId, int vehicleId, CancellationToken cancellationToken = default);
    }
}

