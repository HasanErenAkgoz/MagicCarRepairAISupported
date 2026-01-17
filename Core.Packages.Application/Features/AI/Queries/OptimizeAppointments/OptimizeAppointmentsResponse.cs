namespace MagicCarRepairAISupported.Application.Features.AI.Queries.OptimizeAppointments
{
    public class OptimizeAppointmentsResponse
    {
        public List<AppointmentSuggestionDto> Suggestions { get; set; } = new();
        public string Explanation { get; set; } = string.Empty;
    }

    public class AppointmentSuggestionDto
    {
        public DateTime SuggestedDate { get; set; }
        public TimeSpan SuggestedStartTime { get; set; }
        public TimeSpan SuggestedEndTime { get; set; }
        public int? SuggestedEmployeeId { get; set; }
        public string? SuggestedEmployeeName { get; set; }
        public int SuitabilityScore { get; set; }
        public string Reason { get; set; } = string.Empty;
        public int EmployeeWorkload { get; set; }
    }
}
