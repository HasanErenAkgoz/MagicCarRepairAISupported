using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Infrastructure.Services.AI
{
    /// <summary>
    /// Mock AI bakım önerileri servisi
    /// Gerçek implementasyon ML model veya araç bakım veritabanlarıyla yapılabilir
    /// </summary>
    public class MockAIMaintenanceService : IAIMaintenanceService
    {
        private readonly ILogger<MockAIMaintenanceService> _logger;

        public MockAIMaintenanceService(ILogger<MockAIMaintenanceService> logger)
        {
            _logger = logger;
        }

        public async Task<List<MaintenanceSuggestionDto>> GetMaintenanceSuggestionsAsync(int vehicleId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("AI Maintenance: Getting suggestions for Vehicle {VehicleId}", vehicleId);

            // Mock implementation - Gerçek implementasyonda araç bilgileri ve km analizi yapılabilir
            await Task.Delay(500, cancellationToken); // Simüle edilmiş API çağrısı

            var suggestions = new List<MaintenanceSuggestionDto>
            {
                new MaintenanceSuggestionDto
                {
                    MaintenanceType = "Periyodik Bakım",
                    Description = "30.000 km periyodik bakım zamanı yaklaşıyor",
                    RecommendedParts = "Motor Yağı, Yağ Filtresi, Hava Filtresi",
                    RecommendedKilometers = 30000,
                    RecommendedDate = DateTime.UtcNow.AddMonths(2),
                    UrgencyLevel = 3,
                    Reason = "Araç kilometre sayacına göre periyodik bakım zamanı yaklaşıyor",
                    EstimatedCost = 800m
                },
                new MaintenanceSuggestionDto
                {
                    MaintenanceType = "Önleyici Bakım",
                    Description = "Fren sisteminin kontrol edilmesi önerilir",
                    RecommendedParts = "Fren Balataları (Kontrol)",
                    RecommendedKilometers = null,
                    RecommendedDate = DateTime.UtcNow.AddDays(30),
                    UrgencyLevel = 2,
                    Reason = "Fren balataları normalden daha hızlı aşınıyor",
                    EstimatedCost = 400m
                },
                new MaintenanceSuggestionDto
                {
                    MaintenanceType = "Periyodik Bakım",
                    Description = "Lastik rotasyonu ve kontrolü",
                    RecommendedParts = "Lastik Kontrolü",
                    RecommendedKilometers = null,
                    RecommendedDate = DateTime.UtcNow.AddDays(60),
                    UrgencyLevel = 2,
                    Reason = "Düzenli lastik bakımı yakıt tasarrufu ve güvenlik için önemli",
                    EstimatedCost = 200m
                }
            };

            return suggestions;
        }

        public async Task<PartLifespanEstimateDto> EstimatePartLifespanAsync(int partId, int vehicleId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("AI Maintenance: Estimating part lifespan for Part {PartId} in Vehicle {VehicleId}", partId, vehicleId);

            // Mock implementation - Gerçek implementasyonda parça kullanım geçmişi ve araç bilgileri analiz edilir
            await Task.Delay(400, cancellationToken); // Simüle edilmiş API çağrısı

            var result = new PartLifespanEstimateDto
            {
                PartId = partId,
                PartName = "Motor Yağı Filtresi",
                EstimatedRemainingKilometers = 15000,
                EstimatedRemainingMonths = 8,
                EstimatedReplacementDate = DateTime.UtcNow.AddMonths(8),
                ConditionScore = 65,
                Recommendations = "Motor yağı filtresi normal kullanım koşullarında yaklaşık 8 ay sonra değiştirilmesi önerilir."
            };

            return result;
        }
    }
}

