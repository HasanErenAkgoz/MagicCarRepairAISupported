using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IAppointmentRepository : IEntityRepository<Appointment, int>
    {
        Task<Appointment?> GetByAppointmentNumberAsync(string appointmentNumber, CancellationToken cancellationToken = default);
        Task<List<Appointment>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default);
        Task<List<Appointment>> GetByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default);
        Task<List<Appointment>> GetByDateAsync(DateTime date, CancellationToken cancellationToken = default);
        Task<List<Appointment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<List<Appointment>> GetByStatusAsync(AppointmentStatus status, CancellationToken cancellationToken = default);
        Task<List<Appointment>> GetUpcomingAppointmentsAsync(CancellationToken cancellationToken = default);
        Task<List<Appointment>> GetAppointmentsForReminderAsync(CancellationToken cancellationToken = default);
    }
}






