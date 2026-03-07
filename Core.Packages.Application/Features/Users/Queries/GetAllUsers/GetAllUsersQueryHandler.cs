using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IDataResult<List<GetAllUsersResponse>>>
    {
        private readonly UserManager<User> _userManager;

        public GetAllUsersQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IDataResult<List<GetAllUsersResponse>>> Handle(
            GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var usersQuery = _userManager.Users
                .Include(u => u.Client)
                .AsQueryable();

            if (request.ClientId.HasValue)
                usersQuery = usersQuery.Where(u => u.ClientId == request.ClientId.Value);

            if (request.UserType.HasValue)
                usersQuery = usersQuery.Where(u => (int)u.UserType == request.UserType.Value);

            var users = await usersQuery
                .OrderBy(u => u.UserType)
                .ThenBy(u => u.FirstName)
                .ToListAsync(cancellationToken);

            var result = new List<GetAllUsersResponse>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                result.Add(new GetAllUsersResponse
                {
                    Id = user.Id,
                    FirstName = user.FirstName ?? string.Empty,
                    LastName = user.LastName ?? string.Empty,
                    FullName = $"{user.FirstName} {user.LastName}".Trim(),
                    Email = user.Email ?? string.Empty,
                    PhoneNumber = user.PhoneNumber,
                    UserType = (int)user.UserType,
                    UserTypeLabel = GetUserTypeLabel(user.UserType),
                    ClientId = user.ClientId,
                    ClientName = user.Client?.Name,
                    IsActive = !user.LockoutEnabled || user.LockoutEnd == null || user.LockoutEnd < DateTimeOffset.UtcNow,
                    Roles = roles.ToList()
                });
            }

            return new SuccessDataResult<List<GetAllUsersResponse>>(result);
        }

        private static string GetUserTypeLabel(UserType userType) => userType switch
        {
            UserType.SystemAdmin => "Sistem Yöneticisi",
            UserType.Manager    => "Tamirhane Sahibi",
            UserType.Employee   => "Çalışan",
            UserType.Customer   => "Müşteri",
            _                   => "Bilinmiyor"
        };
    }
}
