namespace MagicCarRepairAISupported.Application.Features.Appointments.Queries.GetAll
{
    public class GetAllAppointmentsResponse
    {
        public int Id { get; set; }
        public string AppointmentNumber { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int? VehicleId { get; set; }
        public string? VehicleLicensePlate { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string AppointmentType { get; set; }
        public string Status { get; set; }
        public int? AssignedEmployeeId { get; set; }
        public string? AssignedEmployeeName { get; set; }
    }
}






