using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class QuoteRequestRepository : EfEntityRepository<QuoteRequest, BaseDbContext>, IQuoteRequestRepository
    {
        public QuoteRequestRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public new async Task<QuoteRequest?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await GetQuoteRequestDetailsAsync(id, cancellationToken);
        }

        public async Task<QuoteRequest?> GetQuoteRequestDetailsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await Context.Set<QuoteRequest>()
                .Include(qr => qr.Customer)
                .Include(qr => qr.Vehicle)
                .Include(qr => qr.Client)
                .Include(qr => qr.QuoteResponses)
                    .ThenInclude(qres => qres.Client)
                .FirstOrDefaultAsync(qr => qr.Id == id, cancellationToken);
        }

        public async Task<List<QuoteRequest>> GetOpenQuoteRequestsAsync(int? clientId = null, CancellationToken cancellationToken = default)
        {
            var query = Context.Set<QuoteRequest>()
                .Include(qr => qr.Customer)
                .Include(qr => qr.Vehicle)
                .Include(qr => qr.Client)
                .Where(qr => qr.Status == QuoteStatus.Open &&
                             qr.QuoteDeadline > DateTime.UtcNow);

            if (clientId.HasValue)
            {
                // Exclude requests from the same client
                query = query.Where(qr => qr.ClientId != clientId.Value);
            }

            return await query
                .OrderByDescending(qr => qr.CreatedDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<QuoteRequest>> GetQuoteRequestsByCustomerAsync(int customerId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<QuoteRequest>()
                .Include(qr => qr.Vehicle)
                .Include(qr => qr.Client)
                .Include(qr => qr.QuoteResponses)
                .Where(qr => qr.CustomerId == customerId)
                .OrderByDescending(qr => qr.CreatedDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<QuoteRequest>> GetQuoteRequestsByStatusAsync(QuoteStatus status, int? clientId = null, CancellationToken cancellationToken = default)
        {
            var query = Context.Set<QuoteRequest>()
                .Include(qr => qr.Customer)
                .Include(qr => qr.Vehicle)
                .Include(qr => qr.Client)
                .Where(qr => qr.Status == status);

            if (clientId.HasValue)
            {
                query = query.Where(qr => qr.ClientId == clientId.Value);
            }

            return await query
                .OrderByDescending(qr => qr.CreatedDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<QuoteRequest>> GetExpiredQuoteRequestsAsync(CancellationToken cancellationToken = default)
        {
            return await Context.Set<QuoteRequest>()
                .Where(qr => qr.Status == QuoteStatus.Open &&
                             qr.QuoteDeadline <= DateTime.UtcNow)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> HasClientSubmittedQuoteAsync(int quoteRequestId, int clientId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<QuoteResponse>()
                .AnyAsync(qres => qres.QuoteRequestId == quoteRequestId &&
                                 qres.ClientId == clientId,
                          cancellationToken);
        }
    }
}
