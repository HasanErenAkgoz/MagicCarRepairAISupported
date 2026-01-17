using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IInvoiceRepository : IEntityRepository<Invoice, int>
    {
        Task<Invoice?> GetByInvoiceNumberAsync(string invoiceNumber, CancellationToken cancellationToken = default);
        Task<List<Invoice>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<List<Invoice>> GetByStatusAsync(InvoiceStatus status, CancellationToken cancellationToken = default);
        Task<List<Invoice>> GetOverdueInvoicesAsync(CancellationToken cancellationToken = default);
        Task<List<Invoice>> GetByWorkOrderIdAsync(int workOrderId, CancellationToken cancellationToken = default);
        Task<List<Invoice>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default);
    }
}

