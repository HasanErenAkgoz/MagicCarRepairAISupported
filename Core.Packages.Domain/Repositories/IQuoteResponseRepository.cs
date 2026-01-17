using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    /// <summary>
    /// QuoteResponse repository interface
    /// </summary>
    public interface IQuoteResponseRepository : IEntityRepository<QuoteResponse, int>
    {
        /// <summary>
        /// Get quote response with all related data
        /// </summary>
        Task<QuoteResponse?> GetQuoteResponseDetailsAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get quote responses by request
        /// </summary>
        Task<List<QuoteResponse>> GetQuoteResponsesByRequestAsync(int quoteRequestId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get quote responses by client
        /// </summary>
        Task<List<QuoteResponse>> GetQuoteResponsesByClientAsync(int clientId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get quote responses by status
        /// </summary>
        Task<List<QuoteResponse>> GetQuoteResponsesByStatusAsync(QuoteResponseStatus status, int? clientId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get accepted quote response for a request
        /// </summary>
        Task<QuoteResponse?> GetAcceptedQuoteResponseAsync(int quoteRequestId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get expired quote responses
        /// </summary>
        Task<List<QuoteResponse>> GetExpiredQuoteResponsesAsync(CancellationToken cancellationToken = default);
    }
}

