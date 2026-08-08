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
                var clientId = _tenantService.GetRequiredClientId();

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
                // Not: similarWorkOrders olmasa da AI kendi genel bilgisiyle öneri yapabilir
                if (_openAIClient != null && partsList.Any())
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
                // ── Benzer iş emirlerindeki parça kullanım sayıları ──────────────
                var usedParts = new Dictionary<int, int>();
                foreach (var wo in similarWorkOrders)
                {
                    if (wo.Items == null) continue;
                    foreach (var item in wo.Items.Where(i => i.PartId.HasValue))
                    {
                        var pid = item.PartId!.Value;
                        usedParts.TryGetValue(pid, out var cnt);
                        usedParts[pid] = cnt + 1;
                    }
                }

                // ── Parça listesini oluştur: stokta olanlar ve sık kullanılanlar önce ──
                var sortedParts = availableParts
                    .OrderByDescending(p => usedParts.ContainsKey(p.Id) ? usedParts[p.Id] : 0)
                    .ThenByDescending(p => p.Stock?.Quantity ?? 0)
                    .Take(60)
                    .ToList();

                // ── System prompt ────────────────────────────────────────────────
                var systemPrompt = new StringBuilder();
                systemPrompt.AppendLine("Sen deneyimli bir otomotiv parça uzmanısın.");
                systemPrompt.AppendLine("Görevin: verilen araç bilgileri ve müşteri şikayetine göre envanterdeki en uygun parçaları önermek.");
                systemPrompt.AppendLine();
                systemPrompt.AppendLine("KURALLAR:");
                systemPrompt.AppendLine("1. YALNIZCA 'Mevcut Parça Listesi' bölümündeki ID değerlerini kullan. Listede olmayan partId yazma.");
                systemPrompt.AppendLine("2. Her öneri için araç teknik özelliklerine ve şikayete dayalı somut gerekçe yaz.");
                systemPrompt.AppendLine("3. Yağ önerilerinde viskozite ve spesifikasyonu (5W-30, 507.00 vb.) araç üreticisi gereksinimlerine göre değerlendir.");
                systemPrompt.AppendLine("4. Yakıt tipini dikkate al: Dizel ve benzinli araçlar farklı yağ/filtre gerektirir.");
                systemPrompt.AppendLine("5. Kilometre bilgisini dikkate al: yüksek kilometreli araçlarda yıpranma parçaları (balata, amortisör vb.) daha olası.");
                systemPrompt.AppendLine("6. suitabilityScore: 80-100 = kesinlikle uyumlu ve gerekli, 50-79 = muhtemelen gerekli, 0-49 = düşük ihtimal.");
                systemPrompt.AppendLine("7. Stokta olmayan parçaları (Stok: 0) listeleyebilirsin ama suitabilityScore'u 10 puan düşür.");
                systemPrompt.AppendLine("8. Yanıtı YALNIZCA geçerli JSON olarak döndür, başka metin ekleme.");

                // ── User prompt ──────────────────────────────────────────────────
                var userPrompt = new StringBuilder();

                // Araç bilgileri
                userPrompt.AppendLine("== ARAÇ BİLGİLERİ ==");
                userPrompt.AppendLine($"Marka       : {vehicle.Brand}");
                userPrompt.AppendLine($"Model       : {vehicle.Model}");
                userPrompt.AppendLine($"Yıl         : {vehicle.Year}");
                userPrompt.AppendLine($"Yakıt Tipi  : {(string.IsNullOrEmpty(vehicle.FuelType) ? "Belirtilmemiş" : vehicle.FuelType)}");
                userPrompt.AppendLine($"Kilometre   : {vehicle.Kilometers:N0}");
                if (!string.IsNullOrEmpty(vehicle.Trim))
                    userPrompt.AppendLine($"Trim/Versiyon: {vehicle.Trim}");
                if (!string.IsNullOrEmpty(vehicle.ModelVariant))
                    userPrompt.AppendLine($"Motor/Varyant: {vehicle.ModelVariant}");
                if (!string.IsNullOrEmpty(vehicle.Vin))
                    userPrompt.AppendLine($"VIN         : {vehicle.Vin}");
                userPrompt.AppendLine();

                // Müşteri şikayeti
                userPrompt.AppendLine("== MÜŞTERİ ŞİKAYETİ ==");
                userPrompt.AppendLine(string.IsNullOrEmpty(request.CustomerComplaint)
                    ? "Belirtilmemiş"
                    : request.CustomerComplaint);
                userPrompt.AppendLine();

                // Geçmiş servis verileri
                if (usedParts.Any())
                {
                    userPrompt.AppendLine($"== GEÇMİŞ SERVİS VERİSİ ({similarWorkOrders.Count} benzer iş emri) ==");
                    userPrompt.AppendLine("Bu araç modeli için daha önce en sık kullanılan parçalar:");
                    foreach (var kv in usedParts.OrderByDescending(x => x.Value).Take(10))
                    {
                        var p = availableParts.FirstOrDefault(x => x.Id == kv.Key);
                        if (p != null)
                            userPrompt.AppendLine($"  - [{p.Id}] {p.Name} → {kv.Value} kez kullanılmış");
                    }
                    userPrompt.AppendLine();
                }

                // Parça listesi — ID dahil, tüm teknik detaylarla
                userPrompt.AppendLine($"== MEVCUT PARÇA LİSTESİ ({sortedParts.Count} parça) ==");
                userPrompt.AppendLine("SADECE bu listedeki ID değerlerini kullan:");
                userPrompt.AppendLine("ID  | Ad | Kategori | Marka | Tip | OEM | Birim | Stok | Fiyat | Açıklama");
                userPrompt.AppendLine(new string('-', 120));
                foreach (var part in sortedParts)
                {
                    var stock = part.Stock?.Quantity ?? 0;
                    var oem = string.IsNullOrEmpty(part.OEMNumber) ? "-" : part.OEMNumber;
                    var brand = string.IsNullOrEmpty(part.Brand) ? "-" : part.Brand;
                    var desc = string.IsNullOrEmpty(part.Description) ? "-" : part.Description.Length > 60
                        ? part.Description[..60] + "…"
                        : part.Description;
                    userPrompt.AppendLine(
                        $"{part.Id,4} | {part.Name,-35} | {part.Category,-12} | {brand,-12} | {part.BrandType,-10} | {oem,-15} | {part.Unit,-6} | {stock,4} | {part.SalePrice,8:N2}₺ | {desc}");
                }
                userPrompt.AppendLine();

                // İstenen format
                userPrompt.AppendLine($"== GÖREV ==");
                userPrompt.AppendLine($"Yukarıdaki araç ve şikayet için en uygun {request.NumberOfSuggestions} parçayı öner.");
                userPrompt.AppendLine("Yanıtı YALNIZCA aşağıdaki JSON formatında döndür:");
                userPrompt.AppendLine(@"{
  ""suggestions"": [
    {
      ""partId"": <yukarıdaki listeden geçerli bir tam sayı ID>,
      ""reason"": ""<araç yakıt tipine, kilometresine ve şikayete özel somut gerekçe>"",
      ""suitabilityScore"": <0-100 tam sayı>,
      ""priority"": <1'den başlayan öncelik sırası>
    }
  ]
}");

                var messages = new List<ChatRequestMessage>
                {
                    new ChatRequestSystemMessage(systemPrompt.ToString()),
                    new ChatRequestUserMessage(userPrompt.ToString())
                };

                var chatCompletionsOptions = new ChatCompletionsOptions(
                    deploymentName: _aiOptions.AzureDeploymentName ?? _aiOptions.Model,
                    messages);

                chatCompletionsOptions.Temperature = 0.2f;
                chatCompletionsOptions.MaxTokens = 2000;

                var response = await _openAIClient!.GetChatCompletionsAsync(chatCompletionsOptions, cancellationToken);
                var aiResponse = response.Value.Choices[0].Message.Content;

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
