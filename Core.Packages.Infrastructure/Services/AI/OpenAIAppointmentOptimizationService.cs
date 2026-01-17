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
    /// OpenAI destekli randevu optimizasyon servisi
    /// </summary>
    public class OpenAIAppointmentOptimizationService : IAppointmentOptimizationService
    {
        private readonly ILogger<OpenAIAppointmentOptimizationService> _logger;
        private readonly AIOptions _aiOptions;
        private readonly OpenAIClient? _openAIClient;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;

        public OpenAIAppointmentOptimizationService(
            ILogger<OpenAIAppointmentOptimizationService> logger,
            IOptions<AIOptions> aiOptions,
            IAppointmentRepository appointmentRepository,
            IEmployeeRepository employeeRepository,
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService)
        {
            _logger = logger;
            _aiOptions = aiOptions.Value;
            _appointmentRepository = appointmentRepository;
            _employeeRepository = employeeRepository;
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
                _logger.LogError(ex, "Failed to initialize OpenAI client for appointment optimization");
            }
        }

        public async Task<AppointmentOptimizationResponseDto> OptimizeAppointmentsAsync(
            AppointmentOptimizationRequestDto request, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId() ?? 1;

                // Mevcut randevuları ve personel yükünü analiz et
                var analysisData = await AnalyzeAvailabilityAsync(request, clientId, cancellationToken);

                // OpenAI ile optimizasyon yap
                if (_openAIClient != null)
                {
                    return await GetOpenAIOptimizationAsync(request, analysisData, cancellationToken);
                }
                else
                {
                    // Mock/Heuristic optimizasyon
                    return GetHeuristicOptimization(request, analysisData);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error optimizing appointments");
                return new AppointmentOptimizationResponseDto
                {
                    Explanation = "Randevu optimizasyonu sırasında bir hata oluştu. Lütfen manuel olarak randevu seçin."
                };
            }
        }

        private async Task<AvailabilityAnalysisData> AnalyzeAvailabilityAsync(
            AppointmentOptimizationRequestDto request, 
            int clientId, 
            CancellationToken cancellationToken)
        {
            var analysis = new AvailabilityAnalysisData();

            // Tarih aralığını belirle
            var startDate = request.PreferredStartDate ?? DateTime.UtcNow.Date.AddDays(1);
            var endDate = request.PreferredEndDate ?? startDate.AddDays(14);

            // Mevcut randevuları getir
            var existingAppointments = await _appointmentRepository.GetByDateRangeAsync(startDate, endDate, cancellationToken);
            analysis.ExistingAppointments = existingAppointments
                .Where(a => a.ClientId == clientId && 
                           a.Status != AppointmentStatus.Cancelled && 
                           a.Status != AppointmentStatus.NoShow)
                .ToList();

            // Personel bilgilerini getir
            var employees = await _employeeRepository.GetListAsync(cancellationToken);
            analysis.AvailableEmployees = employees
                .Where(e => e.ClientId == clientId && e.EmploymentStatus == Domain.Enums.EmploymentStatus.Active)
                .ToList();

            // Personel yükünü hesapla
            foreach (var employee in analysis.AvailableEmployees)
            {
                var employeeAppointments = analysis.ExistingAppointments
                    .Where(a => a.AssignedEmployeeId == employee.Id)
                    .ToList();
                analysis.EmployeeWorkloads[employee.Id] = employeeAppointments.Count;
            }

            // Müşterinin geçmiş randevu tercihlerini analiz et
            var customerAppointments = await _appointmentRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);
            if (customerAppointments != null && customerAppointments.Any())
            {
                var completedAppointments = customerAppointments
                    .Where(a => a.Status == AppointmentStatus.Completed)
                    .OrderByDescending(a => a.AppointmentDate)
                    .Take(10)
                    .ToList();

                if (completedAppointments.Any())
                {
                    // En çok tercih edilen saat aralığını bul
                    var preferredHours = completedAppointments
                        .GroupBy(a => a.StartTime.Hours)
                        .OrderByDescending(g => g.Count())
                        .FirstOrDefault();

                    if (preferredHours != null)
                    {
                        analysis.PreferredHour = preferredHours.Key;
                    }
                }
            }

            return analysis;
        }

        private async Task<AppointmentOptimizationResponseDto> GetOpenAIOptimizationAsync(
            AppointmentOptimizationRequestDto request,
            AvailabilityAnalysisData analysisData,
            CancellationToken cancellationToken)
        {
            var contextBuilder = new StringBuilder();
            contextBuilder.AppendLine("Sen bir otomobil tamir servisi randevu optimizasyon asistanısın.");
            contextBuilder.AppendLine("Müşteri için en uygun randevu saatlerini önereceksin.");
            contextBuilder.AppendLine();

            contextBuilder.AppendLine($"Randevu Türü: {request.AppointmentType}");
            contextBuilder.AppendLine($"Tahmini Süre: {request.EstimatedDurationMinutes ?? 60} dakika");
            contextBuilder.AppendLine();

            if (request.PreferredStartDate.HasValue)
            {
                contextBuilder.AppendLine($"Tercih Edilen Tarih: {request.PreferredStartDate.Value:dd.MM.yyyy}");
            }

            if (request.PreferredStartTime.HasValue)
            {
                contextBuilder.AppendLine($"Tercih Edilen Saat: {request.PreferredStartTime.Value:hh\\:mm}");
            }

            contextBuilder.AppendLine();
            contextBuilder.AppendLine("Mevcut Randevular:");
            foreach (var apt in analysisData.ExistingAppointments.Take(20))
            {
                contextBuilder.AppendLine($"- {apt.AppointmentDate:dd.MM.yyyy} {apt.StartTime:hh\\:mm} - Personel: {apt.AssignedEmployeeId}");
            }

            contextBuilder.AppendLine();
            contextBuilder.AppendLine("Personel Yükü:");
            foreach (var workload in analysisData.EmployeeWorkloads)
            {
                var employee = analysisData.AvailableEmployees.FirstOrDefault(e => e.Id == workload.Key);
                contextBuilder.AppendLine($"- Personel {employee?.FullName ?? workload.Key.ToString()}: {workload.Value} randevu");
            }

            if (analysisData.PreferredHour.HasValue)
            {
                contextBuilder.AppendLine();
                contextBuilder.AppendLine($"Müşterinin geçmiş tercihi: {analysisData.PreferredHour.Value}:00 saatleri");
            }

            contextBuilder.AppendLine();
            contextBuilder.AppendLine($"Lütfen {request.NumberOfSuggestions} adet en uygun randevu saati öner. JSON formatında döndür:");
            contextBuilder.AppendLine("{\"suggestions\": [{\"date\": \"YYYY-MM-DD\", \"startTime\": \"HH:mm\", \"endTime\": \"HH:mm\", \"employeeId\": null, \"score\": 0-100, \"reason\": \"...\"}]}");

            var messages = new List<ChatRequestMessage>
            {
                new ChatRequestSystemMessage(contextBuilder.ToString()),
                new ChatRequestUserMessage($"En uygun {request.NumberOfSuggestions} randevu saatini öner.")
            };

            var chatCompletionsOptions = new ChatCompletionsOptions(
                deploymentName: _aiOptions.AzureDeploymentName ?? _aiOptions.Model,
                messages);

            chatCompletionsOptions.Temperature = 0.3f; // Daha deterministik yanıtlar için
            chatCompletionsOptions.MaxTokens = 1000;

            var response = await _openAIClient!.GetChatCompletionsAsync(chatCompletionsOptions, cancellationToken);
            var aiResponse = response.Value.Choices[0].Message.Content;

            // AI yanıtını parse et
            return ParseAIResponse(aiResponse, request, analysisData);
        }

        private AppointmentOptimizationResponseDto GetHeuristicOptimization(
            AppointmentOptimizationRequestDto request,
            AvailabilityAnalysisData analysisData)
        {
            var suggestions = new List<AppointmentSuggestionDto>();
            var startDate = request.PreferredStartDate ?? DateTime.UtcNow.Date.AddDays(1);
            var endDate = request.PreferredEndDate ?? startDate.AddDays(14);
            var duration = request.EstimatedDurationMinutes ?? 60;
            var preferredStartHour = request.PreferredStartTime?.Hours ?? analysisData.PreferredHour ?? 9;
            var preferredEndHour = request.PreferredEndTime?.Hours ?? 17;

            // Çalışma saatleri (varsayılan: 09:00 - 18:00)
            var workStartHour = 9;
            var workEndHour = 18;

            var currentDate = startDate;
            var suggestionCount = 0;

            while (currentDate <= endDate && suggestionCount < request.NumberOfSuggestions)
            {
                // Hafta sonu kontrolü (opsiyonel - şimdilik atlıyoruz)
                if (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    currentDate = currentDate.AddDays(1);
                    continue;
                }

                // Tercih edilen saat aralığında veya yakınında saatler öner
                for (int hour = Math.Max(workStartHour, preferredStartHour); 
                     hour < Math.Min(workEndHour, preferredEndHour) && suggestionCount < request.NumberOfSuggestions; 
                     hour++)
                {
                    var suggestedStartTime = new TimeSpan(hour, 0, 0);
                    var suggestedEndTime = suggestedStartTime.Add(TimeSpan.FromMinutes(duration));

                    // Bu saatte mevcut randevu var mı kontrol et
                    var conflictingAppointment = analysisData.ExistingAppointments
                        .FirstOrDefault(a => a.AppointmentDate.Date == currentDate.Date &&
                                           a.StartTime <= suggestedStartTime &&
                                           (a.EndTime ?? a.StartTime.Add(TimeSpan.FromHours(1))) > suggestedStartTime);

                    if (conflictingAppointment == null)
                    {
                        // En az yüklü personeli bul
                        var availableEmployee = analysisData.AvailableEmployees
                            .OrderBy(e => analysisData.EmployeeWorkloads.GetValueOrDefault(e.Id, 0))
                            .FirstOrDefault();

                        var score = CalculateSuitabilityScore(
                            currentDate, 
                            suggestedStartTime, 
                            preferredStartHour, 
                            analysisData.EmployeeWorkloads.GetValueOrDefault(availableEmployee?.Id ?? 0, 0));

                        suggestions.Add(new AppointmentSuggestionDto
                        {
                            SuggestedDate = currentDate,
                            SuggestedStartTime = suggestedStartTime,
                            SuggestedEndTime = suggestedEndTime,
                            SuggestedEmployeeId = availableEmployee?.Id,
                            SuggestedEmployeeName = availableEmployee?.FullName,
                            SuitabilityScore = score,
                            Reason = GetSuggestionReason(currentDate, suggestedStartTime, availableEmployee, score),
                            EmployeeWorkload = analysisData.EmployeeWorkloads.GetValueOrDefault(availableEmployee?.Id ?? 0, 0)
                        });

                        suggestionCount++;
                    }
                }

                currentDate = currentDate.AddDays(1);
            }

            return new AppointmentOptimizationResponseDto
            {
                Suggestions = suggestions.OrderByDescending(s => s.SuitabilityScore).ToList(),
                Explanation = $"Personel yükü ve müşteri tercihlerine göre {suggestions.Count} randevu saati önerildi."
            };
        }

        private AppointmentOptimizationResponseDto ParseAIResponse(
            string aiResponse, 
            AppointmentOptimizationRequestDto request,
            AvailabilityAnalysisData analysisData)
        {
            try
            {
                // AI yanıtından JSON çıkar (markdown code block içinde olabilir)
                var jsonStart = aiResponse.IndexOf('{');
                var jsonEnd = aiResponse.LastIndexOf('}') + 1;
                if (jsonStart >= 0 && jsonEnd > jsonStart)
                {
                    var json = aiResponse.Substring(jsonStart, jsonEnd - jsonStart);
                    var parsed = JsonSerializer.Deserialize<JsonElement>(json);

                    var suggestions = new List<AppointmentSuggestionDto>();

                    if (parsed.TryGetProperty("suggestions", out var suggestionsArray))
                    {
                        foreach (var suggestion in suggestionsArray.EnumerateArray())
                        {
                            if (suggestion.TryGetProperty("date", out var dateProp) &&
                                suggestion.TryGetProperty("startTime", out var startTimeProp) &&
                                DateTime.TryParse(dateProp.GetString(), out var date) &&
                                TimeSpan.TryParse(startTimeProp.GetString(), out var startTime))
                            {
                                var endTime = startTime.Add(TimeSpan.FromMinutes(request.EstimatedDurationMinutes ?? 60));
                                if (suggestion.TryGetProperty("endTime", out var endTimeProp))
                                {
                                    if (TimeSpan.TryParse(endTimeProp.GetString(), out var parsedEndTime))
                                    {
                                        endTime = parsedEndTime;
                                    }
                                }

                                int? employeeId = null;
                                if (suggestion.TryGetProperty("employeeId", out var empIdProp) && empIdProp.ValueKind != JsonValueKind.Null)
                                {
                                    employeeId = empIdProp.GetInt32();
                                }

                                var score = 50;
                                if (suggestion.TryGetProperty("score", out var scoreProp))
                                {
                                    score = scoreProp.GetInt32();
                                }

                                var reason = "AI önerisi";
                                if (suggestion.TryGetProperty("reason", out var reasonProp))
                                {
                                    reason = reasonProp.GetString() ?? reason;
                                }

                                var employee = employeeId.HasValue 
                                    ? analysisData.AvailableEmployees.FirstOrDefault(e => e.Id == employeeId.Value)
                                    : analysisData.AvailableEmployees.OrderBy(e => analysisData.EmployeeWorkloads.GetValueOrDefault(e.Id, 0)).FirstOrDefault();

                                suggestions.Add(new AppointmentSuggestionDto
                                {
                                    SuggestedDate = date,
                                    SuggestedStartTime = startTime,
                                    SuggestedEndTime = endTime,
                                    SuggestedEmployeeId = employee?.Id,
                                    SuggestedEmployeeName = employee?.FullName,
                                    SuitabilityScore = score,
                                    Reason = reason,
                                    EmployeeWorkload = analysisData.EmployeeWorkloads.GetValueOrDefault(employee?.Id ?? 0, 0)
                                });
                            }
                        }
                    }

                    return new AppointmentOptimizationResponseDto
                    {
                        Suggestions = suggestions,
                        Explanation = "AI destekli optimizasyon tamamlandı."
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to parse AI response, falling back to heuristic");
            }

            // Fallback to heuristic
            return GetHeuristicOptimization(request, analysisData);
        }

        private int CalculateSuitabilityScore(
            DateTime date, 
            TimeSpan time, 
            int preferredHour, 
            int employeeWorkload)
        {
            var score = 100;

            // Tarih yakınlığı (bugünden uzak = daha düşük skor)
            var daysFromNow = (date - DateTime.UtcNow.Date).Days;
            if (daysFromNow > 7)
                score -= 10;
            if (daysFromNow > 14)
                score -= 10;

            // Saat tercihi
            var hourDiff = Math.Abs(time.Hours - preferredHour);
            score -= hourDiff * 5;

            // Personel yükü (daha az yüklü = daha yüksek skor)
            score -= employeeWorkload * 2;

            return Math.Max(0, Math.Min(100, score));
        }

        private string GetSuggestionReason(
            DateTime date, 
            TimeSpan time, 
            Domain.Entities.Employee? employee, 
            int score)
        {
            var reasons = new List<string>();

            if (date.Date == DateTime.UtcNow.Date.AddDays(1))
                reasons.Add("Yarın müsait");
            else if (date.Date <= DateTime.UtcNow.Date.AddDays(3))
                reasons.Add("Yakın tarih");

            if (time.Hours >= 9 && time.Hours <= 11)
                reasons.Add("Sabah saatleri");
            else if (time.Hours >= 14 && time.Hours <= 16)
                reasons.Add("Öğleden sonra");

            if (employee != null)
                reasons.Add($"{employee.FullName} müsait");

            return string.Join(", ", reasons) + $" (Skor: {score})";
        }

        private class AvailabilityAnalysisData
        {
            public List<Domain.Entities.Appointment> ExistingAppointments { get; set; } = new();
            public List<Domain.Entities.Employee> AvailableEmployees { get; set; } = new();
            public Dictionary<int, int> EmployeeWorkloads { get; set; } = new();
            public int? PreferredHour { get; set; }
        }
    }
}
