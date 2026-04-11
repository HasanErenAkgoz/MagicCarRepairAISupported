using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Auth;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Identity;

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
                var clientId = request.ClientId ?? _tenantService.GetCurrentClientId() ?? 1;
                
                // Check if role exists with same Name and ClientId (multi-tenant unique constraint)
                // RoleManager.FindByNameAsync uses NormalizedName and may bypass global query filter
                // But we need to check ClientId manually, so we use repository with IgnoreQueryFilters equivalent
                // Since Query() applies global filter, we need to check by NormalizedName first, then ClientId
                var normalizedName = request.Name.ToUpperInvariant();
                var existingRole = await _roleManager.FindByNameAsync(normalizedName);
                
                // If role exists, verify it belongs to current client
                if (existingRole != null && existingRole.ClientId == clientId)
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
