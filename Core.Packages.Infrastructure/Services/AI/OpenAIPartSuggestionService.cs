using Azure;
using Azure.AI.OpenAI;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Infrastructure.Configurations.AI;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace MagicCarRepairAISupported.Infrastructure.Services.AI
{
    /// <summary>
    /// OpenAI destekli parça önerisi servisi
    /// </summary>
    public class OpenAIPartSuggestionService : IPartSuggestionService
    {
        private readonly ILogger<OpenAIPartSuggestionService> _logger;
        private readonly AIOptions _aiOptions;
        private readonly OpenAIClient? _openAIClient;
        private readonly IPartRepository _partRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ITenantService _tenantService;

        public OpenAIPartSuggestionService(
            ILogger<OpenAIPartSuggestionService> logger,
            IOptions<AIOptions> aiOptions,
            IPartRepository partRepository,
            IWorkOrderRepository workOrderRepository,
            IVehicleRepository vehicleRepository,
            ITenantService tenantService)
        {
            _logger = logger;
            _aiOptions = aiOptions.Value;
            _partRepository = partRepository;
            _workOrderRepository = workOrderRepository;
            _vehicleRepository = vehicleRepository;
            _tenantService = tenantService;

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
                    _openAIClient = new OpenAIClient(_aiOptions.ApiKey);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize OpenAI client for part suggestion");
            }
        }

        public async Task<PartSuggestionResponseDto> SuggestPartsAsync(
            PartSuggestionRequestDto request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId() ?? 1;

                // İş emri veya araç bilgilerini al
                Domain.Entities.WorkOrder? workOrder = null;
                Domain.Entities.Vehicle? vehicle = null;

                if (request.WorkOrderId.HasValue)
                {
                    workOrder = await _workOrderRepository.GetWithDetailsAsync(request.WorkOrderId.Value, cancellationToken);
                    vehicle = workOrder?.Vehicle;
                }
                else if (request.VehicleId.HasValue)
                {
                    vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId.Value);
                }

                if (vehicle == null)
                {
                    return new PartSuggestionResponseDto
                    {
                        Explanation = "Araç bilgisi bulunamadı. Lütfen geçerli bir iş emri veya araç ID'si sağlayın."
                    };
                }

                // Benzer iş emirlerinden parça kullanımlarını analiz et
                var similarWorkOrders = await FindSimilarWorkOrdersAsync(vehicle, request.CustomerComplaint, clientId, cancellationToken);

                // Mevcut parçaları getir
                var allParts = (await _partRepository.GetListAsync(cancellationToken))
                    .Where(p => p.ClientId == clientId);

                // Filtreleme
                if (!string.IsNullOrEmpty(request.PartCategory))
                {
                    if (Enum.TryParse<PartCategory>(request.PartCategory, out var category))
                    {
                        allParts = allParts.Where(p => p.Category == category);
                    }
                }

                if (request.MinPrice.HasValue)
                {
                    allParts = allParts.Where(p => p.SalePrice >= request.MinPrice.Value);
                }

                if (request.MaxPrice.HasValue)
                {
                    allParts = allParts.Where(p => p.SalePrice <= request.MaxPrice.Value);
                }

                var partsList = allParts.ToList();

                // AI ile öneri yap veya heuristic kullan
                if (_openAIClient != null && similarWorkOrders.Any() && partsList.Any())
                {
                    var aiSuggestions = await GetAISuggestionsAsync(
                        vehicle, 
                        request, 
                        similarWorkOrders, 
                        partsList, 
                        cancellationToken);
                    
                    if (aiSuggestions != null && aiSuggestions.Any())
                    {
                        return new PartSuggestionResponseDto
                        {
                            Suggestions = aiSuggestions,
                            Explanation = "AI destekli parça önerileri oluşturuldu.",
                            AnalysisDate = DateTime.UtcNow
                        };
                    }
                }

                // Heuristic öneriler
                var heuristicSuggestions = GetHeuristicSuggestions(
                    vehicle, 
                    request, 
                    similarWorkOrders, 
                    partsList);

                return new PartSuggestionResponseDto
                {
                    Suggestions = heuristicSuggestions,
                    Explanation = $"Benzer araçlar ve iş emirlerinden analiz edilerek {heuristicSuggestions.Count} parça önerildi.",
                    AnalysisDate = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error suggesting parts");
                return new PartSuggestionResponseDto
                {
                    Explanation = "Parça önerisi sırasında bir hata oluştu."
                };
            }
        }

        private async Task<List<Domain.Entities.WorkOrder>> FindSimilarWorkOrdersAsync(
            Domain.Entities.Vehicle vehicle,
            string? customerComplaint,
            int clientId,
            CancellationToken cancellationToken)
        {
            // Aynı marka, model ve yıla sahip araçların iş emirlerini bul
            var allWorkOrders = await _workOrderRepository.GetListAsync(cancellationToken);
            
            var similarWorkOrders = allWorkOrders
                .Where(wo => wo.ClientId == clientId && 
                            wo.Vehicle != null &&
                            wo.Vehicle.Brand == vehicle.Brand &&
                            wo.Vehicle.Model == vehicle.Model &&
                            wo.Vehicle.Year == vehicle.Year &&
                            wo.Status == WorkOrderStatus.Delivered)
                .OrderByDescending(wo => wo.EntryDate)
                .Take(20)
                .ToList();

            // Şikayet benzerliğine göre filtrele (basit keyword matching)
            if (!string.IsNullOrEmpty(customerComplaint) && similarWorkOrders.Any())
            {
                var complaintKeywords = customerComplaint.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                similarWorkOrders = similarWorkOrders
                            .Where(wo => complaintKeywords.Any(keyword => 
                        (wo.CustomerComplaints ?? "").ToLower().Contains(keyword) ||
                        (wo.Notes ?? "").ToLower().Contains(keyword)))
                    .ToList();
            }

            return similarWorkOrders;
        }

        private async Task<List<PartSuggestionDto>?> GetAISuggestionsAsync(
            Domain.Entities.Vehicle vehicle,
            PartSuggestionRequestDto request,
            List<Domain.Entities.WorkOrder> similarWorkOrders,
            List<Domain.Entities.Part> availableParts,
            CancellationToken cancellationToken)
        {
            try
            {
                var contextBuilder = new StringBuilder();
                contextBuilder.AppendLine("Sen bir otomobil parça önerisi uzmanısın. Müşterinin ihtiyacına uygun parçalar önereceksin.");
                contextBuilder.AppendLine();

                contextBuilder.AppendLine($"Araç Bilgileri:");
                contextBuilder.AppendLine($"- Marka: {vehicle.Brand}");
                contextBuilder.AppendLine($"- Model: {vehicle.Model}");
                contextBuilder.AppendLine($"- Yıl: {vehicle.Year}");
                contextBuilder.AppendLine($"- Kilometre: {vehicle.Kilometers}");
                contextBuilder.AppendLine();

                if (!string.IsNullOrEmpty(request.CustomerComplaint))
                {
                    contextBuilder.AppendLine($"Müşteri Şikayeti: {request.CustomerComplaint}");
                    contextBuilder.AppendLine();
                }

                // Benzer iş emirlerinde kullanılan parçalar
                var usedParts = new Dictionary<int, int>();
                foreach (var wo in similarWorkOrders)
                {
                    if (wo.Items != null)
                    {
                        foreach (var item in wo.Items.Where(i => i.PartId.HasValue))
                        {
                            var partId = item.PartId!.Value;
                            if (!usedParts.ContainsKey(partId))
                                usedParts[partId] = 0;
                            usedParts[partId]++;
                        }
                    }
                }

                contextBuilder.AppendLine($"Benzer iş emirlerinde en çok kullanılan parçalar:");
                foreach (var partUsage in usedParts.OrderByDescending(p => p.Value).Take(10))
                {
                    var part = availableParts.FirstOrDefault(p => p.Id == partUsage.Key);
                    if (part != null)
                    {
                        contextBuilder.AppendLine($"- {part.Name} ({partUsage.Value} kez kullanılmış)");
                    }
                }
                contextBuilder.AppendLine();

                contextBuilder.AppendLine($"Mevcut Parçalar ({availableParts.Count} adet):");
                foreach (var part in availableParts.Take(30))
                {
                    var stockInfo = part.Stock != null ? $", Stok: {part.Stock.Quantity}" : ", Stok: 0";
                    contextBuilder.AppendLine($"- {part.Name} ({part.Category}){stockInfo}, Fiyat: {part.SalePrice:C}");
                }

                contextBuilder.AppendLine();
                contextBuilder.AppendLine($"Lütfen JSON formatında {request.NumberOfSuggestions} parça önerisi döndür:");
                contextBuilder.AppendLine("{\"suggestions\": [{\"partId\": 0, \"reason\": \"...\", \"suitabilityScore\": 0-100, \"priority\": 1-10}]}");

                var messages = new List<ChatRequestMessage>
                {
                    new ChatRequestSystemMessage(contextBuilder.ToString()),
                    new ChatRequestUserMessage($"Bu araç ve şikayet için en uygun {request.NumberOfSuggestions} parçayı öner.")
                };

                var chatCompletionsOptions = new ChatCompletionsOptions(
                    deploymentName: _aiOptions.AzureDeploymentName ?? _aiOptions.Model,
                    messages);

                chatCompletionsOptions.Temperature = 0.3f;
                chatCompletionsOptions.MaxTokens = 1000;

                var response = await _openAIClient!.GetChatCompletionsAsync(chatCompletionsOptions, cancellationToken);
                var aiResponse = response.Value.Choices[0].Message.Content;

                // AI yanıtını parse et
                return ParseAIResponse(aiResponse, availableParts, usedParts, request);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to get AI suggestions, falling back to heuristic");
                return null;
            }
        }

        private List<PartSuggestionDto> GetHeuristicSuggestions(
            Domain.Entities.Vehicle vehicle,
            PartSuggestionRequestDto request,
            List<Domain.Entities.WorkOrder> similarWorkOrders,
            List<Domain.Entities.Part> availableParts)
        {
            var suggestions = new List<PartSuggestionDto>();

            // Benzer iş emirlerinde kullanılan parçaları say
            var partUsageCount = new Dictionary<int, int>();
            foreach (var wo in similarWorkOrders)
            {
                if (wo.Items != null)
                {
                    foreach (var item in wo.Items.Where(i => i.PartId.HasValue))
                    {
                        var partId = item.PartId!.Value;
                        if (!partUsageCount.ContainsKey(partId))
                            partUsageCount[partId] = 0;
                        partUsageCount[partId]++;
                    }
                }
            }

            // Parçaları skorla
            var scoredParts = new List<(Domain.Entities.Part Part, int Score)>();

            foreach (var part in availableParts)
            {
                var score = 0;

                // Kullanım sıklığı (en önemli faktör)
                if (partUsageCount.ContainsKey(part.Id))
                {
                    score += partUsageCount[part.Id] * 30; // Her kullanım 30 puan
                }

                // Stok durumu
                if (request.PreferInStock && part.Stock != null && part.Stock.Quantity > 0)
                {
                    score += 20;
                    if (part.Stock.Quantity >= part.MinimumStockLevel)
                        score += 10;
                }

                // Fiyat (orta fiyatlı parçalar tercih edilir)
                var avgPrice = availableParts.Any() ? availableParts.Average(p => p.SalePrice) : 0;
                if (avgPrice > 0)
                {
                    var priceRatio = (double)(part.SalePrice / avgPrice);
                    if (priceRatio >= 0.5 && priceRatio <= 1.5)
                        score += 15; // Normal fiyat aralığı
                    else if (priceRatio < 0.5)
                        score += 10; // Ucuz
                }

                // Marka tipi (orijinal tercih edilir)
                if (part.BrandType == PartBrandType.Original)
                    score += 10;

                // Kategori eşleşmesi
                if (!string.IsNullOrEmpty(request.PartCategory) && 
                    Enum.TryParse<PartCategory>(request.PartCategory, out var category) &&
                    part.Category == category)
                {
                    score += 15;
                }

                scoredParts.Add((part, score));
            }

            // En yüksek skorlu parçaları seç
            var topParts = scoredParts
                .OrderByDescending(p => p.Score)
                .Take(request.NumberOfSuggestions)
                .ToList();

            int priority = 1;
            foreach (var (part, score) in topParts)
            {
                var reason = BuildReason(part, partUsageCount, similarWorkOrders.Count, request);
                var stock = part.Stock?.Quantity ?? 0;

                suggestions.Add(new PartSuggestionDto
                {
                    PartId = part.Id,
                    PartName = part.Name,
                    PartCode = part.PartCode,
                    Category = part.Category.ToString(),
                    BrandType = part.BrandType.ToString(),
                    Brand = part.Brand,
                    OEMNumber = part.OEMNumber,
                    SalePrice = part.SalePrice,
                    CurrentStock = stock,
                    Reason = reason,
                    SuitabilityScore = Math.Min(100, score),
                    Priority = priority++
                });
            }

            return suggestions;
        }

        private List<PartSuggestionDto>? ParseAIResponse(
            string aiResponse,
            List<Domain.Entities.Part> availableParts,
            Dictionary<int, int> usedParts,
            PartSuggestionRequestDto request)
        {
            try
            {
                var jsonStart = aiResponse.IndexOf('{');
                var jsonEnd = aiResponse.LastIndexOf('}') + 1;
                if (jsonStart >= 0 && jsonEnd > jsonStart)
                {
                    var json = aiResponse.Substring(jsonStart, jsonEnd - jsonStart);
                    var parsed = JsonSerializer.Deserialize<JsonElement>(json);

                    var suggestions = new List<PartSuggestionDto>();

                    if (parsed.TryGetProperty("suggestions", out var suggestionsArray))
                    {
                        int priority = 1;
                        foreach (var suggestion in suggestionsArray.EnumerateArray())
                        {
                            if (suggestion.TryGetProperty("partId", out var partIdProp))
                            {
                                var partId = partIdProp.GetInt32();
                                var part = availableParts.FirstOrDefault(p => p.Id == partId);
                                if (part != null)
                                {
                                    var reason = suggestion.TryGetProperty("reason", out var reasonProp) 
                                        ? reasonProp.GetString() ?? ""
                                        : "";
                                    var score = suggestion.TryGetProperty("suitabilityScore", out var scoreProp) 
                                        ? scoreProp.GetInt32() 
                                        : 50;
                                    var suggPriority = suggestion.TryGetProperty("priority", out var prioProp) 
                                        ? prioProp.GetInt32() 
                                        : priority++;

                                    suggestions.Add(new PartSuggestionDto
                                    {
                                        PartId = part.Id,
                                        PartName = part.Name,
                                        PartCode = part.PartCode,
                                        Category = part.Category.ToString(),
                                        BrandType = part.BrandType.ToString(),
                                        Brand = part.Brand,
                                        OEMNumber = part.OEMNumber,
                                        SalePrice = part.SalePrice,
                                        CurrentStock = part.Stock?.Quantity ?? 0,
                                        Reason = reason,
                                        SuitabilityScore = score,
                                        Priority = suggPriority
                                    });
                                }
                            }
                        }
                    }

                    return suggestions.OrderBy(s => s.Priority).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to parse AI response");
            }

            return null;
        }

        private string BuildReason(
            Domain.Entities.Part part,
            Dictionary<int, int> partUsageCount,
            int similarWorkOrderCount,
            PartSuggestionRequestDto request)
        {
            var reasons = new List<string>();

            if (partUsageCount.ContainsKey(part.Id))
            {
                reasons.Add($"{similarWorkOrderCount} benzer iş emrinde {partUsageCount[part.Id]} kez kullanılmış");
            }

            if (part.Stock != null && part.Stock.Quantity > 0)
            {
                reasons.Add("Stokta mevcut");
            }

            if (part.BrandType == PartBrandType.Original)
            {
                reasons.Add("Orijinal parça");
            }

            if (!string.IsNullOrEmpty(part.Brand))
            {
                reasons.Add($"{part.Brand} marka");
            }

            return string.Join(", ", reasons);
        }
    }
}
