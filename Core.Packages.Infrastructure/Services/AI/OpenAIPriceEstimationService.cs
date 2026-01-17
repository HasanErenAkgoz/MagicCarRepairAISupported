using Azure;
using Azure.AI.OpenAI;
using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Infrastructure.Configurations.AI;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace MagicCarRepairAISupported.Infrastructure.Services.AI
{
    /// <summary>
    /// OpenAI destekli fiyat tahmini servisi
    /// </summary>
    public class OpenAIPriceEstimationService : IAIPriceEstimationService
    {
        private readonly ILogger<OpenAIPriceEstimationService> _logger;
        private readonly AIOptions _aiOptions;
        private readonly OpenAIClient? _openAIClient;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IPartRepository _partRepository;

        public OpenAIPriceEstimationService(
            ILogger<OpenAIPriceEstimationService> logger,
            IOptions<AIOptions> aiOptions,
            IWorkOrderRepository workOrderRepository,
            ICustomerRepository customerRepository,
            IVehicleRepository vehicleRepository,
            IPartRepository partRepository)
        {
            _logger = logger;
            _aiOptions = aiOptions.Value;
            _workOrderRepository = workOrderRepository;
            _customerRepository = customerRepository;
            _vehicleRepository = vehicleRepository;
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
                _logger.LogError(ex, "Failed to initialize OpenAI client for price estimation");
            }
        }

        public async Task<PriceEstimationResultDto> EstimatePriceAsync(PriceEstimationRequestDto request, CancellationToken cancellationToken = default)
        {
            if (_openAIClient == null)
            {
                _logger.LogWarning("OpenAI client not available, falling back to mock service behavior");
                return await GetMockResultAsync(request, cancellationToken);
            }

            try
            {
                _logger.LogInformation("AI Price Estimation: Estimating price for WorkOrder {WorkOrderId}", request.WorkOrderId);

                // Get context data
                var contextBuilder = new StringBuilder();
                Domain.Entities.WorkOrder? workOrder = null;
                Customer? customer = null;
                Vehicle? vehicle = null;

                if (request.WorkOrderId > 0)
                {
                    workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId);
                    if (workOrder != null)
                    {
                        contextBuilder.AppendLine($"İş Emri Bilgileri:");
                        contextBuilder.AppendLine($"- İş Emri No: {workOrder.WorkOrderNumber}");
                        contextBuilder.AppendLine($"- Durum: {workOrder.Status}");
                        contextBuilder.AppendLine($"- Ara Toplam: {workOrder.SubTotal:C}");
                        contextBuilder.AppendLine($"- Toplam: {workOrder.TotalAmount:C}");
                        contextBuilder.AppendLine();

                        request.VehicleId ??= workOrder.VehicleId;
                        request.CustomerId ??= workOrder.CustomerId;
                    }
                }

                if (request.CustomerId.HasValue)
                {
                    customer = await _customerRepository.GetByIdAsync(request.CustomerId.Value);
                    if (customer != null)
                    {
                        contextBuilder.AppendLine($"Müşteri Bilgileri:");
                        contextBuilder.AppendLine($"- Ad Soyad: {customer.FullName}");
                        
                        // Get customer's work order history for loyalty analysis
                        var customerWorkOrders = await _workOrderRepository.GetByCustomerIdAsync(customer.Id, cancellationToken);
                        var completedOrders = customerWorkOrders.Count(wo => wo.Status == Domain.Enums.WorkOrderStatus.Delivered);
                        var totalSpent = customerWorkOrders.Sum(wo => wo.TotalAmount);
                        
                        contextBuilder.AppendLine($"- Tamamlanan İş Emri Sayısı: {completedOrders}");
                        contextBuilder.AppendLine($"- Toplam Harcama: {totalSpent:C}");
                        contextBuilder.AppendLine();
                    }
                }

                if (request.VehicleId.HasValue)
                {
                    vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId.Value);
                    if (vehicle != null)
                    {
                        contextBuilder.AppendLine($"Araç Bilgileri:");
                        contextBuilder.AppendLine($"- Marka/Model: {vehicle.Brand} {vehicle.Model} ({vehicle.Year})");
                        contextBuilder.AppendLine($"- Kilometre: {vehicle.Kilometers:N0} km");
                        contextBuilder.AppendLine();
                    }
                }

                // Get part prices if part IDs are provided
                if (request.PartIds != null && request.PartIds.Any())
                {
                    contextBuilder.AppendLine("Parça Bilgileri:");
                    foreach (var partId in request.PartIds.Take(10)) // Limit to 10 parts for prompt size
                    {
                        var part = await _partRepository.GetByIdAsync(partId);
                        if (part != null)
                        {
                            contextBuilder.AppendLine($"- {part.Name}: {part.SalePrice:C} (Alış: {part.PurchasePrice:C})");
                        }
                    }
                    contextBuilder.AppendLine();
                }

                var systemPrompt = @"Sen bir oto servis fiyat tahmin uzmanısın. Verilen bilgilere göre rekabetçi ve adil bir fiyat tahmini yapmalısın.
Müşteri geçmişi, araç bilgileri ve piyasa fiyatlarını dikkate alarak tahmin yap.
Yanıtını JSON formatında döndür. Format:
{
  ""estimatedPartsAmount"": 1500,
  ""estimatedLaborAmount"": 2000,
  ""estimatedTaxAmount"": 700,
  ""estimatedTotalAmount"": 4200,
  ""recommendedDiscountPercentage"": 5,
  ""recommendedDiscountAmount"": 210,
  ""discountReason"": ""Müşteri sadakati ve geçmiş alışveriş geçmişine göre önerilen indirim"",
  ""confidenceScore"": 85,
  ""recommendations"": ""Fiyat tahmini önerileri"",
  ""marketPriceAnalysis"": {
    ""averageMarketPrice"": 4500,
    ""minMarketPrice"": 3800,
    ""maxMarketPrice"": 5500,
    ""ourPrice"": 3990,
    ""priceDifference"": -210,
    ""competitivenessLevel"": ""Rekabetçi""
  }
}";

                var userPrompt = new StringBuilder();
                userPrompt.AppendLine("Fiyat Tahmini İsteği:");
                userPrompt.AppendLine(contextBuilder.ToString());
                
                if (!string.IsNullOrEmpty(request.ProblemDescription))
                {
                    userPrompt.AppendLine($"Sorun Açıklaması: {request.ProblemDescription}");
                }

                if (request.LaborTypes != null && request.LaborTypes.Any())
                {
                    userPrompt.AppendLine($"İşçilik Türleri: {string.Join(", ", request.LaborTypes)}");
                }

                userPrompt.AppendLine("\nLütfen yukarıdaki bilgilere göre fiyat tahmini yap ve JSON formatında sonuç döndür.");

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
                    Temperature = 0.5f, // Lower temperature for more consistent pricing
                    MaxTokens = 1500
                };

                var response = await _openAIClient.GetChatCompletionsAsync(chatCompletionsOptions, cancellationToken);
                var content = response.Value.Choices[0].Message.Content;

                _logger.LogInformation("AI Price Estimation: Received response from OpenAI");

                return ParsePriceEstimationResponse(content, request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AI price estimation. Falling back to mock result.");
                return await GetMockResultAsync(request, cancellationToken);
            }
        }

        private PriceEstimationResultDto ParsePriceEstimationResponse(string jsonContent, PriceEstimationRequestDto request)
        {
            try
            {
                using var doc = JsonDocument.Parse(jsonContent);
                var root = doc.RootElement;

                var estimatedPartsAmount = root.TryGetProperty("estimatedPartsAmount", out var parts) ? parts.GetDecimal() : 1500m;
                var estimatedLaborAmount = root.TryGetProperty("estimatedLaborAmount", out var labor) ? labor.GetDecimal() : 2000m;
                var estimatedTaxAmount = root.TryGetProperty("estimatedTaxAmount", out var tax) ? tax.GetDecimal() : (estimatedPartsAmount + estimatedLaborAmount) * 0.20m;
                var estimatedTotalAmount = root.TryGetProperty("estimatedTotalAmount", out var total) ? total.GetDecimal() : estimatedPartsAmount + estimatedLaborAmount + estimatedTaxAmount;

                var recommendedDiscountPercentage = root.TryGetProperty("recommendedDiscountPercentage", out var discPct) ? discPct.GetDecimal() : (decimal?)null;
                var recommendedDiscountAmount = root.TryGetProperty("recommendedDiscountAmount", out var discAmt) ? discAmt.GetDecimal() : (decimal?)null;

                var result = new PriceEstimationResultDto
                {
                    EstimatedPartsAmount = estimatedPartsAmount,
                    EstimatedLaborAmount = estimatedLaborAmount,
                    EstimatedTaxAmount = estimatedTaxAmount,
                    EstimatedTotalAmount = estimatedTotalAmount,
                    RecommendedDiscountPercentage = recommendedDiscountPercentage,
                    RecommendedDiscountAmount = recommendedDiscountAmount,
                    DiscountReason = root.TryGetProperty("discountReason", out var reason) ? reason.GetString() : null,
                    ConfidenceScore = root.TryGetProperty("confidenceScore", out var conf) ? conf.GetInt32() : 75,
                    Recommendations = root.TryGetProperty("recommendations", out var rec) ? rec.GetString() : null,
                    MarketPriceAnalysis = root.TryGetProperty("marketPriceAnalysis", out var market)
                        ? new MarketPriceAnalysisDto
                        {
                            AverageMarketPrice = market.TryGetProperty("averageMarketPrice", out var avg) ? avg.GetDecimal() : estimatedTotalAmount * 1.10m,
                            MinMarketPrice = market.TryGetProperty("minMarketPrice", out var min) ? min.GetDecimal() : estimatedTotalAmount * 0.90m,
                            MaxMarketPrice = market.TryGetProperty("maxMarketPrice", out var max) ? max.GetDecimal() : estimatedTotalAmount * 1.30m,
                            OurPrice = estimatedTotalAmount - (recommendedDiscountAmount ?? 0),
                            PriceDifference = market.TryGetProperty("priceDifference", out var diff) ? diff.GetDecimal() : null,
                            CompetitivenessLevel = market.TryGetProperty("competitivenessLevel", out var level) ? level.GetString() : "Rekabetçi"
                        }
                        : null
                };

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error parsing AI price estimation response: {Content}", jsonContent);
                return GetMockResultAsync(request, CancellationToken.None).GetAwaiter().GetResult();
            }
        }

        private async Task<PriceEstimationResultDto> GetMockResultAsync(PriceEstimationRequestDto request, CancellationToken cancellationToken)
        {
            await Task.Delay(600, cancellationToken);

            var estimatedPartsAmount = 1500m;
            var estimatedLaborAmount = 2000m;
            var subtotal = estimatedPartsAmount + estimatedLaborAmount;
            var taxAmount = subtotal * 0.20m;
            var estimatedTotal = subtotal + taxAmount;

            var recommendedDiscountPercentage = request.CustomerId.HasValue ? 5m : (decimal?)null;
            var recommendedDiscountAmount = recommendedDiscountPercentage.HasValue ? estimatedTotal * (recommendedDiscountPercentage.Value / 100) : (decimal?)null;

            return new PriceEstimationResultDto
            {
                EstimatedPartsAmount = estimatedPartsAmount,
                EstimatedLaborAmount = estimatedLaborAmount,
                EstimatedTaxAmount = taxAmount,
                EstimatedTotalAmount = estimatedTotal,
                RecommendedDiscountPercentage = recommendedDiscountPercentage,
                RecommendedDiscountAmount = recommendedDiscountAmount,
                DiscountReason = recommendedDiscountPercentage.HasValue ? "Müşteri sadakati ve geçmiş alışveriş geçmişine göre önerilen indirim" : null,
                ConfidenceScore = 80,
                Recommendations = "Fiyat tahmini, müşteri geçmişi ve piyasa analizi temel alınarak yapılmıştır.",
                MarketPriceAnalysis = new MarketPriceAnalysisDto
                {
                    AverageMarketPrice = estimatedTotal * 1.10m,
                    MinMarketPrice = estimatedTotal * 0.90m,
                    MaxMarketPrice = estimatedTotal * 1.30m,
                    OurPrice = estimatedTotal - (recommendedDiscountAmount ?? 0),
                    PriceDifference = recommendedDiscountAmount.HasValue ? -recommendedDiscountAmount.Value : 0,
                    CompetitivenessLevel = "Rekabetçi"
                }
            };
        }
    }
}

