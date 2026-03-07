using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class QuoteResponseRepository : EfEntityRepository<QuoteResponse, BaseDbContext>, IQuoteResponseRepository
    {
        public QuoteResponseRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public new async Task<QuoteResponse?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await GetQuoteResponseDetailsAsync(id, cancellationToken);
        }

        public async Task<QuoteResponse?> GetQuoteResponseDetailsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await Context.Set<QuoteResponse>()
                .Include(qres => qres.QuoteRequest)
                    .ThenInclude(qr => qr.Customer)
                .Include(qres => qres.QuoteRequest)
                    .ThenInclude(qr => qr.Vehicle)
                .Include(qres => qres.Client)
                .FirstOrDefaultAsync(qres => qres.Id == id, cancellationToken);
        }

        public async Task<List<QuoteResponse>> GetQuoteResponsesByRequestAsync(int quoteRequestId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<QuoteResponse>()
                .Include(qres => qres.Client)
                .Where(qres => qres.QuoteRequestId == quoteRequestId)
                .OrderByDescending(qres => qres.QuoteDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<QuoteResponse>> GetQuoteResponsesByClientAsync(int clientId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<QuoteResponse>()
                .Include(qres => qres.QuoteRequest)
                    .ThenInclude(qr => qr.Customer)
                .Include(qres => qres.QuoteRequest)
                    .ThenInclude(qr => qr.Vehicle)
                .Where(qres => qres.ClientId == clientId)
                .OrderByDescending(qres => qres.QuoteDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<QuoteResponse>> GetQuoteResponsesByStatusAsync(QuoteResponseStatus status, int? clientId = null, CancellationToken cancellationToken = default)
        {
            var statusStr = status.ToString();
            var query = Context.Set<QuoteResponse>()
                .Include(qres => qres.QuoteRequest)
                .Include(qres => qres.Client)
                .Where(qres => qres.Status == statusStr);

            if (clientId.HasValue)
            {
                query = query.Where(qres => qres.ClientId == clientId.Value);
            }

            return await query
                .OrderByDescending(qres => qres.QuoteDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<QuoteResponse?> GetAcceptedQuoteResponseAsync(int quoteRequestId, CancellationToken cancellationToken = default)
        {
            return await Context.Set<QuoteResponse>()
                .Include(qres => qres.Client)
                .Include(qres => qres.QuoteRequest)
                .FirstOrDefaultAsync(qres => qres.QuoteRequestId == quoteRequestId &&
                                             qres.Status == "Accepted", cancellationToken);
        }

        public async Task<List<QuoteResponse>> GetExpiredQuoteResponsesAsync(CancellationToken cancellationToken = default)
        {
            return await Context.Set<QuoteResponse>()
                .Where(qres => qres.Status == "Pending" &&
                               qres.ValidUntilDate <= DateTime.UtcNow)
                .ToListAsync(cancellationToken);
        }
    }
}
