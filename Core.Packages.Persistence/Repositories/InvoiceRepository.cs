using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class InvoiceRepository : EfEntityRepository<Invoice, BaseDbContext>, IInvoiceRepository
    {
        public InvoiceRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<Invoice?> GetByIdAsync(int id)
        {
            return await Context.Set<Invoice>().FindAsync(id);
        }

        public async Task<Invoice?> GetByInvoiceNumberAsync(string invoiceNumber, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Invoice>()
                .FirstOrDefaultAsync(i => i.InvoiceNumber == invoiceNumber, cancellationToken);
        }

        public async Task<List<Invoice>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Invoice>()
                .Where(i => i.InvoiceDate >= startDate && i.InvoiceDate <= endDate)
                .OrderByDescending(i => i.InvoiceDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Invoice>> GetByStatusAsync(InvoiceStatus status, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Invoice>()
                .Where(i => i.Status == status)
                .OrderByDescending(i => i.InvoiceDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Invoice>> GetOverdueInvoicesAsync(CancellationToken cancellationToken = default)
        {
            return await Context.Set<Invoice>()
                .Where(i => i.DueDate.HasValue && 
                           i.DueDate.Value < DateTime.UtcNow && 
                           i.Status != InvoiceStatus.Paid)
                .OrderBy(i => i.DueDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Invoice>> GetByWorkOrderIdAsync(int workOrderId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Invoice>()
                .Where(i => i.WorkOrderId == workOrderId)
                .OrderByDescending(i => i.InvoiceDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Invoice>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Invoice>()
                .Where(i => i.CustomerId == customerId)
                .OrderByDescending(i => i.InvoiceDate)
                .ToListAsync(cancellationToken);
        }
    }
}

