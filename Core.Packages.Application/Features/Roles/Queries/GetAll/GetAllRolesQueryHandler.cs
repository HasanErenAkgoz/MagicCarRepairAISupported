using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Roles.Queries.GetAll
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IDataResult<List<GetAllRolesResponse>>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ITenantService _tenantService;
        private readonly UserManager<UserEntity> _userManager;

        public GetAllRolesQueryHandler(
            IRoleRepository roleRepository,
            ITenantService tenantService,
            UserManager<UserEntity> userManager)
        {
            _roleRepository = roleRepository;
            _tenantService = tenantService;
            _userManager = userManager;
        }

        public async Task<IDataResult<List<GetAllRolesResponse>>> Handle(
            GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var clientId = request.ClientId ?? _tenantService.GetRequiredClientId();

            var roles = await _roleRepository.Query()
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                .Where(r => r.ClientId == clientId)
                .OrderBy(r => r.Name)
                .ToListAsync(cancellationToken);

            var result = new List<GetAllRolesResponse>();
            foreach (var role in roles)
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name!);
                result.Add(new GetAllRolesResponse
                {
                    Id = role.Id,
                    Name = role.Name ?? string.Empty,
                    ClientId = role.ClientId,
                    PermissionCount = role.RolePermissions.Count,
                    Permissions = role.RolePermissions
                        .Where(rp => rp.Permission != null)
                        .Select(rp => new RolePermissionItem
                        {
                            Id = rp.Permission.Id,
                            Name = rp.Permission.Name,
                        }).ToList(),
                    UserCount = usersInRole.Count,
                });
            }

            return new SuccessDataResult<List<GetAllRolesResponse>>(result);
        }
    }
}
