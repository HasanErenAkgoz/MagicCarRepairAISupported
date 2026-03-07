using MagicCarRepairAISupported.Domain.Entities;

namespace MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore
{
    public interface IHelpArticleRepository : IEntityRepository<HelpArticle>
    {
        Task<List<HelpArticle>> GetPublishedArticlesAsync(string? category = null, CancellationToken cancellationToken = default);
        Task<HelpArticle?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
