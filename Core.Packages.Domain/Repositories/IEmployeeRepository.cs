using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IEmployeeRepository : IEntityRepository<Employee, int>
    {
        Task<Employee?> GetByEmployeeNoAsync(string employeeNo, CancellationToken cancellationToken = default);
        Task<List<Employee>> GetByPositionAsync(EmployeePosition position, CancellationToken cancellationToken = default);
        Task<List<Employee>> GetActiveEmployeesAsync(CancellationToken cancellationToken = default);
        Task<bool> IsEmployeeNoExistsAsync(string employeeNo, CancellationToken cancellationToken = default);
        Task DeleteAllByClientIdAsync(int clientId, CancellationToken cancellationToken = default);
        Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}

