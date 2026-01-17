using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IErrorMessageRepository : IEntityRepository<ErrorMessage, int>
    {
        Task<ErrorMessage?> GetByCodeAndLanguageAsync(string errorCode, string language);
        Task<List<ErrorMessage>> GetByLanguageAsync(string language);
    }
}

