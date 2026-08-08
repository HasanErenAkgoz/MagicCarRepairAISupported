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
    /// OpenAI destekli stok tahmin servisi
    /// </summary>
    public class OpenAIStockForecastService : IStockForecastService
    {
        private readonly ILogger<OpenAIStockForecastService> _logger;
        private readonly AIOptions _aiOptions;
        private readonly OpenAIClient? _openAIClient;
        private readonly IPartRepository _partRepository;
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;

        public OpenAIStockForecastService(
            ILogger<OpenAIStockForecastService> logger,
            IOptions<AIOptions> aiOptions,
            IPartRepository partRepository,
            IStockMovementRepository stockMovementRepository,
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService)
        {
            _logger = logger;
            _aiOptions = aiOptions.Value;
            _partRepository = partRepository;
            _stockMovementRepository = stockMovementRepository;
            _workOrderRepository = workOrderRepository;
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
                _logger.LogError(ex, "Failed to initialize OpenAI client for stock forecast");
            }
        }

        public async Task<StockForecastResponseDto> ForecastStockAsync(
            StockForecastRequestDto request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var clientId = _tenantService.GetRequiredClientId();

                // Parçaları getir
                var parts = request.PartId.HasValue
                    ? new List<Domain.Entities.Part> { (await _partRepository.GetByIdAsync(request.PartId.Value))! }
                        .Where(p => p != null && p.ClientId == clientId)
                    : (await _partRepository.GetListAsync(cancellationToken))
                        .Where(p => p.ClientId == clientId);

                var forecasts = new List<PartStockForecastDto>();

                foreach (var part in parts)
                {
                    if (part == null) continue;

                    // Geçmiş verileri analiz et
                    var historicalData = await AnalyzeHistoricalDataAsync(part.Id, request.HistoricalDataDays, clientId, cancellationToken);

                    // AI ile tahmin yap veya heuristic kullan
                    if (_openAIClient != null && historicalData.Movements.Count > 10)
                    {
                        var aiForecast = await GetAIForecastAsync(part, historicalData, request, cancellationToken);
                        if (aiForecast != null)
                        {
                            forecasts.Add(aiForecast);
                            continue;
                        }
                    }

                    // Heuristic tahmin
                    var heuristicForecast = GetHeuristicForecast(part, historicalData, request);
                    forecasts.Add(heuristicForecast);
                }

                return new StockForecastResponseDto
                {
                    Forecasts = forecasts.OrderByDescending(f => f.StockoutRisk == "Yüksek")
                                         .ThenByDescending(f => f.PredictedConsumption)
                                         .ToList(),
                    Summary = GenerateSummary(forecasts),
                    AnalysisDate = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error forecasting stock");
                return new StockForecastResponseDto
                {
                    Summary = "Stok tahmini sırasında bir hata oluştu."
                };
            }
        }

        private async Task<HistoricalDataAnalysis> AnalyzeHistoricalDataAsync(
            int partId,
            int historicalDays,
            int clientId,
            CancellationToken cancellationToken)
        {
            var endDate = DateTime.UtcNow;
            var startDate = endDate.AddDays(-historicalDays);

            // Stok hareketlerini getir
            var movements = await _stockMovementRepository.GetMovementsByDateRangeAsync(
                partId, startDate, endDate, cancellationToken);

            var movementsList = movements
                .Where(m => m.ClientId == clientId && m.MovementType == StockMovementType.Out)
                .OrderBy(m => m.MovementDate)
                .ToList();

            // İş emri bazlı tüketim analizi
            var workOrders = await _workOrderRepository.GetActiveWorkOrdersAsync(cancellationToken);
            var workOrderItems = workOrders
                .Where(wo => wo.ClientId == clientId && wo.Items != null)
                .SelectMany(wo => wo.Items.Where(item => item.PartId == partId))
                .ToList();

            return new HistoricalDataAnalysis
            {
                Movements = movementsList,
                WorkOrderItems = workOrderItems,
                StartDate = startDate,
                EndDate = endDate
            };
        }

        private async Task<PartStockForecastDto?> GetAIForecastAsync(
            Domain.Entities.Part part,
            HistoricalDataAnalysis historicalData,
            StockForecastRequestDto request,
            CancellationToken cancellationToken)
        {
            try
            {
                var contextBuilder = new StringBuilder();
                contextBuilder.AppendLine("Sen bir stok tahmin uzmanısın. Parça stok tüketimini tahmin edeceksin.");
                contextBuilder.AppendLine();

                contextBuilder.AppendLine($"Parça: {part.Name} ({part.PartCode})");
                contextBuilder.AppendLine($"Mevcut Stok: {part.Stock?.Quantity ?? 0}");
                contextBuilder.AppendLine($"Minimum Stok Seviyesi: {part.MinimumStockLevel}");
                contextBuilder.AppendLine();

                // Günlük tüketim verilerini hazırla
                var dailyConsumptions = new Dictionary<DateTime, int>();
                foreach (var movement in historicalData.Movements)
                {
                    var date = movement.MovementDate.Date;
                    if (!dailyConsumptions.ContainsKey(date))
                        dailyConsumptions[date] = 0;
                    dailyConsumptions[date] += movement.Quantity;
                }

                contextBuilder.AppendLine("Son 30 Günlük Tüketim:");
                foreach (var day in dailyConsumptions.OrderByDescending(d => d.Key).Take(30))
                {
                    contextBuilder.AppendLine($"{day.Key:yyyy-MM-dd}: {day.Value} adet");
                }

                var avgDaily = dailyConsumptions.Values.Any() 
                    ? dailyConsumptions.Values.Average() 
                    : 0;

                contextBuilder.AppendLine();
                contextBuilder.AppendLine($"Ortalama Günlük Tüketim: {avgDaily:F2}");
                contextBuilder.AppendLine($"Tahmin Periyodu: {request.ForecastPeriodDays} gün");
                contextBuilder.AppendLine();

                contextBuilder.AppendLine("Lütfen JSON formatında tahmin döndür:");
                contextBuilder.AppendLine("{\"predictedConsumption\": 0, \"trend\": \"Artan|Azalan|Stabil\", \"confidenceScore\": 0-100, \"recommendedMinimumStock\": 0, \"recommendedOrderQuantity\": 0, \"explanation\": \"...\"}");

                var messages = new List<ChatRequestMessage>
                {
                    new ChatRequestSystemMessage(contextBuilder.ToString()),
                    new ChatRequestUserMessage($"Önümüzdeki {request.ForecastPeriodDays} gün için stok tüketimini tahmin et.")
                };

                var chatCompletionsOptions = new ChatCompletionsOptions(
                    deploymentName: _aiOptions.AzureDeploymentName ?? _aiOptions.Model,
                    messages);

                chatCompletionsOptions.Temperature = 0.2f; // Daha deterministik
                chatCompletionsOptions.MaxTokens = 500;

                var response = await _openAIClient!.GetChatCompletionsAsync(chatCompletionsOptions, cancellationToken);
                var aiResponse = response.Value.Choices[0].Message.Content;

                // AI yanıtını parse et
                return ParseAIResponse(part, historicalData, request, aiResponse, avgDaily);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to get AI forecast, falling back to heuristic");
                return null;
            }
        }

        private PartStockForecastDto GetHeuristicForecast(
            Domain.Entities.Part part,
            HistoricalDataAnalysis historicalData,
            StockForecastRequestDto request)
        {
            var currentStock = part.Stock?.Quantity ?? 0;
            var movements = historicalData.Movements;
            var workOrderItems = historicalData.WorkOrderItems;

            // Ortalama günlük tüketim hesapla
            var totalDays = (historicalData.EndDate - historicalData.StartDate).TotalDays;
            var totalConsumption = movements.Sum(m => m.Quantity);
            var avgDailyConsumption = totalDays > 0 ? totalConsumption / totalDays : 0;

            // Trend analizi (son 30 gün vs önceki 30 gün)
            var recentMovements = movements.Where(m => m.MovementDate >= historicalData.EndDate.AddDays(-30)).ToList();
            var olderMovements = movements.Where(m => m.MovementDate < historicalData.EndDate.AddDays(-30)).ToList();
            
            var recentAvg = recentMovements.Any() 
                ? recentMovements.Sum(m => m.Quantity) / 30.0 
                : 0;
            var olderAvg = olderMovements.Any() 
                ? olderMovements.Sum(m => m.Quantity) / 30.0 
                : 0;

            string trend;
            double trendMultiplier = 1.0;
            if (recentAvg > olderAvg * 1.1)
            {
                trend = "Artan";
                trendMultiplier = 1.15;
            }
            else if (recentAvg < olderAvg * 0.9)
            {
                trend = "Azalan";
                trendMultiplier = 0.85;
            }
            else
            {
                trend = "Stabil";
            }

            // Tahmin edilen tüketim
            var predictedConsumption = (int)Math.Ceiling(avgDailyConsumption * request.ForecastPeriodDays * trendMultiplier);
            var predictedEndStock = currentStock - predictedConsumption;

            // Stok tükenme riski
            string stockoutRisk;
            DateTime? stockoutDate = null;
            if (predictedEndStock < 0)
            {
                stockoutRisk = "Yüksek";
                var daysToStockout = currentStock > 0 
                    ? (int)(currentStock / Math.Max(avgDailyConsumption * trendMultiplier, 0.1))
                    : 0;
                stockoutDate = DateTime.UtcNow.AddDays(daysToStockout);
            }
            else if (predictedEndStock < part.MinimumStockLevel)
            {
                stockoutRisk = "Orta";
                var daysToMinimum = (int)((currentStock - part.MinimumStockLevel) / Math.Max(avgDailyConsumption * trendMultiplier, 0.1));
                stockoutDate = DateTime.UtcNow.AddDays(Math.Max(daysToMinimum, request.ForecastPeriodDays));
            }
            else
            {
                stockoutRisk = "Düşük";
            }

            // Öneriler
            int? recommendedMinimumStock = null;
            int? recommendedOrderQuantity = null;
            if (request.IncludeMinimumStockRecommendation)
            {
                recommendedMinimumStock = Math.Max(
                    part.MinimumStockLevel,
                    (int)Math.Ceiling(avgDailyConsumption * trendMultiplier * 30)); // 30 günlük güvenlik stoğu
            }

            if (request.IncludeOrderRecommendation && stockoutRisk != "Düşük")
            {
                var safetyStock = recommendedMinimumStock ?? part.MinimumStockLevel;
                recommendedOrderQuantity = Math.Max(0, safetyStock - predictedEndStock);
            }

            var confidenceScore = movements.Count >= 30 ? 85 : movements.Count >= 10 ? 70 : 50;

            return new PartStockForecastDto
            {
                PartId = part.Id,
                PartName = part.Name,
                PartCode = part.PartCode,
                CurrentStock = currentStock,
                MinimumStockLevel = part.MinimumStockLevel,
                PredictedConsumption = predictedConsumption,
                PredictedEndStock = Math.Max(0, predictedEndStock),
                StockoutRisk = stockoutRisk,
                PredictedStockoutDate = stockoutDate,
                RecommendedMinimumStock = recommendedMinimumStock,
                RecommendedOrderQuantity = recommendedOrderQuantity,
                Trend = trend,
                ConfidenceScore = confidenceScore,
                Explanation = $"{trend} trend gözleniyor. Günlük ortalama {avgDailyConsumption:F2} adet tüketim öngörülüyor.",
                AverageDailyConsumption = avgDailyConsumption
            };
        }

        private PartStockForecastDto? ParseAIResponse(
            Domain.Entities.Part part,
            HistoricalDataAnalysis historicalData,
            StockForecastRequestDto request,
            string aiResponse,
            double avgDailyConsumption)
        {
            try
            {
                var jsonStart = aiResponse.IndexOf('{');
                var jsonEnd = aiResponse.LastIndexOf('}') + 1;
                if (jsonStart >= 0 && jsonEnd > jsonStart)
                {
                    var json = aiResponse.Substring(jsonStart, jsonEnd - jsonStart);
                    var parsed = JsonSerializer.Deserialize<JsonElement>(json);

                    var predictedConsumption = parsed.TryGetProperty("predictedConsumption", out var predCons) 
                        ? predCons.GetInt32() 
                        : (int)Math.Ceiling(avgDailyConsumption * request.ForecastPeriodDays);

                    var trend = parsed.TryGetProperty("trend", out var trendProp) 
                        ? trendProp.GetString() ?? "Stabil"
                        : "Stabil";

                    var confidenceScore = parsed.TryGetProperty("confidenceScore", out var confProp) 
                        ? confProp.GetInt32() 
                        : 75;

                    var recommendedMinimumStock = parsed.TryGetProperty("recommendedMinimumStock", out var recMin) 
                        ? recMin.GetInt32() 
                        : (int?)null;

                    var recommendedOrderQuantity = parsed.TryGetProperty("recommendedOrderQuantity", out var recOrder) 
                        ? recOrder.GetInt32() 
                        : (int?)null;

                    var explanation = parsed.TryGetProperty("explanation", out var explProp) 
                        ? explProp.GetString() ?? string.Empty
                        : "AI tahmini";

                    var currentStock = part.Stock?.Quantity ?? 0;
                    var predictedEndStock = currentStock - predictedConsumption;
                    var stockoutRisk = predictedEndStock < 0 ? "Yüksek" 
                                     : predictedEndStock < part.MinimumStockLevel ? "Orta" 
                                     : "Düşük";

                    return new PartStockForecastDto
                    {
                        PartId = part.Id,
                        PartName = part.Name,
                        PartCode = part.PartCode,
                        CurrentStock = currentStock,
                        MinimumStockLevel = part.MinimumStockLevel,
                        PredictedConsumption = predictedConsumption,
                        PredictedEndStock = Math.Max(0, predictedEndStock),
                        StockoutRisk = stockoutRisk,
                        RecommendedMinimumStock = recommendedMinimumStock,
                        RecommendedOrderQuantity = recommendedOrderQuantity,
                        Trend = trend,
                        ConfidenceScore = confidenceScore,
                        Explanation = explanation,
                        AverageDailyConsumption = avgDailyConsumption
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to parse AI response");
            }

            return null;
        }

        private string GenerateSummary(List<PartStockForecastDto> forecasts)
        {
            var highRisk = forecasts.Count(f => f.StockoutRisk == "Yüksek");
            var mediumRisk = forecasts.Count(f => f.StockoutRisk == "Orta");
            var totalRecommendations = forecasts.Count(f => f.RecommendedOrderQuantity.HasValue && f.RecommendedOrderQuantity > 0);

            return $"Toplam {forecasts.Count} parça analiz edildi. " +
                   $"{highRisk} parça yüksek risk, {mediumRisk} parça orta risk altında. " +
                   $"{totalRecommendations} parça için sipariş önerisi mevcut.";
        }

        private class HistoricalDataAnalysis
        {
            public List<Domain.Entities.StockMovement> Movements { get; set; } = new();
            public List<Domain.Entities.WorkOrderItem> WorkOrderItems { get; set; } = new();
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }
    }
}
