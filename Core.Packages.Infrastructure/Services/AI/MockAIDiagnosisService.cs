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

        public async Task<DiagnosisResultDto> DiagnoseFromTextAsync(string complaint, int? vehicleId = null, List<DiagnosisImage>? images = null, string language = "tr", CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("AI Diagnosis: Analyzing complaint text. VehicleId: {VehicleId}", vehicleId);

            await Task.Delay(500, cancellationToken);

            var lower = complaint.ToLowerInvariant();
            string[] accidentKeywords = ["kaza", "çarptı", "çarpma", "hasar", "göçük", "kaporta", "ezik", "darbe", "dent", "crash", "accident"];
            bool isAccident = accidentKeywords.Any(k => lower.Contains(k));

            if (isAccident)
            {
                var accident = new DiagnosisResultDto
                {
                    DiagnosisType = DiagnosisType.Accident,
                    ConfidenceScore = 82,
                    EstimatedDays = 7,
                    EstimatedCost = 12000m,
                    Recommendations = "Kaza hasarı tespit edildi. Gizli hasar riski nedeniyle şase ve güvenlik sistemleri mutlaka kontrol edilmelidir.",
                    PossibleIssues = new List<DiagnosisItemDto>
                    {
                        new DiagnosisItemDto { IssueName = "Ön Kaporta Hasarı", Description = "Ön tampon ve kaput bölgesinde kaza hasarı", ProbabilityScore = 92, Category = "Kaporta" },
                        new DiagnosisItemDto { IssueName = "Şase Hasarı Riski", Description = "Darbe şiddetine göre şase bütünlüğü tehlikte olabilir", ProbabilityScore = 65, Category = "Şase" }
                    },
                    RecommendedParts = new List<RecommendedPartDto>
                    {
                        new RecommendedPartDto { PartName = "Ön Tampon", Category = "Kaporta", EstimatedPrice = 3500m, Quantity = 1, ProbabilityScore = 95, IsInDatabase = false, IsInStock = false },
                        new RecommendedPartDto { PartName = "Kaput", Category = "Kaporta", EstimatedPrice = 5000m, Quantity = 1, ProbabilityScore = 80, IsInDatabase = false, IsInStock = false }
                    },
                    RecommendedLabors = new List<RecommendedLaborDto>
                    {
                        new RecommendedLaborDto { LaborName = "Kaporta Düzeltme ve Boya", Description = "Hasarlı panellerin düzeltilmesi ve boyanması", EstimatedPrice = 4000m, EstimatedHours = 12, ProbabilityScore = 90 },
                        new RecommendedLaborDto { LaborName = "Şase Ölçümü", Description = "Şase bütünlüğü ve geometri kontrolü", EstimatedPrice = 500m, EstimatedHours = 1, ProbabilityScore = 85 }
                    },
                    DamagedParts = new List<DamagedPartDto>
                    {
                        new DamagedPartDto { PartName = "Ön Tampon", DamageLevel = DamageLevel.Heavy, RecommendedAction = RepairAction.Replace, ConfidencePercent = 95, EstimatedCostMin = 2000m, EstimatedCostMax = 4500m, Notes = "Kırık ve deformasyonlu, değişim gerekli" },
                        new DamagedPartDto { PartName = "Kaput", DamageLevel = DamageLevel.Medium, RecommendedAction = RepairAction.Repair, ConfidencePercent = 85, EstimatedCostMin = 1500m, EstimatedCostMax = 3500m, Notes = "Göçük mevcut, düzeltme ve boya yapılabilir" },
                        new DamagedPartDto { PartName = "Sol Ön Çamurluk", DamageLevel = DamageLevel.Light, RecommendedAction = RepairAction.Paint, ConfidencePercent = 70, EstimatedCostMin = 500m, EstimatedCostMax = 1500m },
                        new DamagedPartDto { PartName = "Ön Sol Far", DamageLevel = DamageLevel.Heavy, RecommendedAction = RepairAction.Replace, ConfidencePercent = 90, EstimatedCostMin = 1200m, EstimatedCostMax = 3000m, Notes = "Kırık, değişim gerekli" }
                    },
                    CriticalChecks = new List<CriticalCheckDto>
                    {
                        new CriticalCheckDto { ComponentName = "Radyatör", RiskLevel = RiskLevel.High, Warning = "Ön darbe radyatörü etkilemiş olabilir, soğutma sistemi kontrol edilmeli", RequiresImmediateInspection = true },
                        new CriticalCheckDto { ComponentName = "Şase/Longeron", RiskLevel = RiskLevel.Medium, Warning = "Darbe şiddetine göre şase bütünlüğü kontrol edilmeli", RequiresImmediateInspection = false },
                        new CriticalCheckDto { ComponentName = "Hava Yastığı ECU", RiskLevel = RiskLevel.Critical, Warning = "Kaza sonrası hava yastığı sistemi ve ECU mutlaka kontrol edilmeli", RequiresImmediateInspection = true }
                    },
                    EstimatedRepairRange = new EstimatedRepairRangeDto { Min = 5700m, Max = 12500m, Currency = "TRY" }
                };
                accident.MobileDisplayMarkdown = AccidentMobileMarkdownFormatter.BuildFallback(accident);
                return accident;
            }

            var result = new DiagnosisResultDto
            {
                DiagnosisType = DiagnosisType.Mechanical,
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
                        ProbabilityScore = 60,
                        IsInDatabase = false,
                        IsInStock = false
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
            return await DiagnoseFromTextAsync("Sesli şikayet analizi (henüz implement edilmedi)", vehicleId, null, "tr", cancellationToken);
        }

        public Task<GenerateDescriptionResultDto> GenerateShopDescriptionAsync(string shopName, string? address = null, string? phone = null, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Mock AI GenerateDescription: shopName={ShopName}", shopName);

            var description = $"{shopName}, müşteri memnuniyetini ön planda tutan, uzman kadrosuyla kaliteli oto servis hizmeti sunan güvenilir bir tamir ve bakım merkezidir.";

            return Task.FromResult(new GenerateDescriptionResultDto { Description = description });
        }
    }
}
