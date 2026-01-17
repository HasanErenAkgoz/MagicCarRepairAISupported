using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;

namespace MagicCarRepairAISupported.Infrastructure.Services.AI
{
    /// <summary>
    /// Mock randevu optimizasyon servisi (test ve geliştirme için)
    /// </summary>
    public class MockAppointmentOptimizationService : IAppointmentOptimizationService
    {
        public async Task<AppointmentOptimizationResponseDto> OptimizeAppointmentsAsync(
            AppointmentOptimizationRequestDto request, 
            CancellationToken cancellationToken = default)
        {
            await Task.Delay(300, cancellationToken); // Simulate processing

            var suggestions = new List<AppointmentSuggestionDto>();
            var startDate = request.PreferredStartDate ?? DateTime.UtcNow.Date.AddDays(1);
            var duration = request.EstimatedDurationMinutes ?? 60;

            // Basit öneriler oluştur
            for (int i = 0; i < request.NumberOfSuggestions && i < 5; i++)
            {
                var date = startDate.AddDays(i);
                var startTime = request.PreferredStartTime ?? new TimeSpan(9 + i, 0, 0);
                var endTime = startTime.Add(TimeSpan.FromMinutes(duration));

                suggestions.Add(new AppointmentSuggestionDto
                {
                    SuggestedDate = date,
                    SuggestedStartTime = startTime,
                    SuggestedEndTime = endTime,
                    SuitabilityScore = 80 - (i * 5),
                    Reason = $"Önerilen randevu saati #{i + 1}",
                    EmployeeWorkload = 0
                });
            }

            return new AppointmentOptimizationResponseDto
            {
                Suggestions = suggestions,
                Explanation = "Mock optimizasyon: Basit saat önerileri oluşturuldu."
            };
        }
    }
}
