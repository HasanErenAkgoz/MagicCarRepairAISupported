using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class ErrorMessageRepository : EfEntityRepository<ErrorMessage, BaseDbContext>, IErrorMessageRepository
    {
        public ErrorMessageRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public new async Task<ErrorMessage?> GetByIdAsync(int id)
        {
            return await Context.ErrorMessages.FindAsync(id);
        }

        public async Task<ErrorMessage?> GetByCodeAndLanguageAsync(string errorCode, string language)
        {
            return await Context.ErrorMessages
                .FirstOrDefaultAsync(e => e.ErrorCode == errorCode && e.Language == language);
        }

        public async Task<List<ErrorMessage>> GetByLanguageAsync(string language)
        {
            return await Context.ErrorMessages
                .Where(e => e.Language == language)
                .ToListAsync();
        }
    }
}

