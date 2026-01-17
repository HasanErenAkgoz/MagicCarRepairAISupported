using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Queries.OptimizeAppointments
{
    public class OptimizeAppointmentsQuery : IRequest<OptimizeAppointmentsResponse>
    {
        public int CustomerId { get; set; }
        public int? VehicleId { get; set; }
        public AppointmentType AppointmentType { get; set; }
        public DateTime? PreferredStartDate { get; set; }
        public DateTime? PreferredEndDate { get; set; }
        public TimeSpan? PreferredStartTime { get; set; }
        public TimeSpan? PreferredEndTime { get; set; }
        public int? EstimatedDurationMinutes { get; set; }
        public string? Priority { get; set; }
        public int NumberOfSuggestions { get; set; } = 5;
    }
}
