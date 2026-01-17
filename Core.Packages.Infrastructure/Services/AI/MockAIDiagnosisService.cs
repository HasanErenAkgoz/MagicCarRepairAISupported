using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Infrastructure.Services.AI
{
    /// <summary>
    /// Mock AI arıza tespiti servisi
    /// Gerçek implementasyon OpenAI/Azure OpenAI ile yapılabilir
    /// </summary>
    public class MockAIDiagnosisService : IAIDiagnosisService
    {
        private readonly ILogger<MockAIDiagnosisService> _logger;

        public MockAIDiagnosisService(ILogger<MockAIDiagnosisService> logger)
        {
            _logger = logger;
        }

        public async Task<DiagnosisResultDto> DiagnoseFromTextAsync(string complaint, int? vehicleId = null, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("AI Diagnosis: Analyzing complaint text. VehicleId: {VehicleId}", vehicleId);

            // Mock implementation - Gerçek implementasyonda OpenAI/Azure OpenAI kullanılabilir
            await Task.Delay(500, cancellationToken); // Simüle edilmiş API çağrısı

            var result = new DiagnosisResultDto
            {
                ConfidenceScore = 75,
                EstimatedDays = 3,
                EstimatedCost = 2500m,
                Recommendations = "Arıza tespiti için detaylı muayene önerilir.",
                PossibleIssues = new List<DiagnosisItemDto>
                {
                    new DiagnosisItemDto
                    {
                        IssueName = "Motor Arızası",
                        Description = complaint.Contains("motor", StringComparison.OrdinalIgnoreCase) 
                            ? "Motor ile ilgili bir sorun olabilir" 
                            : "Olası motor arızası",
                        ProbabilityScore = 70,
                        Category = "Motor"
                    },
                    new DiagnosisItemDto
                    {
                        IssueName = "Elektrik Arızası",
                        Description = "Elektrik sistemi ile ilgili bir sorun olabilir",
                        ProbabilityScore = 50,
                        Category = "Elektrik"
                    }
                },
                RecommendedParts = new List<RecommendedPartDto>
                {
                    new RecommendedPartDto
                    {
                        PartName = "Benzin Pompası",
                        Category = "Yakıt Sistemi",
                        EstimatedPrice = 800m,
                        Quantity = 1,
                        ProbabilityScore = 60
                    }
                },
                RecommendedLabors = new List<RecommendedLaborDto>
                {
                    new RecommendedLaborDto
                    {
                        LaborName = "Arıza Tespiti",
                        Description = "Detaylı arıza tespiti ve muayene",
                        EstimatedPrice = 200m,
                        EstimatedHours = 2,
                        ProbabilityScore = 100
                    },
                    new RecommendedLaborDto
                    {
                        LaborName = "Parça Değişimi",
                        Description = "Gerekli parçaların değiştirilmesi",
                        EstimatedPrice = 500m,
                        EstimatedHours = 4,
                        ProbabilityScore = 80
                    }
                }
            };

            return result;
        }

        public async Task<DiagnosisResultDto> DiagnoseFromVoiceAsync(byte[] audioData, int? vehicleId = null, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("AI Diagnosis: Analyzing voice complaint. VehicleId: {VehicleId}, AudioSize: {AudioSize} bytes", vehicleId, audioData.Length);

            // Mock implementation - Gerçek implementasyonda ses-to-text + text analizi yapılabilir
            await Task.Delay(1000, cancellationToken); // Simüle edilmiş API çağrısı

            // Şimdilik text analizi ile aynı sonucu döndür
            return await DiagnoseFromTextAsync("Sesli şikayet analizi (henüz implement edilmedi)", vehicleId, cancellationToken);
        }
    }
}

