using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Appointments.Commands.Create
{
    public class CreateAppointmentCommand : IRequest<CreateAppointmentResponse>
    {
        public int CustomerId { get; set; }
        public int? VehicleId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public Domain.Enums.AppointmentType AppointmentType { get; set; }
        public string? Description { get; set; }
        public int? AssignedEmployeeId { get; set; }
    }
}






