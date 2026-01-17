using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Appointments.Commands.Cancel
{
    public class CancelAppointmentCommand : IRequest<CancelAppointmentResponse>
    {
        public int AppointmentId { get; set; }
        public string? CancellationReason { get; set; }
    }
}






