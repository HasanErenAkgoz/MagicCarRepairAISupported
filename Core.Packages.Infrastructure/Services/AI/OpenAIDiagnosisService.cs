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
    /// OpenAI destekli arıza tespiti servisi
    /// </summary>
    public class OpenAIDiagnosisService : IAIDiagnosisService
    {
        private readonly ILogger<OpenAIDiagnosisService> _logger;
        private readonly AIOptions _aiOptions;
        private readonly OpenAIClient? _openAIClient;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IWorkOrderRepository _workOrderRepository;

        public OpenAIDiagnosisService(
            ILogger<OpenAIDiagnosisService> logger,
            IOptions<AIOptions> aiOptions,
            IVehicleRepository vehicleRepository,
            IWorkOrderRepository workOrderRepository)
        {
            _logger = logger;
            _aiOptions = aiOptions.Value;
            _vehicleRepository = vehicleRepository;
            _workOrderRepository = workOrderRepository;

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
                else
                {
                    _logger.LogWarning("OpenAI client not initialized. Provider: {Provider}, API Key: {HasKey}", 
                        _aiOptions.Provider, !string.IsNullOrEmpty(_aiOptions.ApiKey));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize OpenAI client");
            }
        }

        public async Task<DiagnosisResultDto> DiagnoseFromTextAsync(string complaint, int? vehicleId = null, CancellationToken cancellationToken = default)
        {
            if (_openAIClient == null)
            {
                _logger.LogWarning("OpenAI client not available, falling back to mock service behavior");
                return await GetMockResultAsync(complaint, vehicleId, cancellationToken);
            }

            try
            {
                _logger.LogInformation("AI Diagnosis: Analyzing complaint text. VehicleId: {VehicleId}", vehicleId);

                // Get vehicle context if available
                string vehicleContext = string.Empty;
                string workOrderHistory = string.Empty;

                if (vehicleId.HasValue)
                {
                    var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId.Value);
                    if (vehicle != null)
                    {
                        vehicleContext = $"Araç Bilgileri:\n- Marka: {vehicle.Brand}\n- Model: {vehicle.Model}\n- Yıl: {vehicle.Year}\n- Plaka: {vehicle.LicensePlate}\n- Kilometre: {vehicle.Kilometers:N0} km\n";

                        // Get work order history for this vehicle
                        var workOrders = await _workOrderRepository.GetByVehicleIdAsync(vehicleId.Value, cancellationToken);
                        if (workOrders.Any())
                        {
                            var history = workOrders
                                .OrderByDescending(wo => wo.EntryDate)
                                .Take(5)
                                .Select(wo => $"- {wo.EntryDate:yyyy-MM-dd}: {wo.CustomerComplaints ?? "N/A"} (Durum: {wo.Status})");
                            
                            workOrderHistory = "\nGeçmiş İş Emirleri:\n" + string.Join("\n", history);
                        }
                    }
                }

                // Build prompt
                var systemPrompt = @"Sen bir oto servis uzmanısın. Müşterinin şikayetini analiz edip olası arıza türlerini, gerekli parçaları ve işçilikleri tespit etmelisin.
Yanıtını JSON formatında döndür. Format:
{
  ""possibleIssues"": [
    {""issueName"": ""Arıza Adı"", ""description"": ""Açıklama"", ""probabilityScore"": 70, ""category"": ""Motor|Elektrik|Şanzıman|Fren|vb""}
  ],
  ""recommendedParts"": [
    {""partName"": ""Parça Adı"", ""category"": ""Kategori"", ""estimatedPrice"": 500, ""quantity"": 1, ""probabilityScore"": 80}
  ],
  ""recommendedLabors"": [
    {""laborName"": ""İşçilik Adı"", ""description"": ""Açıklama"", ""estimatedPrice"": 300, ""estimatedHours"": 2, ""probabilityScore"": 90}
  ],
  ""estimatedDays"": 3,
  ""estimatedCost"": 2500,
  ""confidenceScore"": 85,
  ""recommendations"": ""Öneriler""
}";

                var userPrompt = new StringBuilder();
                userPrompt.AppendLine("Müşteri Şikayeti:");
                userPrompt.AppendLine(complaint);
                userPrompt.AppendLine();
                
                if (!string.IsNullOrEmpty(vehicleContext))
                {
                    userPrompt.AppendLine(vehicleContext);
                }
                
                if (!string.IsNullOrEmpty(workOrderHistory))
                {
                    userPrompt.AppendLine(workOrderHistory);
                }

                userPrompt.AppendLine("\nLütfen yukarıdaki bilgilere göre arıza tespiti yap ve JSON formatında sonuç döndür.");

                var deploymentName = _aiOptions.Provider == "AzureOpenAI" 
                    ? (_aiOptions.AzureDeploymentName ?? _aiOptions.Model)
                    : _aiOptions.Model;

                var chatCompletionsOptions = new ChatCompletionsOptions
                {
                    DeploymentName = deploymentName,
                    Messages =
                    {
                        new ChatRequestSystemMessage(systemPrompt),
                        new ChatRequestUserMessage(userPrompt.ToString())
                    },
                    Temperature = 0.7f,
                    MaxTokens = 2000
                };

                // For OpenAI, use non-Azure format
                if (_aiOptions.Provider == "OpenAI")
                {
                    chatCompletionsOptions.ResponseFormat = ChatCompletionsResponseFormat.JsonObject;
                }

                var response = await _openAIClient.GetChatCompletionsAsync(chatCompletionsOptions, cancellationToken);
                var content = response.Value.Choices[0].Message.Content;

                _logger.LogInformation("AI Diagnosis: Received response from OpenAI");

                // Parse JSON response
                var result = ParseDiagnosisResponse(content);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AI diagnosis. Falling back to mock result.");
                return await GetMockResultAsync(complaint, vehicleId, cancellationToken);
            }
        }

        public async Task<DiagnosisResultDto> DiagnoseFromVoiceAsync(byte[] audioData, int? vehicleId = null, CancellationToken cancellationToken = default)
        {
            if (_openAIClient == null)
            {
                _logger.LogWarning("OpenAI client not available, falling back to mock service behavior");
                return await GetMockResultAsync("Sesli şikayet (mock)", vehicleId, cancellationToken);
            }

            try
            {
                _logger.LogInformation("AI Diagnosis: Analyzing voice complaint. VehicleId: {VehicleId}, AudioSize: {AudioSize} bytes", 
                    vehicleId, audioData.Length);

                // Convert audio to text using Whisper (if available) or return mock
                // For now, we'll use a mock implementation until Whisper API is properly integrated
                _logger.LogWarning("Voice-to-text conversion not yet implemented. Using mock diagnosis.");
                return await GetMockResultAsync("Sesli şikayet analizi (henüz implement edilmedi)", vehicleId, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in voice diagnosis. Falling back to mock result.");
                return await GetMockResultAsync("Sesli şikayet (hata)", vehicleId, cancellationToken);
            }
        }

        private DiagnosisResultDto ParseDiagnosisResponse(string jsonContent)
        {
            try
            {
                using var doc = JsonDocument.Parse(jsonContent);
                var root = doc.RootElement;

                var result = new DiagnosisResultDto
                {
                    PossibleIssues = root.GetProperty("possibleIssues").EnumerateArray()
                        .Select(issue => new DiagnosisItemDto
                        {
                            IssueName = issue.GetProperty("issueName").GetString() ?? "",
                            Description = issue.TryGetProperty("description", out var desc) ? desc.GetString() : null,
                            ProbabilityScore = issue.TryGetProperty("probabilityScore", out var prob) ? prob.GetInt32() : 50,
                            Category = issue.TryGetProperty("category", out var cat) ? cat.GetString() : null
                        }).ToList(),

                    RecommendedParts = root.TryGetProperty("recommendedParts", out var parts) 
                        ? parts.EnumerateArray()
                            .Select(part => new RecommendedPartDto
                            {
                                PartName = part.GetProperty("partName").GetString() ?? "",
                                Category = part.TryGetProperty("category", out var cat) ? cat.GetString() : null,
                                EstimatedPrice = part.TryGetProperty("estimatedPrice", out var price) ? price.GetDecimal() : null,
                                Quantity = part.TryGetProperty("quantity", out var qty) ? qty.GetInt32() : 1,
                                ProbabilityScore = part.TryGetProperty("probabilityScore", out var prob) ? prob.GetInt32() : 50
                            }).ToList()
                        : new List<RecommendedPartDto>(),

                    RecommendedLabors = root.TryGetProperty("recommendedLabors", out var labors)
                        ? labors.EnumerateArray()
                            .Select(labor => new RecommendedLaborDto
                            {
                                LaborName = labor.GetProperty("laborName").GetString() ?? "",
                                Description = labor.TryGetProperty("description", out var desc) ? desc.GetString() : null,
                                EstimatedPrice = labor.TryGetProperty("estimatedPrice", out var price) ? price.GetDecimal() : null,
                                EstimatedHours = labor.TryGetProperty("estimatedHours", out var hours) ? hours.GetDecimal() : null,
                                ProbabilityScore = labor.TryGetProperty("probabilityScore", out var prob) ? prob.GetInt32() : 50
                            }).ToList()
                        : new List<RecommendedLaborDto>(),

                    EstimatedDays = root.TryGetProperty("estimatedDays", out var days) ? days.GetInt32() : null,
                    EstimatedCost = root.TryGetProperty("estimatedCost", out var cost) ? cost.GetDecimal() : null,
                    ConfidenceScore = root.TryGetProperty("confidenceScore", out var conf) ? conf.GetInt32() : 75,
                    Recommendations = root.TryGetProperty("recommendations", out var rec) ? rec.GetString() : null
                };

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error parsing AI response: {Content}", jsonContent);
                // Return a basic result if parsing fails
                return new DiagnosisResultDto
                {
                    ConfidenceScore = 50,
                    Recommendations = "AI yanıtı parse edilemedi. Manuel kontrol önerilir.",
                    PossibleIssues = new List<DiagnosisItemDto>
                    {
                        new DiagnosisItemDto
                        {
                            IssueName = "Genel Arıza",
                            Description = jsonContent,
                            ProbabilityScore = 50,
                            Category = "Diğer"
                        }
                    }
                };
            }
        }

        private async Task<DiagnosisResultDto> GetMockResultAsync(string complaint, int? vehicleId, CancellationToken cancellationToken)
        {
            await Task.Delay(500, cancellationToken);

            return new DiagnosisResultDto
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
                    }
                }
            };
        }
    }
}

