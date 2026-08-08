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
    /// OpenAI destekli personel performans analizi servisi
    /// </summary>
    public class OpenAIEmployeePerformanceAnalysisService : IEmployeePerformanceAnalysisService
    {
        private readonly ILogger<OpenAIEmployeePerformanceAnalysisService> _logger;
        private readonly AIOptions _aiOptions;
        private readonly OpenAIClient? _openAIClient;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IServiceRatingRepository _ratingRepository;
        private readonly ITenantService _tenantService;

        public OpenAIEmployeePerformanceAnalysisService(
            ILogger<OpenAIEmployeePerformanceAnalysisService> logger,
            IOptions<AIOptions> aiOptions,
            IEmployeeRepository employeeRepository,
            IWorkOrderRepository workOrderRepository,
            IServiceRatingRepository ratingRepository,
            ITenantService tenantService)
        {
            _logger = logger;
            _aiOptions = aiOptions.Value;
            _employeeRepository = employeeRepository;
            _workOrderRepository = workOrderRepository;
            _ratingRepository = ratingRepository;
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
                _logger.LogError(ex, "Failed to initialize OpenAI client for employee performance analysis");
            }
        }

        public async Task<EmployeePerformanceAnalysisResponseDto> AnalyzeEmployeePerformanceAsync(
            EmployeePerformanceAnalysisRequestDto request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var clientId = _tenantService.GetRequiredClientId();
                var endDate = DateTime.UtcNow;
                var startDate = endDate.AddDays(-request.AnalysisPeriodDays);

                // Personelleri getir
                var employees = request.EmployeeId.HasValue
                    ? new List<Domain.Entities.Employee> { (await _employeeRepository.GetByIdAsync(request.EmployeeId.Value))! }
                        .Where(e => e != null && e.ClientId == clientId && e.EmploymentStatus == EmploymentStatus.Active)
                    : (await _employeeRepository.GetListAsync(cancellationToken))
                        .Where(e => e.ClientId == clientId && e.EmploymentStatus == EmploymentStatus.Active);

                var analyses = new List<EmployeePerformanceAnalysisDto>();

                foreach (var employee in employees)
                {
                    if (employee == null) continue;

                    // Personel verilerini topla
                    var employeeData = await CollectEmployeeDataAsync(employee.Id, startDate, endDate, clientId, cancellationToken);

                    // AI ile analiz yap veya heuristic kullan
                    if (_openAIClient != null && request.IncludeDetailedAnalysis && employeeData.AssignedWorkOrders.Count > 5)
                    {
                        var aiAnalysis = await GetAIAnalysisAsync(employee, employeeData, request, cancellationToken);
                        if (aiAnalysis != null)
                        {
                            analyses.Add(aiAnalysis);
                            continue;
                        }
                    }

                    // Heuristic analiz
                    var heuristicAnalysis = GetHeuristicAnalysis(employee, employeeData, request);
                    analyses.Add(heuristicAnalysis);
                }

                return new EmployeePerformanceAnalysisResponseDto
                {
                    Analyses = analyses.OrderByDescending(a => a.PerformanceScore)
                                       .ThenByDescending(a => a.TotalRevenueContribution)
                                       .ToList(),
                    Summary = GenerateSummary(analyses),
                    AnalysisDate = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error analyzing employee performance");
                return new EmployeePerformanceAnalysisResponseDto
                {
                    Summary = "Personel performans analizi sırasında bir hata oluştu."
                };
            }
        }

        private async Task<EmployeeData> CollectEmployeeDataAsync(
            int employeeId,
            DateTime startDate,
            DateTime endDate,
            int clientId,
            CancellationToken cancellationToken)
        {
            var data = new EmployeeData();

            // Assigned WorkOrders
            var allAssignedWorkOrders = await _workOrderRepository.GetByEmployeeIdAsync(employeeId, cancellationToken);
            data.AssignedWorkOrders = allAssignedWorkOrders?
                .Where(wo => wo.ClientId == clientId && wo.EntryDate >= startDate && wo.EntryDate <= endDate)
                .ToList() ?? new List<Domain.Entities.WorkOrder>();

            // Labor entries (işi yapan personel)
            var allWorkOrders = await _workOrderRepository.GetListAsync(cancellationToken);
            var allLaborEntries = allWorkOrders
                .Where(wo => wo.ClientId == clientId && wo.Labors != null)
                .SelectMany(wo => wo.Labors.Where(l => l.EmployeeId == employeeId))
                .ToList();

            data.LaborEntries = allLaborEntries
                .Where(l => l.StartTime >= startDate && l.StartTime <= endDate)
                .ToList();

            // Ratings (personel davranışı)
            var allRatings = await _ratingRepository.GetListAsync(cancellationToken);
            var ratingsWithWorkOrders = allRatings?
                .Where(r => r.ClientId == clientId)
                .ToList() ?? new List<Domain.Entities.ServiceRating>();

            // Ratings'i work order'lara bağlayarak personel performansını bul
            data.Ratings = new List<Domain.Entities.ServiceRating>();
            foreach (var rating in ratingsWithWorkOrders)
            {
                var workOrder = data.AssignedWorkOrders.FirstOrDefault(wo => wo.Id == rating.WorkOrderId);
                if (workOrder != null)
                {
                    data.Ratings.Add(rating);
                }
            }

            return data;
        }

        private async Task<EmployeePerformanceAnalysisDto?> GetAIAnalysisAsync(
            Domain.Entities.Employee employee,
            EmployeeData employeeData,
            EmployeePerformanceAnalysisRequestDto request,
            CancellationToken cancellationToken)
        {
            try
            {
                var contextBuilder = new StringBuilder();
                contextBuilder.AppendLine("Sen bir personel performans analiz uzmanısın. Personel performansını analiz edeceksin.");
                contextBuilder.AppendLine();

                contextBuilder.AppendLine($"Personel: {employee.FullName} ({employee.Position})");
                contextBuilder.AppendLine($"İşe Başlama: {employee.HireDate:yyyy-MM-dd}");
                contextBuilder.AppendLine($"Analiz Periyodu: Son {request.AnalysisPeriodDays} gün");
                contextBuilder.AppendLine();

                contextBuilder.AppendLine($"İş Emri İstatistikleri:");
                contextBuilder.AppendLine($"- Atanan İş Emri: {employeeData.AssignedWorkOrders.Count}");
                var completed = employeeData.AssignedWorkOrders.Where(wo => wo.Status == WorkOrderStatus.Delivered).ToList();
                contextBuilder.AppendLine($"- Tamamlanan: {completed.Count}");
                var completionRate = employeeData.AssignedWorkOrders.Any() 
                    ? (double)completed.Count / employeeData.AssignedWorkOrders.Count * 100 
                    : 0;
                contextBuilder.AppendLine($"- Tamamlanma Oranı: {completionRate:F1}%");
                contextBuilder.AppendLine();

                if (employeeData.LaborEntries.Any())
                {
                    var totalHours = employeeData.LaborEntries.Sum(l => l.DurationHours ?? 0);
                    var totalRevenue = employeeData.LaborEntries.Sum(l => l.TotalAmount);
                    contextBuilder.AppendLine($"Çalışma İstatistikleri:");
                    contextBuilder.AppendLine($"- Toplam Çalışma Saati: {totalHours:F1}");
                    contextBuilder.AppendLine($"- Gelir Katkısı: {totalRevenue:C}");
                    contextBuilder.AppendLine($"- Ortalama Saat Ücreti: {(totalHours > 0 ? totalRevenue / (decimal)totalHours : 0):C}");
                    contextBuilder.AppendLine();
                }

                if (employeeData.Ratings.Any())
                {
                    var avgSatisfaction = employeeData.Ratings.Average(r => r.StaffBehavior);
                    contextBuilder.AppendLine($"Müşteri Memnuniyeti:");
                    contextBuilder.AppendLine($"- Ortalama Personel Davranışı: {avgSatisfaction:F2}/5");
                    contextBuilder.AppendLine($"- Toplam Değerlendirme: {employeeData.Ratings.Count}");
                    contextBuilder.AppendLine();
                }

                contextBuilder.AppendLine("Lütfen JSON formatında analiz döndür:");
                contextBuilder.AppendLine("{\"performanceLevel\": \"Mükemmel|İyi|Orta|Düşük\", \"performanceScore\": 0-100, \"trend\": \"Artan|Azalan|Stabil\", \"strengths\": [\"...\"], \"improvementAreas\": [\"...\"], \"recommendations\": [\"...\"], \"explanation\": \"...\", \"confidenceScore\": 0-100}");

                var messages = new List<ChatRequestMessage>
                {
                    new ChatRequestSystemMessage(contextBuilder.ToString()),
                    new ChatRequestUserMessage("Bu personelin performansını analiz et ve seviye, skor, trend, güçlü yönler, gelişim alanları ve öneriler belirle.")
                };

                var chatCompletionsOptions = new ChatCompletionsOptions(
                    deploymentName: _aiOptions.AzureDeploymentName ?? _aiOptions.Model,
                    messages);

                chatCompletionsOptions.Temperature = 0.3f;
                chatCompletionsOptions.MaxTokens = 800;

                var response = await _openAIClient!.GetChatCompletionsAsync(chatCompletionsOptions, cancellationToken);
                var aiResponse = response.Value.Choices[0].Message.Content;

                // AI yanıtını parse et
                return ParseAIResponse(employee, employeeData, request, aiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to get AI analysis, falling back to heuristic");
                return null;
            }
        }

        private EmployeePerformanceAnalysisDto GetHeuristicAnalysis(
            Domain.Entities.Employee employee,
            EmployeeData employeeData,
            EmployeePerformanceAnalysisRequestDto request)
        {
            var assignedWorkOrders = employeeData.AssignedWorkOrders;
            var completedWorkOrders = assignedWorkOrders.Where(wo => wo.Status == WorkOrderStatus.Delivered).ToList();
            var laborEntries = employeeData.LaborEntries;
            var ratings = employeeData.Ratings;

            var totalWorkHours = laborEntries.Sum(l => (double)(l.DurationHours ?? 0));
            var totalRevenue = laborEntries.Sum(l => l.TotalAmount);

            // Ortalama tamamlanma süresi
            var avgCompletionDays = completedWorkOrders
                .Where(wo => wo.EstimatedDeliveryDate.HasValue && wo.ActualDeliveryDate.HasValue)
                .Select(wo => (wo.ActualDeliveryDate!.Value - wo.EntryDate).TotalDays)
                .DefaultIfEmpty(0)
                .Average();

            // Zamanında teslim oranı
            var onTimeDeliveries = completedWorkOrders
                .Count(wo => wo.EstimatedDeliveryDate.HasValue && 
                            wo.ActualDeliveryDate.HasValue && 
                            wo.ActualDeliveryDate.Value <= wo.EstimatedDeliveryDate.Value);
            var onTimeRate = completedWorkOrders.Any() 
                ? (double)onTimeDeliveries / completedWorkOrders.Count * 100 
                : 0;

            // Ortalama müşteri memnuniyeti
            var avgSatisfaction = ratings.Any() ? ratings.Average(r => r.StaffBehavior) : 0;

            // Performans skoru hesapla (0-100)
            var completionScore = assignedWorkOrders.Any() 
                ? (double)completedWorkOrders.Count / assignedWorkOrders.Count * 30 
                : 0;
            var onTimeScore = onTimeRate / 100 * 30;
            var satisfactionScore = avgSatisfaction / 5 * 25;
            var efficiencyScore = assignedWorkOrders.Any() && avgCompletionDays > 0 
                ? Math.Min(15, 15 / avgCompletionDays * 3) 
                : 0;

            var performanceScore = (int)(completionScore + onTimeScore + satisfactionScore + efficiencyScore);

            // Performans seviyesi
            string performanceLevel;
            if (performanceScore >= 85)
                performanceLevel = "Mükemmel";
            else if (performanceScore >= 70)
                performanceLevel = "İyi";
            else if (performanceScore >= 50)
                performanceLevel = "Orta";
            else
                performanceLevel = "Düşük";

            // Trend analizi
            var recentOrders = assignedWorkOrders.Where(wo => wo.EntryDate >= DateTime.UtcNow.AddDays(-30)).ToList();
            var olderOrders = assignedWorkOrders.Where(wo => wo.EntryDate < DateTime.UtcNow.AddDays(-30)).ToList();

            string trend;
            if (recentOrders.Count > olderOrders.Count * 1.2)
                trend = "Artan";
            else if (recentOrders.Count < olderOrders.Count * 0.8)
                trend = "Azalan";
            else
                trend = "Stabil";

            // Güçlü yönler
            var strengths = new List<string>();
            if (onTimeRate >= 90)
                strengths.Add("Yüksek zamanında teslim oranı");
            if (avgSatisfaction >= 4.5)
                strengths.Add("Mükemmel müşteri memnuniyeti");
            if (completionScore >= 85)
                strengths.Add("Yüksek tamamlanma oranı");
            if (assignedWorkOrders.Count >= 20)
                strengths.Add("Yüksek iş yükü kapasitesi");

            // Gelişim alanları
            var improvementAreas = new List<string>();
            if (onTimeRate < 70)
                improvementAreas.Add("Zamanında teslim oranını artırma");
            if (avgSatisfaction < 3.5 && ratings.Any())
                improvementAreas.Add("Müşteri memnuniyetini iyileştirme");
            if (avgCompletionDays > 7)
                improvementAreas.Add("Tamamlanma süresini kısaltma");
            if (performanceScore < 50)
                improvementAreas.Add("Genel performansı artırma");

            // Öneriler
            var recommendations = new List<string>();
            if (onTimeRate < 80)
                recommendations.Add("Zaman yönetimi eğitimi");
            if (avgSatisfaction < 4 && ratings.Any())
                recommendations.Add("Müşteri ilişkileri eğitimi");
            if (performanceScore >= 85)
                recommendations.Add("Mentor olarak görevlendirilebilir");
            if (totalWorkHours < 160)
                recommendations.Add("Daha fazla iş atanabilir");

            var confidenceScore = assignedWorkOrders.Count >= 10 ? 85 : assignedWorkOrders.Count >= 5 ? 70 : 50;

            return new EmployeePerformanceAnalysisDto
            {
                EmployeeId = employee.Id,
                EmployeeName = employee.FullName,
                Position = employee.Position.ToString(),
                PerformanceLevel = performanceLevel,
                PerformanceScore = performanceScore,
                TotalAssignedWorkOrders = assignedWorkOrders.Count,
                CompletedWorkOrders = completedWorkOrders.Count,
                TotalWorkHours = totalWorkHours,
                AverageCompletionDays = avgCompletionDays,
                TotalRevenueContribution = totalRevenue,
                AverageCustomerSatisfaction = avgSatisfaction,
                OnTimeDeliveryRate = onTimeRate,
                EfficiencyScore = (int)(efficiencyScore / 15 * 100),
                Trend = trend,
                Strengths = strengths,
                ImprovementAreas = improvementAreas,
                Recommendations = recommendations,
                Explanation = $"{performanceLevel} performans seviyesinde. {trend} trend gözleniyor.",
                ConfidenceScore = confidenceScore
            };
        }

        private EmployeePerformanceAnalysisDto? ParseAIResponse(
            Domain.Entities.Employee employee,
            EmployeeData employeeData,
            EmployeePerformanceAnalysisRequestDto request,
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

                    var assignedWorkOrders = employeeData.AssignedWorkOrders;
                    var completedWorkOrders = assignedWorkOrders.Where(wo => wo.Status == WorkOrderStatus.Delivered).ToList();
                    var laborEntries = employeeData.LaborEntries;
                    var ratings = employeeData.Ratings;

                    var totalWorkHours = laborEntries.Sum(l => (double)(l.DurationHours ?? 0));
                    var totalRevenue = laborEntries.Sum(l => l.TotalAmount);
                    var avgCompletionDays = completedWorkOrders
                        .Where(wo => wo.EstimatedDeliveryDate.HasValue && wo.ActualDeliveryDate.HasValue)
                        .Select(wo => (wo.ActualDeliveryDate!.Value - wo.EntryDate).TotalDays)
                        .DefaultIfEmpty(0)
                        .Average();
                    var onTimeDeliveries = completedWorkOrders
                        .Count(wo => wo.EstimatedDeliveryDate.HasValue && 
                                    wo.ActualDeliveryDate.HasValue && 
                                    wo.ActualDeliveryDate.Value <= wo.EstimatedDeliveryDate.Value);
                    var onTimeRate = completedWorkOrders.Any() 
                        ? (double)onTimeDeliveries / completedWorkOrders.Count * 100 
                        : 0;
                    var avgSatisfaction = ratings.Any() ? ratings.Average(r => r.StaffBehavior) : 0;

                    var performanceLevel = parsed.TryGetProperty("performanceLevel", out var levelProp) 
                        ? levelProp.GetString() ?? "Orta"
                        : "Orta";

                    var performanceScore = parsed.TryGetProperty("performanceScore", out var scoreProp) 
                        ? scoreProp.GetInt32() 
                        : 50;

                    var trend = parsed.TryGetProperty("trend", out var trendProp) 
                        ? trendProp.GetString() ?? "Stabil"
                        : "Stabil";

                    var strengths = new List<string>();
                    if (parsed.TryGetProperty("strengths", out var strProp) && strProp.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var strength in strProp.EnumerateArray())
                        {
                            if (strength.ValueKind == JsonValueKind.String)
                                strengths.Add(strength.GetString() ?? "");
                        }
                    }

                    var improvementAreas = new List<string>();
                    if (parsed.TryGetProperty("improvementAreas", out var impProp) && impProp.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var area in impProp.EnumerateArray())
                        {
                            if (area.ValueKind == JsonValueKind.String)
                                improvementAreas.Add(area.GetString() ?? "");
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

                    var explanation = parsed.TryGetProperty("explanation", out var explProp) 
                        ? explProp.GetString() ?? ""
                        : "AI analizi";

                    var confidenceScore = parsed.TryGetProperty("confidenceScore", out var confProp) 
                        ? confProp.GetInt32() 
                        : 75;

                    var efficiencyScore = assignedWorkOrders.Any() && avgCompletionDays > 0 
                        ? (int)Math.Min(100, 100 / avgCompletionDays * 3) 
                        : 0;

                    return new EmployeePerformanceAnalysisDto
                    {
                        EmployeeId = employee.Id,
                        EmployeeName = employee.FullName,
                        Position = employee.Position.ToString(),
                        PerformanceLevel = performanceLevel,
                        PerformanceScore = performanceScore,
                        TotalAssignedWorkOrders = assignedWorkOrders.Count,
                        CompletedWorkOrders = completedWorkOrders.Count,
                        TotalWorkHours = totalWorkHours,
                        AverageCompletionDays = avgCompletionDays,
                        TotalRevenueContribution = totalRevenue,
                        AverageCustomerSatisfaction = avgSatisfaction,
                        OnTimeDeliveryRate = onTimeRate,
                        EfficiencyScore = efficiencyScore,
                        Trend = trend,
                        Strengths = strengths,
                        ImprovementAreas = improvementAreas,
                        Recommendations = recommendations,
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

        private string GenerateSummary(List<EmployeePerformanceAnalysisDto> analyses)
        {
            var excellentCount = analyses.Count(a => a.PerformanceLevel == "Mükemmel");
            var goodCount = analyses.Count(a => a.PerformanceLevel == "İyi");
            var needsImprovementCount = analyses.Count(a => a.PerformanceLevel == "Düşük" || a.PerformanceLevel == "Orta");

            return $"Toplam {analyses.Count} personel analiz edildi. " +
                   $"{excellentCount} mükemmel, {goodCount} iyi performans gösteriyor. " +
                   $"{needsImprovementCount} personel için gelişim önerileri mevcut.";
        }

        private class EmployeeData
        {
            public List<Domain.Entities.WorkOrder> AssignedWorkOrders { get; set; } = new();
            public List<Domain.Entities.WorkOrderLabor> LaborEntries { get; set; } = new();
            public List<Domain.Entities.ServiceRating> Ratings { get; set; } = new();
        }
    }
}
