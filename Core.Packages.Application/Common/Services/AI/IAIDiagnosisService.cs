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
        Task<DiagnosisResultDto> DiagnoseFromTextAsync(string complaint, int? vehicleId = null, List<string>? photoUrls = null, string language = "tr", CancellationToken cancellationToken = default);

        /// <summary>
        /// Sesli şikayetten arıza tespiti yapar (opsiyonel)
        /// </summary>
        Task<DiagnosisResultDto> DiagnoseFromVoiceAsync(byte[] audioData, int? vehicleId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Servis yeri için AI destekli benzersiz açıklama üretir
        /// </summary>
        Task<GenerateDescriptionResultDto> GenerateShopDescriptionAsync(string shopName, string? address = null, string? phone = null, CancellationToken cancellationToken = default);
    }
}

