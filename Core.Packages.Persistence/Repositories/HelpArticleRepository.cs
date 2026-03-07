using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class HelpArticleRepository : EfEntityRepository<HelpArticle, BaseDbContext>, IHelpArticleRepository
    {
        public HelpArticleRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<List<HelpArticle>> GetPublishedArticlesAsync(string? category = null, CancellationToken cancellationToken = default)
        {
            var query = Context.Set<HelpArticle>()
                .Where(a => a.IsPublished)
                .AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(a => a.Category == category);
            }

            return await query.OrderBy(a => a.Order).ThenBy(a => a.Title).ToListAsync(cancellationToken);
        }

        public async Task<HelpArticle?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await Context.Set<HelpArticle>()
                .FirstOrDefaultAsync(a => a.Id == id && a.IsPublished, cancellationToken);
        }
    }
}
