using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Infrastructure.Services.AI
{
    /// <summary>
    /// Mock AI fotoğraf analizi servisi
    /// Gerçek implementasyon Azure Computer Vision veya benzeri servislerle yapılabilir
    /// </summary>
    public class MockAIPhotoAnalysisService : IAIPhotoAnalysisService
    {
        private readonly ILogger<MockAIPhotoAnalysisService> _logger;

        public MockAIPhotoAnalysisService(ILogger<MockAIPhotoAnalysisService> logger)
        {
            _logger = logger;
        }

        public async Task<PhotoAnalysisResultDto> AnalyzePhotoAsync(byte[] photoData, string? fileName = null, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("AI Photo Analysis: Analyzing photo. FileName: {FileName}, Size: {Size} bytes", fileName, photoData.Length);

            // Mock implementation - Gerçek implementasyonda Azure Computer Vision veya benzeri kullanılabilir
            await Task.Delay(800, cancellationToken); // Simüle edilmiş API çağrısı

            var result = new PhotoAnalysisResultDto
            {
                PhotoQualityScore = 85,
                PhotoQualityNotes = "Fotoğraf kalitesi iyi. Hasar tespiti için yeterli detay mevcut.",
                DamageSeverityScore = 60,
                Recommendations = "Detaylı muayene önerilir. Sigorta hasarı olabilir.",
                DetectedDamages = new List<DamageDetectionDto>
                {
                    new DamageDetectionDto
                    {
                        DamageType = "Çizik",
                        Location = "Ön Kaput",
                        SeverityScore = 40,
                        EstimatedRepairCost = 500m,
                        Description = "Ön kaput üzerinde orta şiddette çizik tespit edildi.",
                        Coordinates = new List<CoordinateDto>
                        {
                            new CoordinateDto { X = 100, Y = 150 },
                            new CoordinateDto { X = 200, Y = 150 },
                            new CoordinateDto { X = 200, Y = 250 },
                            new CoordinateDto { X = 100, Y = 250 }
                        }
                    },
                    new DamageDetectionDto
                    {
                        DamageType = "Çökme",
                        Location = "Ön Tampon",
                        SeverityScore = 70,
                        EstimatedRepairCost = 1500m,
                        Description = "Ön tampon üzerinde çökme tespit edildi.",
                        Coordinates = new List<CoordinateDto>
                        {
                            new CoordinateDto { X = 300, Y = 400 },
                            new CoordinateDto { X = 450, Y = 400 },
                            new CoordinateDto { X = 450, Y = 500 },
                            new CoordinateDto { X = 300, Y = 500 }
                        }
                    }
                },
                RecommendedParts = new List<RecommendedPartDto>
                {
                    new RecommendedPartDto
                    {
                        PartName = "Ön Tampon",
                        Category = "Karoseri",
                        EstimatedPrice = 1200m,
                        Quantity = 1,
                        ProbabilityScore = 90
                    },
                    new RecommendedPartDto
                    {
                        PartName = "Kaput Boyası",
                        Category = "Boyama",
                        EstimatedPrice = 300m,
                        Quantity = 1,
                        ProbabilityScore = 70
                    }
                },
                RecommendedLabors = new List<RecommendedLaborDto>
                {
                    new RecommendedLaborDto
                    {
                        LaborName = "Karoseri Onarımı",
                        Description = "Tampon değişimi ve kaput boyama",
                        EstimatedPrice = 800m,
                        EstimatedHours = 6,
                        ProbabilityScore = 85
                    }
                },
                InsuranceReportJson = System.Text.Json.JsonSerializer.Serialize(new
                {
                    DamageType = "Çökme ve Çizik",
                    Severity = "Orta",
                    EstimatedCost = 2800m,
                    RequiresInspection = true
                })
            };

            return result;
        }

        public async Task<PhotoAnalysisResultDto> AnalyzeMultiplePhotosAsync(List<byte[]> photoDataList, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("AI Photo Analysis: Analyzing {Count} photos", photoDataList.Count);

            // Mock implementation - Gerçek implementasyonda tüm fotoğraflar birlikte analiz edilir
            await Task.Delay(1000 * photoDataList.Count, cancellationToken); // Simüle edilmiş API çağrısı

            // Şimdilik ilk fotoğrafın analizi ile başlayalım
            if (photoDataList.Count == 0)
            {
                throw new ArgumentException("En az bir fotoğraf gerekli", nameof(photoDataList));
            }

            var firstPhotoResult = await AnalyzePhotoAsync(photoDataList[0], null, cancellationToken);

            // Birden fazla fotoğraf varsa, hasarları birleştir
            if (photoDataList.Count > 1)
            {
                firstPhotoResult.Recommendations = $"{firstPhotoResult.Recommendations} Toplam {photoDataList.Count} fotoğraf analiz edildi.";
            }

            return firstPhotoResult;
        }
    }
}

