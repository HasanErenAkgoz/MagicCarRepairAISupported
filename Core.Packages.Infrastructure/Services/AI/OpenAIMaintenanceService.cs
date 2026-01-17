using Azure;
using Azure.AI.OpenAI;
using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Infrastructure.Configurations.AI;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace MagicCarRepairAISupported.Infrastructure.Services.AI
{
    /// <summary>
    /// OpenAI destekli bakım önerileri servisi
    /// </summary>
    public class OpenAIMaintenanceService : IAIMaintenanceService
    {
        private readonly ILogger<OpenAIMaintenanceService> _logger;
        private readonly AIOptions _aiOptions;
        private readonly OpenAIClient? _openAIClient;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IPartRepository _partRepository;

        public OpenAIMaintenanceService(
            ILogger<OpenAIMaintenanceService> logger,
            IOptions<AIOptions> aiOptions,
            IVehicleRepository vehicleRepository,
            IWorkOrderRepository workOrderRepository,
            IPartRepository partRepository)
        {
            _logger = logger;
            _aiOptions = aiOptions.Value;
            _vehicleRepository = vehicleRepository;
            _workOrderRepository = workOrderRepository;
            _partRepository = partRepository;

            // Initialize OpenAI client
            try
            {
                if (_aiOptions.Provider == "AzureOpenAI" && !string.IsNullOrEmpty(_aiOptions.AzureEndpoint))
                {
                    _openAIClient = new OpenAIClient(
                        new Uri(_aiOptions.AzureEndpoint!),
                        new AzureKeyCredential(_aiOptions.ApiKey ?? throw new InvalidOperationException("Azure OpenAI API Key is required")));
                }
                else if (_aiOptions.Provider == "OpenAI" && !string.IsNullOrEmpty(_aiOptions.ApiKey))
                {
                    var endpoint = new Uri(_aiOptions.BaseUrl ?? "https://api.openai.com/v1");
                    _openAIClient = new OpenAIClient(endpoint, new AzureKeyCredential(_aiOptions.ApiKey));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize OpenAI client for maintenance service");
            }
        }

        public async Task<List<MaintenanceSuggestionDto>> GetMaintenanceSuggestionsAsync(int vehicleId, CancellationToken cancellationToken = default)
        {
            if (_openAIClient == null)
            {
                _logger.LogWarning("OpenAI client not available, falling back to mock service behavior");
                return await GetMockMaintenanceSuggestionsAsync(vehicleId, cancellationToken);
            }

            try
            {
                _logger.LogInformation("AI Maintenance: Getting suggestions for Vehicle {VehicleId}", vehicleId);

                var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);
                if (vehicle == null)
                {
                    _logger.LogWarning("Vehicle {VehicleId} not found", vehicleId);
                    return await GetMockMaintenanceSuggestionsAsync(vehicleId, cancellationToken);
                }

                // Get work order history for this vehicle
                var workOrders = await _workOrderRepository.GetByVehicleIdAsync(vehicleId, cancellationToken);
                var completedWorkOrders = workOrders
                    .Where(wo => wo.Status == Domain.Enums.WorkOrderStatus.Delivered)
                    .OrderByDescending(wo => wo.EntryDate)
                    .Take(10)
                    .ToList();

                var contextBuilder = new StringBuilder();
                contextBuilder.AppendLine($"Araç Bilgileri:");
                contextBuilder.AppendLine($"- Marka: {vehicle.Brand}");
                contextBuilder.AppendLine($"- Model: {vehicle.Model}");
                contextBuilder.AppendLine($"- Yıl: {vehicle.Year}");
                contextBuilder.AppendLine($"- Kilometre: {vehicle.Kilometers:N0} km");
                contextBuilder.AppendLine($"- Yaş: {DateTime.UtcNow.Year - vehicle.Year} yıl");
                contextBuilder.AppendLine();

                if (completedWorkOrders.Any())
                {
                    contextBuilder.AppendLine("Bakım Geçmişi:");
                    foreach (var wo in completedWorkOrders)
                    {
                        contextBuilder.AppendLine($"- {wo.EntryDate:yyyy-MM-dd}: {wo.CustomerComplaints ?? "Bakım"} (Toplam: {wo.TotalAmount:C})");
                    }
                    contextBuilder.AppendLine();
                }

                var systemPrompt = @"Sen bir oto servis bakım uzmanısın. Araç bilgileri ve bakım geçmişine göre periyodik ve önleyici bakım önerileri sunmalısın.
Yanıtını JSON formatında döndür. Format:
{
  ""suggestions"": [
    {
      ""maintenanceType"": ""Periyodik Bakım|Önleyici Bakım|Acil Bakım"",
      ""description"": ""Açıklama"",
      ""recommendedParts"": ""Parça adları"",
      ""recommendedKilometers"": 30000,
      ""recommendedDate"": ""2024-12-31T00:00:00Z"",
      ""urgencyLevel"": 3,
      ""reason"": ""Neden gerekli"",
      ""estimatedCost"": 800
    }
  ]
}";

                var userPrompt = $"Aşağıdaki araç bilgilerine göre bakım önerileri sun:\n\n{contextBuilder}\nLütfen JSON formatında sonuç döndür.";

                var deploymentName = _aiOptions.Provider == "AzureOpenAI"
                    ? (_aiOptions.AzureDeploymentName ?? _aiOptions.Model)
                    : _aiOptions.Model;

                var chatCompletionsOptions = new ChatCompletionsOptions
                {
                    DeploymentName = deploymentName,
                    Messages =
                    {
                        new ChatRequestSystemMessage(systemPrompt),
                        new ChatRequestUserMessage(userPrompt)
                    },
                    Temperature = 0.6f,
                    MaxTokens = 2000
                };

                var response = await _openAIClient.GetChatCompletionsAsync(chatCompletionsOptions, cancellationToken);
                var content = response.Value.Choices[0].Message.Content;

                _logger.LogInformation("AI Maintenance: Received response from OpenAI");

                return ParseMaintenanceSuggestionsResponse(content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AI maintenance suggestions. Falling back to mock result.");
                return await GetMockMaintenanceSuggestionsAsync(vehicleId, cancellationToken);
            }
        }

        public async Task<PartLifespanEstimateDto> EstimatePartLifespanAsync(int partId, int vehicleId, CancellationToken cancellationToken = default)
        {
            if (_openAIClient == null)
            {
                _logger.LogWarning("OpenAI client not available, falling back to mock service behavior");
                return await GetMockPartLifespanAsync(partId, vehicleId, cancellationToken);
            }

            try
            {
                _logger.LogInformation("AI Maintenance: Estimating part lifespan for Part {PartId} in Vehicle {VehicleId}", 
                    partId, vehicleId);

                var part = await _partRepository.GetByIdAsync(partId);
                var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);

                if (part == null || vehicle == null)
                {
                    _logger.LogWarning("Part {PartId} or Vehicle {VehicleId} not found", partId, vehicleId);
                    return await GetMockPartLifespanAsync(partId, vehicleId, cancellationToken);
                }

                // Get work order history for this vehicle and part usage
                var workOrders = await _workOrderRepository.GetByVehicleIdAsync(vehicleId, cancellationToken);
                var partUsageHistory = workOrders
                    .SelectMany(wo => wo.Items ?? Enumerable.Empty<Domain.Entities.WorkOrderItem>())
                    .Where(item => item.PartId == partId)
                    .OrderByDescending(item => item.CreatedDate)
                    .Take(5)
                    .ToList();

                var contextBuilder = new StringBuilder();
                contextBuilder.AppendLine($"Parça Bilgileri:");
                contextBuilder.AppendLine($"- Ad: {part.Name}");
                contextBuilder.AppendLine($"- Kategori: {part.Category}");
                contextBuilder.AppendLine($"- Marka Tipi: {part.BrandType}");
                contextBuilder.AppendLine($"- Garanti Süresi: {part.WarrantyMonths ?? 0} ay");
                contextBuilder.AppendLine();

                contextBuilder.AppendLine($"Araç Bilgileri:");
                contextBuilder.AppendLine($"- Marka/Model: {vehicle.Brand} {vehicle.Model} ({vehicle.Year})");
                contextBuilder.AppendLine($"- Kilometre: {vehicle.Kilometers:N0} km");
                contextBuilder.AppendLine();

                if (partUsageHistory.Any())
                {
                    contextBuilder.AppendLine("Parça Kullanım Geçmişi:");
                    foreach (var usage in partUsageHistory)
                    {
                        contextBuilder.AppendLine($"- {usage.CreatedDate:yyyy-MM-dd}: Kullanıldı");
                    }
                    contextBuilder.AppendLine();
                }

                var systemPrompt = @"Sen bir oto servis parça uzmanısın. Parça bilgileri, araç kullanımı ve geçmişine göre parça ömrü tahmini yapmalısın.
Yanıtını JSON formatında döndür. Format:
{
  ""partId"": 1,
  ""partName"": ""Parça Adı"",
  ""estimatedRemainingKilometers"": 15000,
  ""estimatedRemainingMonths"": 8,
  ""estimatedReplacementDate"": ""2024-12-31T00:00:00Z"",
  ""conditionScore"": 65,
  ""recommendations"": ""Öneriler""
}";

                var userPrompt = $"Aşağıdaki bilgilere göre parça ömrü tahmini yap:\n\n{contextBuilder}\nLütfen JSON formatında sonuç döndür.";

                var deploymentName = _aiOptions.Provider == "AzureOpenAI"
                    ? (_aiOptions.AzureDeploymentName ?? _aiOptions.Model)
                    : _aiOptions.Model;

                var chatCompletionsOptions = new ChatCompletionsOptions
                {
                    DeploymentName = deploymentName,
                    Messages =
                    {
                        new ChatRequestSystemMessage(systemPrompt),
                        new ChatRequestUserMessage(userPrompt)
                    },
                    Temperature = 0.5f,
                    MaxTokens = 1500
                };

                var response = await _openAIClient.GetChatCompletionsAsync(chatCompletionsOptions, cancellationToken);
                var content = response.Value.Choices[0].Message.Content;

                _logger.LogInformation("AI Maintenance: Received response from OpenAI for part lifespan");

                return ParsePartLifespanResponse(content, partId, part.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AI part lifespan estimation. Falling back to mock result.");
                return await GetMockPartLifespanAsync(partId, vehicleId, cancellationToken);
            }
        }

        private List<MaintenanceSuggestionDto> ParseMaintenanceSuggestionsResponse(string jsonContent)
        {
            try
            {
                using var doc = JsonDocument.Parse(jsonContent);
                var root = doc.RootElement;

                if (root.TryGetProperty("suggestions", out var suggestions))
                {
                    return suggestions.EnumerateArray()
                        .Select(s => new MaintenanceSuggestionDto
                        {
                            MaintenanceType = s.GetProperty("maintenanceType").GetString() ?? "",
                            Description = s.TryGetProperty("description", out var desc) ? desc.GetString() : null,
                            RecommendedParts = s.TryGetProperty("recommendedParts", out var parts) ? parts.GetString() : null,
                            RecommendedKilometers = s.TryGetProperty("recommendedKilometers", out var km) ? km.GetInt32() : null,
                            RecommendedDate = s.TryGetProperty("recommendedDate", out var date) && DateTime.TryParse(date.GetString(), out var dt) ? dt : null,
                            UrgencyLevel = s.TryGetProperty("urgencyLevel", out var urgency) ? urgency.GetInt32() : 3,
                            Reason = s.TryGetProperty("reason", out var reason) ? reason.GetString() : null,
                            EstimatedCost = s.TryGetProperty("estimatedCost", out var cost) ? cost.GetDecimal() : null
                        }).ToList();
                }

                return new List<MaintenanceSuggestionDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error parsing maintenance suggestions response: {Content}", jsonContent);
                return new List<MaintenanceSuggestionDto>();
            }
        }

        private PartLifespanEstimateDto ParsePartLifespanResponse(string jsonContent, int partId, string partName)
        {
            try
            {
                using var doc = JsonDocument.Parse(jsonContent);
                var root = doc.RootElement;

                return new PartLifespanEstimateDto
                {
                    PartId = partId,
                    PartName = root.TryGetProperty("partName", out var name) ? name.GetString() ?? partName : partName,
                    EstimatedRemainingKilometers = root.TryGetProperty("estimatedRemainingKilometers", out var km) ? km.GetInt32() : 15000,
                    EstimatedRemainingMonths = root.TryGetProperty("estimatedRemainingMonths", out var months) ? months.GetInt32() : 8,
                    EstimatedReplacementDate = root.TryGetProperty("estimatedReplacementDate", out var date) && DateTime.TryParse(date.GetString(), out var dt) ? dt : DateTime.UtcNow.AddMonths(8),
                    ConditionScore = root.TryGetProperty("conditionScore", out var score) ? score.GetInt32() : 65,
                    Recommendations = root.TryGetProperty("recommendations", out var rec) ? rec.GetString() : null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error parsing part lifespan response: {Content}", jsonContent);
                return new PartLifespanEstimateDto
                {
                    PartId = partId,
                    PartName = partName,
                    EstimatedRemainingKilometers = 15000,
                    EstimatedRemainingMonths = 8,
                    EstimatedReplacementDate = DateTime.UtcNow.AddMonths(8),
                    ConditionScore = 65,
                    Recommendations = "AI yanıtı parse edilemedi. Manuel kontrol önerilir."
                };
            }
        }

        private async Task<List<MaintenanceSuggestionDto>> GetMockMaintenanceSuggestionsAsync(int vehicleId, CancellationToken cancellationToken)
        {
            await Task.Delay(500, cancellationToken);

            return new List<MaintenanceSuggestionDto>
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
                }
            };
        }

        private async Task<PartLifespanEstimateDto> GetMockPartLifespanAsync(int partId, int vehicleId, CancellationToken cancellationToken)
        {
            await Task.Delay(400, cancellationToken);

            return new PartLifespanEstimateDto
            {
                PartId = partId,
                PartName = "Motor Yağı Filtresi",
                EstimatedRemainingKilometers = 15000,
                EstimatedRemainingMonths = 8,
                EstimatedReplacementDate = DateTime.UtcNow.AddMonths(8),
                ConditionScore = 65,
                Recommendations = "Motor yağı filtresi normal kullanım koşullarında yaklaşık 8 ay sonra değiştirilmesi önerilir."
            };
        }
    }
}

