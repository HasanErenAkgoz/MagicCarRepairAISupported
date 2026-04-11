using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MagicCarRepairAISupported.Application.Features.RolePermissions.Create
{
    public class CreateRolePermissionCommandHandler : IRequestHandler<CreateRolePermissionCommand, IResult>
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly RoleManager<Role> _roleManager;
        private readonly ITenantService _tenantService;

        public CreateRolePermissionCommandHandler(
            IRolePermissionRepository rolePermissionRepository,
            RoleManager<Role> roleManager,
            ITenantService tenantService)
        {
            _rolePermissionRepository = rolePermissionRepository;
            _roleManager = roleManager;
            _tenantService = tenantService;
        }

        public async Task<IResult> Handle(CreateRolePermissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Get current ClientId (default to 1 if not set, for system operations)
                var clientId = request.ClientId ?? _tenantService.GetCurrentClientId() ?? 1;
                
                // Verify role exists and belongs to current client (security check)
                var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
                if (role == null)
                {
                    return new ErrorResult("Role not found");
                }
                
                if (role.ClientId != clientId)
                {
                    return new ErrorResult("Role does not belong to current client");
                }
                
                // Check if role permission exists with same RoleId, PermissionId and ClientId (multi-tenant unique constraint)
                var exists = _rolePermissionRepository.Query()
                    .Any(rp => rp.RoleId == request.RoleId && rp.PermissionId == request.PermissionId && rp.ClientId == clientId);
                
                if (!exists)
                {
                    await _rolePermissionRepository.AddAsync(new RolePermission 
                    { 
                        RoleId = request.RoleId, 
                        PermissionId = request.PermissionId,
                        ClientId = clientId
                    }, cancellationToken);
                    return new SuccessResult("Role Permission created successfully");
                }
                return new ErrorResult("Role Permission already exists");
            }
            catch (Exception ex) 
            {
                return new ErrorResult(ex.Message);
            }
        }
    }
}
