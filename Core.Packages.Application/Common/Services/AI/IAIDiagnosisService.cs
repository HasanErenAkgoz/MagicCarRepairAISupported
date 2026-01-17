using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;

namespace MagicCarRepairAISupported.Application.Common.Services.AI
{
    /// <summary>
    /// AI destekli arıza tespiti servisi
    /// </summary>
    public interface IAIDiagnosisService
    {
        /// <summary>
        /// Müşteri şikayetinden arıza tespiti yapar
        /// </summary>
        Task<DiagnosisResultDto> DiagnoseFromTextAsync(string complaint, int? vehicleId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sesli şikayetten arıza tespiti yapar (opsiyonel)
        /// </summary>
        Task<DiagnosisResultDto> DiagnoseFromVoiceAsync(byte[] audioData, int? vehicleId = null, CancellationToken cancellationToken = default);
    }
}

