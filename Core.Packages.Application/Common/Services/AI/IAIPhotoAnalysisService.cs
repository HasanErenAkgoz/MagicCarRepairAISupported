using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;

namespace MagicCarRepairAISupported.Application.Common.Services.AI
{
    /// <summary>
    /// AI destekli fotoğraf analizi servisi
    /// </summary>
    public interface IAIPhotoAnalysisService
    {
        /// <summary>
        /// Hasar fotoğrafından analiz yapar
        /// </summary>
        Task<PhotoAnalysisResultDto> AnalyzePhotoAsync(byte[] photoData, string? fileName = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Çoklu fotoğraftan analiz yapar
        /// </summary>
        Task<PhotoAnalysisResultDto> AnalyzeMultiplePhotosAsync(List<byte[]> photoDataList, CancellationToken cancellationToken = default);
    }
}

