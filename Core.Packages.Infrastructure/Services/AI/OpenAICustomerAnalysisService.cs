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
    /// OpenAI destekli müşteri analizi servisi
    /// </summary>
    public class OpenAICustomerAnalysisService : ICustomerAnalysisService
    {
        private readonly ILogger<OpenAICustomerAnalysisService> _logger;
        private readonly AIOptions _aiOptions;
        private readonly OpenAIClient? _openAIClient;
        private readonly ICustomerRepository _customerRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IServiceRatingRepository _ratingRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ITenantService _tenantService;

        public OpenAICustomerAnalysisService(
            ILogger<OpenAICustomerAnalysisService> logger,
            IOptions<AIOptions> aiOptions,
            ICustomerRepository customerRepository,
            IWorkOrderRepository workOrderRepository,
            IInvoiceRepository invoiceRepository,
            IServiceRatingRepository ratingRepository,
            IAppointmentRepository appointmentRepository,
            IVehicleRepository vehicleRepository,
            ITenantService tenantService)
        {
            _logger = logger;
            _aiOptions = aiOptions.Value;
            _customerRepository = customerRepository;
            _workOrderRepository = workOrderRepository;
            _invoiceRepository = invoiceRepository;
            _ratingRepository = ratingRepository;
            _appointmentRepository = appointmentRepository;
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
                _logger.LogError(ex, "Failed to initialize OpenAI client for customer analysis");
            }
        }

        public async Task<CustomerAnalysisResponseDto> AnalyzeCustomersAsync(
            CustomerAnalysisRequestDto request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var clientId = _tenantService.GetRequiredClientId();
                var endDate = DateTime.UtcNow;
                var startDate = endDate.AddDays(-request.AnalysisPeriodDays);

                // Müşterileri getir
                var customers = request.CustomerId.HasValue
                    ? new List<Domain.Entities.Customer> { (await _customerRepository.GetByIdAsync(request.CustomerId.Value))! }
                        .Where(c => c != null && c.ClientId == clientId)
                    : (await _customerRepository.GetListAsync(cancellationToken))
                        .Where(c => c.ClientId == clientId);

                var analyses = new List<CustomerAnalysisDto>();

                foreach (var customer in customers)
                {
                    if (customer == null) continue;

                    // Müşteri verilerini topla
                    var customerData = await CollectCustomerDataAsync(customer.Id, startDate, endDate, clientId, cancellationToken);

                    // AI ile analiz yap veya heuristic kullan
                    if (_openAIClient != null && request.IncludeDetailedAnalysis && customerData.WorkOrders.Count > 3)
                    {
                        var aiAnalysis = await GetAIAnalysisAsync(customer, customerData, request, cancellationToken);
                        if (aiAnalysis != null)
                        {
                            analyses.Add(aiAnalysis);
                            continue;
                        }
                    }

                    // Heuristic analiz
                    var heuristicAnalysis = GetHeuristicAnalysis(customer, customerData, request);
                    analyses.Add(heuristicAnalysis);
                }

                return new CustomerAnalysisResponseDto
                {
                    Analyses = analyses.OrderByDescending(a => a.CustomerValue == "Yüksek")
                                       .ThenByDescending(a => a.TotalRevenue)
                                       .ToList(),
                    Summary = GenerateSummary(analyses),
                    AnalysisDate = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error analyzing customers");
                return new CustomerAnalysisResponseDto
                {
                    Summary = "Müşteri analizi sırasında bir hata oluştu."
                };
            }
        }

        private async Task<CustomerData> CollectCustomerDataAsync(
            int customerId,
            DateTime startDate,
            DateTime endDate,
            int clientId,
            CancellationToken cancellationToken)
        {
            var data = new CustomerData();

            // WorkOrders
            var allWorkOrders = await _workOrderRepository.GetByCustomerIdAsync(customerId, cancellationToken);
            data.WorkOrders = allWorkOrders?
                .Where(wo => wo.ClientId == clientId && wo.EntryDate >= startDate && wo.EntryDate <= endDate)
                .ToList() ?? new List<Domain.Entities.WorkOrder>();

            // Invoices
            var allInvoices = await _invoiceRepository.GetByCustomerIdAsync(customerId, cancellationToken);
            data.Invoices = allInvoices?
                .Where(inv => inv.ClientId == clientId && inv.CreatedDate >= startDate && inv.CreatedDate <= endDate)
                .ToList() ?? new List<Domain.Entities.Invoice>();

            // Ratings
            var allRatings = await _ratingRepository.GetByCustomerIdAsync(customerId, cancellationToken);
            data.Ratings = allRatings?
                .Where(r => r.ClientId == clientId && r.CreatedDate >= startDate && r.CreatedDate <= endDate)
                .ToList() ?? new List<Domain.Entities.ServiceRating>();

            // Appointments
            var allAppointments = await _appointmentRepository.GetByCustomerIdAsync(customerId, cancellationToken);
            data.Appointments = allAppointments?
                .Where(apt => apt.ClientId == clientId && apt.AppointmentDate >= startDate && apt.AppointmentDate <= endDate)
                .ToList() ?? new List<Domain.Entities.Appointment>();

            // Vehicles
            var allVehicles = await _vehicleRepository.GetByCustomerIdAsync(customerId, cancellationToken);
            data.Vehicles = allVehicles?
                .Where(v => v.ClientId == clientId)
                .ToList() ?? new List<Domain.Entities.Vehicle>();

            return data;
        }

        private async Task<CustomerAnalysisDto?> GetAIAnalysisAsync(
            Domain.Entities.Customer customer,
            CustomerData customerData,
            CustomerAnalysisRequestDto request,
            CancellationToken cancellationToken)
        {
            try
            {
                var contextBuilder = new StringBuilder();
                contextBuilder.AppendLine("Sen bir müşteri analiz uzmanısın. Müşteri davranışını ve değerini analiz edeceksin.");
                contextBuilder.AppendLine();

                contextBuilder.AppendLine($"Müşteri: {customer.FullName} (Yaş: {customer.GetAge()})");
                contextBuilder.AppendLine($"Kayıt Tarihi: {customer.CreatedDate:yyyy-MM-dd}");
                contextBuilder.AppendLine();

                contextBuilder.AppendLine($"Analiz Periyodu: Son {request.AnalysisPeriodDays} gün");
                contextBuilder.AppendLine();

                contextBuilder.AppendLine($"İş Emri İstatistikleri:");
                contextBuilder.AppendLine($"- Toplam İş Emri: {customerData.WorkOrders.Count}");
                contextBuilder.AppendLine($"- Toplam Gelir: {customerData.WorkOrders.Sum(wo => wo.TotalAmount):C}");
                var avgAmount = customerData.WorkOrders.Any() ? customerData.WorkOrders.Average(wo => wo.TotalAmount) : 0;
                contextBuilder.AppendLine($"- Ortalama İş Emri: {avgAmount:C}");
                contextBuilder.AppendLine();

                if (customerData.Ratings.Any())
                {
                    contextBuilder.AppendLine($"Değerlendirme İstatistikleri:");
                    contextBuilder.AppendLine($"- Ortalama Puan: {customerData.Ratings.Average(r => r.Rating):F2}");
                    contextBuilder.AppendLine($"- Hizmet Kalitesi: {customerData.Ratings.Average(r => r.ServiceQuality):F2}");
                    contextBuilder.AppendLine($"- Toplam Değerlendirme: {customerData.Ratings.Count}");
                    contextBuilder.AppendLine();
                }

                contextBuilder.AppendLine($"Fatura İstatistikleri:");
                contextBuilder.AppendLine($"- Toplam Fatura: {customerData.Invoices.Count}");
                var paidInvoices = customerData.Invoices.Where(inv => inv.Status == InvoiceStatus.Paid).ToList();
                contextBuilder.AppendLine($"- Ödenen: {paidInvoices.Count}");
                contextBuilder.AppendLine($"- Ödenmemiş: {customerData.Invoices.Count - paidInvoices.Count}");
                contextBuilder.AppendLine();

                contextBuilder.AppendLine($"Araç Sayısı: {customerData.Vehicles.Count}");
                contextBuilder.AppendLine($"Randevu Sayısı: {customerData.Appointments.Count}");

                contextBuilder.AppendLine();
                contextBuilder.AppendLine("Lütfen JSON formatında analiz döndür:");
                contextBuilder.AppendLine("{\"customerSegment\": \"VIP|Sadık|Yeni|Risk Altında\", \"customerValue\": \"Yüksek|Orta|Düşük\", \"trend\": \"Artan|Azalan|Stabil\", \"riskFactors\": [\"...\"], \"recommendations\": [\"...\"], \"paymentBehavior\": \"İyi|Orta|Kötü\", \"explanation\": \"...\", \"confidenceScore\": 0-100}");

                var messages = new List<ChatRequestMessage>
                {
                    new ChatRequestSystemMessage(contextBuilder.ToString()),
                    new ChatRequestUserMessage("Bu müşteriyi analiz et ve segment, değer, trend, risk faktörleri ve öneriler belirle.")
                };

                var chatCompletionsOptions = new ChatCompletionsOptions(
                    deploymentName: _aiOptions.AzureDeploymentName ?? _aiOptions.Model,
                    messages);

                chatCompletionsOptions.Temperature = 0.3f;
                chatCompletionsOptions.MaxTokens = 800;

                var response = await _openAIClient!.GetChatCompletionsAsync(chatCompletionsOptions, cancellationToken);
                var aiResponse = response.Value.Choices[0].Message.Content;

                // AI yanıtını parse et
                return ParseAIResponse(customer, customerData, request, aiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to get AI analysis, falling back to heuristic");
                return null;
            }
        }

        private CustomerAnalysisDto GetHeuristicAnalysis(
            Domain.Entities.Customer customer,
            CustomerData customerData,
            CustomerAnalysisRequestDto request)
        {
            var workOrders = customerData.WorkOrders;
            var invoices = customerData.Invoices;
            var ratings = customerData.Ratings;

            var totalRevenue = workOrders.Sum(wo => wo.TotalAmount);
            var avgWorkOrderAmount = workOrders.Any() ? workOrders.Average(wo => wo.TotalAmount) : 0;
            var avgRating = ratings.Any() ? ratings.Average(r => r.Rating) : 0;

            var lastVisitDate = workOrders.OrderByDescending(wo => wo.EntryDate).FirstOrDefault()?.EntryDate;
            var daysSinceLastVisit = lastVisitDate.HasValue 
                ? (DateTime.UtcNow - lastVisitDate.Value).Days 
                : int.MaxValue;

            // Müşteri segmenti belirle
            string customerSegment;
            if (workOrders.Count >= 10 && totalRevenue >= 50000)
                customerSegment = "VIP";
            else if (workOrders.Count >= 5 && daysSinceLastVisit < 90)
                customerSegment = "Sadık";
            else if (workOrders.Count < 3)
                customerSegment = "Yeni";
            else if (daysSinceLastVisit > 180 || avgRating < 3)
                customerSegment = "Risk Altında";
            else
                customerSegment = "Standart";

            // Müşteri değeri
            string customerValue;
            if (totalRevenue >= 30000)
                customerValue = "Yüksek";
            else if (totalRevenue >= 10000)
                customerValue = "Orta";
            else
                customerValue = "Düşük";

            // Trend analizi
            var recentOrders = workOrders.Where(wo => wo.EntryDate >= DateTime.UtcNow.AddDays(-90)).ToList();
            var olderOrders = workOrders.Where(wo => wo.EntryDate < DateTime.UtcNow.AddDays(-90)).ToList();
            
            string trend;
            if (recentOrders.Count > olderOrders.Count * 1.2)
                trend = "Artan";
            else if (recentOrders.Count < olderOrders.Count * 0.8)
                trend = "Azalan";
            else
                trend = "Stabil";

            // Risk faktörleri
            var riskFactors = new List<string>();
            if (daysSinceLastVisit > 180)
                riskFactors.Add("Son ziyaretten bu yana 6 aydan fazla geçti");
            if (avgRating < 3 && ratings.Any())
                riskFactors.Add("Düşük memnuniyet skoru");
            
            var unpaidInvoices = invoices.Where(inv => inv.Status != InvoiceStatus.Paid).ToList();
            if (unpaidInvoices.Any())
                riskFactors.Add($"{unpaidInvoices.Count} adet ödenmemiş fatura");

            // Ödeme davranışı
                var paidInvoices = invoices.Where(inv => inv.Status == InvoiceStatus.Paid).ToList();
            string paymentBehavior;
            var paymentRate = invoices.Any() ? (double)paidInvoices.Count / invoices.Count : 1.0;
            if (paymentRate >= 0.9)
                paymentBehavior = "İyi";
            else if (paymentRate >= 0.7)
                paymentBehavior = "Orta";
            else
                paymentBehavior = "Kötü";

            // Öneriler
            var recommendations = new List<string>();
            if (daysSinceLastVisit > 90)
                recommendations.Add("Müşteriyi hatırlatma kampanyası gönder");
            if (avgRating < 3 && ratings.Any())
                recommendations.Add("Memnuniyet anketi gönder ve geri bildirim al");
            if (totalRevenue < 5000 && workOrders.Count > 2)
                recommendations.Add("Yükseltme teklifleri sun");
            if (customerValue == "Yüksek")
                recommendations.Add("VIP müşteri programına dahil et");

            var confidenceScore = workOrders.Count >= 5 ? 85 : workOrders.Count >= 2 ? 70 : 50;

            return new CustomerAnalysisDto
            {
                CustomerId = customer.Id,
                CustomerName = customer.FullName,
                CustomerSegment = customerSegment,
                CustomerValue = customerValue,
                TotalWorkOrders = workOrders.Count,
                TotalRevenue = totalRevenue,
                AverageWorkOrderAmount = (decimal)avgWorkOrderAmount,
                AverageRating = avgRating,
                TotalVehicles = customerData.Vehicles.Count,
                LastVisitDate = lastVisitDate,
                DaysSinceLastVisit = daysSinceLastVisit,
                Trend = trend,
                RiskFactors = riskFactors,
                Recommendations = recommendations,
                PaymentBehavior = paymentBehavior,
                AveragePaymentDelayDays = 0, // TODO: Calculate from invoice dates
                Explanation = $"{customerSegment} müşteri segmentinde. {trend} trend gözleniyor. Toplam {totalRevenue:C} gelir.",
                ConfidenceScore = confidenceScore
            };
        }

        private CustomerAnalysisDto? ParseAIResponse(
            Domain.Entities.Customer customer,
            CustomerData customerData,
            CustomerAnalysisRequestDto request,
            string aiResponse)
        {
            try
            {
                var jsonStart = aiResponse.IndexOf('{');
                var jsonEnd = aiResponse.LastIndexOf('}') + 1;
                if (jsonStart >= 0 && jsonEnd > jsonStart)
                {
                    var json = aiResponse.Substring(jsonStart, jsonEnd - jsonStart);
                    var parsed = JsonSerializer.Deserialize<JsonElement>(json);

                    var workOrders = customerData.WorkOrders;
                    var ratings = customerData.Ratings;
                    var lastVisitDate = workOrders.OrderByDescending(wo => wo.EntryDate).FirstOrDefault()?.EntryDate;

                    var customerSegment = parsed.TryGetProperty("customerSegment", out var segProp) 
                        ? segProp.GetString() ?? "Standart"
                        : "Standart";

                    var customerValue = parsed.TryGetProperty("customerValue", out var valProp) 
                        ? valProp.GetString() ?? "Orta"
                        : "Orta";

                    var trend = parsed.TryGetProperty("trend", out var trendProp) 
                        ? trendProp.GetString() ?? "Stabil"
                        : "Stabil";

                    var riskFactors = new List<string>();
                    if (parsed.TryGetProperty("riskFactors", out var riskProp) && riskProp.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var risk in riskProp.EnumerateArray())
                        {
                            if (risk.ValueKind == JsonValueKind.String)
                                riskFactors.Add(risk.GetString() ?? "");
                        }
                    }

                    var recommendations = new List<string>();
                    if (parsed.TryGetProperty("recommendations", out var recProp) && recProp.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var rec in recProp.EnumerateArray())
                        {
                            if (rec.ValueKind == JsonValueKind.String)
                                recommendations.Add(rec.GetString() ?? "");
                        }
                    }

                    var paymentBehavior = parsed.TryGetProperty("paymentBehavior", out var payProp) 
                        ? payProp.GetString() ?? "Orta"
                        : "Orta";

                    var explanation = parsed.TryGetProperty("explanation", out var explProp) 
                        ? explProp.GetString() ?? ""
                        : "AI analizi";

                    var confidenceScore = parsed.TryGetProperty("confidenceScore", out var confProp) 
                        ? confProp.GetInt32() 
                        : 75;

                    return new CustomerAnalysisDto
                    {
                        CustomerId = customer.Id,
                        CustomerName = customer.FullName,
                        CustomerSegment = customerSegment,
                        CustomerValue = customerValue,
                        TotalWorkOrders = workOrders.Count,
                        TotalRevenue = workOrders.Sum(wo => wo.TotalAmount),
                        AverageWorkOrderAmount = workOrders.Any() ? workOrders.Average(wo => wo.TotalAmount) : 0,
                        AverageRating = ratings.Any() ? ratings.Average(r => r.Rating) : 0,
                        TotalVehicles = customerData.Vehicles.Count,
                        LastVisitDate = lastVisitDate,
                        DaysSinceLastVisit = lastVisitDate.HasValue ? (DateTime.UtcNow - lastVisitDate.Value).Days : int.MaxValue,
                        Trend = trend,
                        RiskFactors = riskFactors,
                        Recommendations = recommendations,
                        PaymentBehavior = paymentBehavior,
                        Explanation = explanation,
                        ConfidenceScore = confidenceScore
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to parse AI response");
            }

            return null;
        }

        private string GenerateSummary(List<CustomerAnalysisDto> analyses)
        {
            var vipCount = analyses.Count(a => a.CustomerSegment == "VIP");
            var loyalCount = analyses.Count(a => a.CustomerSegment == "Sadık");
            var atRiskCount = analyses.Count(a => a.CustomerSegment == "Risk Altında");
            var highValueCount = analyses.Count(a => a.CustomerValue == "Yüksek");

            return $"Toplam {analyses.Count} müşteri analiz edildi. " +
                   $"{vipCount} VIP, {loyalCount} sadık müşteri, {atRiskCount} risk altında müşteri. " +
                   $"{highValueCount} yüksek değerli müşteri.";
        }

        private class CustomerData
        {
            public List<Domain.Entities.WorkOrder> WorkOrders { get; set; } = new();
            public List<Domain.Entities.Invoice> Invoices { get; set; } = new();
            public List<Domain.Entities.ServiceRating> Ratings { get; set; } = new();
            public List<Domain.Entities.Appointment> Appointments { get; set; } = new();
            public List<Domain.Entities.Vehicle> Vehicles { get; set; } = new();
        }
    }
}
