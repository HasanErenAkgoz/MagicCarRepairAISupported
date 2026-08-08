using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Auth;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Roles.Commands.Create
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, IDataResult<int>>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IRoleRepository _roleRepository;
        private readonly ITenantService _tenantService;
        
        public CreateRoleCommandHandler(
            RoleManager<Role> roleManager, 
            IAuthenticationService authenticationService,
            IRoleRepository roleRepository,
            ITenantService tenantService)
        {
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _tenantService = tenantService;
        }

        public async Task<IDataResult<int>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Get current ClientId (default to 1 if not set, for system operations)
                var clientId = request.ClientId ?? _tenantService.GetRequiredClientId();
                
                var normalizedName = request.Name.ToUpperInvariant();
                var existingRole = await _roleRepository.Query()
                    .IgnoreQueryFilters()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(r => r.NormalizedName == normalizedName && r.ClientId == clientId, cancellationToken);

                if (existingRole != null)
                {
                    return new ErrorDataResult<int>(existingRole.Id, "Role already exists");
                }
                
                Role role = new Role 
                { 
                    Name = request.Name,
                    NormalizedName = normalizedName,
                    ClientId = clientId,
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return new SuccessDataResult<int>(role.Id, "Role created successfully");
                }
                return new ErrorDataResult<int>(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<int>(ex.Message);
            }
           
        }
    }
}
