using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Appointments.Queries.GetAll
{
    public class GetAllAppointmentsQuery : IRequest<List<GetAllAppointmentsResponse>>
    {
        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Domain.Enums.AppointmentStatus? Status { get; set; }
    }
}






