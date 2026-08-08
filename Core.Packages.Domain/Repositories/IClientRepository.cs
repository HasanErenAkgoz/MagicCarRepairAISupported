using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IClientRepository : IEntityRepository<Client, int>
    {
        /// <summary>
        /// Login/registration — tenant context and query filters must not hide the client row.
        /// </summary>
        Task<Client?> GetByIdForAuthAsync(int id, CancellationToken cancellationToken = default);

        Task<Client?> GetByCodeAsync(string code);
        Task<bool> IsCodeUniqueAsync(string code, int? excludeId = null);

        /// <summary>
        /// Removes or clears data that references AspNetUsers (chat messages, reminders, customer/employee UserId)
        /// so Identity user deletion can succeed. Call before UserManager.DeleteAsync; then call <see cref="DeleteCascadeAsync"/>.
        /// </summary>
        Task PrepareForClientUserDeletionAsync(int clientId, IReadOnlyList<int> userIds, CancellationToken cancellationToken);

        Task DeleteCascadeAsync(int clientId, IEnumerable<int> userIds, CancellationToken cancellationToken);
    }
}

