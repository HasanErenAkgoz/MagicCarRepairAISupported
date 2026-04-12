using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;
using System.Net.Http;
using Microsoft.Extensions.Http;

namespace MagicCarRepairAISupported.Application.Features.AI.Commands.AnalyzeDamagePhotos
{
    public class AnalyzeDamagePhotosCommandHandler : IRequestHandler<AnalyzeDamagePhotosCommand, IDataResult<AnalyzeDamagePhotosResponse>>
    {
        private readonly IAIPhotoAnalysisService _aiPhotoAnalysisService;
        private readonly HttpClient _httpClient;

        public AnalyzeDamagePhotosCommandHandler(
            IAIPhotoAnalysisService aiPhotoAnalysisService,
            IHttpClientFactory httpClientFactory)
        {
            _aiPhotoAnalysisService = aiPhotoAnalysisService;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IDataResult<AnalyzeDamagePhotosResponse>> Handle(AnalyzeDamagePhotosCommand request, CancellationToken cancellationToken)
        {
            if (request.PhotoPaths == null || request.PhotoPaths.Count == 0)
            {
                return new ErrorDataResult<AnalyzeDamagePhotosResponse>("En az bir fotoğraf gerekli.");
            }

            try
            {
                // Fotoğrafları byte array'e çevir
                var photoDataList = new List<byte[]>();
                foreach (var photoPath in request.PhotoPaths)
                {
                    try
                    {
                        // Eğer URL ise indir, değilse dosya yolundan oku
                        byte[] photoData;
                        if (photoPath.StartsWith("http://") || photoPath.StartsWith("https://"))
                        {
                            photoData = await _httpClient.GetByteArrayAsync(photoPath, cancellationToken);
                        }
                        else
                        {
                            // Local file path - bu kısım backend'de çalışmayabilir, frontend'den base64 gönderilmeli
                            photoData = await File.ReadAllBytesAsync(photoPath, cancellationToken);
                        }
                        photoDataList.Add(photoData);
                    }
                    catch (Exception ex)
                    {
                        // Fotoğraf yüklenemezse atla
                        continue;
                    }
                }

                if (photoDataList.Count == 0)
                {
                    return new ErrorDataResult<AnalyzeDamagePhotosResponse>("Fotoğraflar yüklenemedi.");
                }

                // AI servisini kullanarak fotoğraf analizi yap
                var analysisResult = await _aiPhotoAnalysisService.AnalyzeMultiplePhotosAsync(
                    photoDataList, request.Language, cancellationToken);

                if (analysisResult == null)
                {
                    return new ErrorDataResult<AnalyzeDamagePhotosResponse>("AI analizi başarısız oldu.");
                }

                // PhotoAnalysisResultDto'dan AnalyzeDamagePhotosResponse'a çevir
                var totalCost = analysisResult.RecommendedParts?.Sum(p => (p.EstimatedPrice ?? 0) * p.Quantity) ?? 0;
                totalCost += analysisResult.RecommendedLabors?.Sum(l => l.EstimatedPrice ?? 0) ?? 0;

                var partsNeeded = analysisResult.RecommendedParts?
                    .Select(p => $"{p.PartName} ({p.Quantity} adet)")
                    .ToList() ?? new List<string>();

                var damageDescription = analysisResult.DetectedDamages?
                    .Select(d => $"{d.DamageType} - {d.Location}: {d.Description}")
                    .Aggregate((a, b) => $"{a}\n{b}") ?? "Hasar tespit edildi.";

                var detailedAnalysis = $"Hasar Şiddet Skoru: {analysisResult.DamageSeverityScore}/100\n" +
                    $"Fotoğraf Kalite Skoru: {analysisResult.PhotoQualityScore}/100\n" +
                    $"{analysisResult.Recommendations ?? ""}";

                var response = new AnalyzeDamagePhotosResponse
                {
                    EstimatedCost = totalCost > 0 ? totalCost : (analysisResult.DetectedDamages?.Sum(d => d.EstimatedRepairCost) ?? 0),
                    DamageDescription = damageDescription,
                    PartsNeeded = partsNeeded,
                    DetailedAnalysis = detailedAnalysis,
                    ConfidenceScore = analysisResult.PhotoQualityScore
                };

                return new SuccessDataResult<AnalyzeDamagePhotosResponse>(response, "Fotoğraf analizi başarıyla tamamlandı.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<AnalyzeDamagePhotosResponse>($"AI analizi sırasında hata oluştu: {ex.Message}");
            }
        }
    }
}
