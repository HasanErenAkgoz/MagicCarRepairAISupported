using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    /// <summary>
    /// QuoteRequest repository interface
    /// </summary>
    public interface IQuoteRequestRepository : IEntityRepository<QuoteRequest, int>
    {
        /// <summary>
        /// Get quote request with all related data
        /// </summary>
        Task<QuoteRequest?> GetQuoteRequestDetailsAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get open quote requests (can accept quotes)
        /// </summary>
        Task<List<QuoteRequest>> GetOpenQuoteRequestsAsync(int? clientId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get quote requests by customer
        /// </summary>
        Task<List<QuoteRequest>> GetQuoteRequestsByCustomerAsync(int customerId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get quote requests by status
        /// </summary>
        Task<List<QuoteRequest>> GetQuoteRequestsByStatusAsync(QuoteStatus status, int? clientId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get expired quote requests
        /// </summary>
        Task<List<QuoteRequest>> GetExpiredQuoteRequestsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Check if client already submitted a quote for this request
        /// </summary>
        Task<bool> HasClientSubmittedQuoteAsync(int quoteRequestId, int clientId, CancellationToken cancellationToken = default);
    }
}

