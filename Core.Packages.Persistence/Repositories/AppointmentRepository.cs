using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class AppointmentRepository : EfEntityRepository<Appointment, BaseDbContext>, IAppointmentRepository
    {
        public AppointmentRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<Appointment?> GetByIdAsync(int id)
        {
            return await Context.Set<Appointment>().FindAsync(id);
        }

        public async Task<Appointment?> GetByAppointmentNumberAsync(string appointmentNumber, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Appointment>()
                .FirstOrDefaultAsync(a => a.AppointmentNumber == appointmentNumber, cancellationToken);
        }

        public async Task<List<Appointment>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Appointment>()
                .Where(a => a.CustomerId == customerId)
                .OrderByDescending(a => a.AppointmentDate)
                .ThenByDescending(a => a.StartTime)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Appointment>> GetByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Appointment>()
                .Where(a => a.AssignedEmployeeId == employeeId)
                .OrderBy(a => a.AppointmentDate)
                .ThenBy(a => a.StartTime)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Appointment>> GetByDateAsync(DateTime date, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Appointment>()
                .Where(a => a.AppointmentDate.Date == date.Date)
                .OrderBy(a => a.StartTime)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Appointment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Appointment>()
                .Where(a => a.AppointmentDate.Date >= startDate.Date && a.AppointmentDate.Date <= endDate.Date)
                .OrderBy(a => a.AppointmentDate)
                .ThenBy(a => a.StartTime)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Appointment>> GetByStatusAsync(AppointmentStatus status, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Appointment>()
                .Where(a => a.Status == status)
                .OrderBy(a => a.AppointmentDate)
                .ThenBy(a => a.StartTime)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Appointment>> GetUpcomingAppointmentsAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.Now;
            return await Context.Set<Appointment>()
                .Where(a => a.AppointmentDate.Date >= now.Date && 
                           (a.Status == AppointmentStatus.Scheduled || a.Status == AppointmentStatus.Confirmed))
                .OrderBy(a => a.AppointmentDate)
                .ThenBy(a => a.StartTime)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Appointment>> GetAppointmentsForReminderAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.Now;
            var tomorrow = now.AddDays(1);
            
            return await Context.Set<Appointment>()
                .Where(a => a.Status == AppointmentStatus.Scheduled &&
                           !a.ReminderSent &&
                           a.AppointmentDate.Date == tomorrow.Date)
                .ToListAsync(cancellationToken);
        }
    }
}






