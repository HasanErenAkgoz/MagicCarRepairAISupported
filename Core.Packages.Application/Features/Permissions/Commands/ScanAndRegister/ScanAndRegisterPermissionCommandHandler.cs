using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Auth.Register.Commands;
using MagicCarRepairAISupported.Application.Features.RolePermissions.Create;
using MagicCarRepairAISupported.Application.Features.Roles.Commands.Create;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MagicCarRepairAISupported.Application.Features.Permissions.Commands.ScanAndRegister
{
    public class ScanAndRegisterPermissionCommandHandler : IRequestHandler<ScanAndRegisterPermissionsCommand, Unit>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMediator _mediator;
        private readonly ITenantService _tenantService;
        
        public ScanAndRegisterPermissionCommandHandler(
            IPermissionRepository permissionRepository,
            IRolePermissionRepository rolePermissionRepository,
            RoleManager<Role> roleManager,
            IMediator mediator,
            ITenantService tenantService)
        {
            _permissionRepository = permissionRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _roleManager = roleManager;
            _mediator = mediator;
            _tenantService = tenantService;
        }

        public async Task<Unit> Handle(ScanAndRegisterPermissionsCommand request, CancellationToken cancellationToken)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var handlerTypes = assembly.GetTypes()
                .Where(t => t.Name.EndsWith("Handler") && t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
                .ToList();

            var clientId = _tenantService.GetCurrentClientId() ?? 1;
            
            // Create or get System Admin role
            IDataResult<int> roleResult = await _mediator.Send(new CreateRoleCommand { Name = "System Admin"});
            
            // Get role - if it already exists, ErrorDataResult will contain the role ID in Data property
            int roleId = roleResult.Data;
            if (roleId == 0)
            {
                // Role creation failed and no existing role ID returned
                return Unit.Value;
            }
            
            // Get role by name to ensure we have the correct role (FindByNameAsync may bypass global query filter)
            var role = await _roleManager.FindByNameAsync("SYSTEM ADMIN");
            if (role == null || role.Id != roleId)
            {
                // Fallback: try FindByIdAsync if FindByNameAsync didn't work
                role = await _roleManager.FindByIdAsync(roleId.ToString());
                if (role == null)
                {
                    return Unit.Value;
                }
            }
            
            // Verify role belongs to current client
            if (role.ClientId != clientId)
            {
                return Unit.Value;
            }
            
            // Create System Admin user
            var createdUser = await _mediator.Send(new RegisterCommand
            {
                FirstName = "System",
                LastName = "Admin",
                Email = "SystemAdmin@CorePackages.com",
                Password = "SystemAdmin123.",
                ConfirmPassword = "SystemAdmin123.",
                IdentityNo = "12345678910",
            });
            
            // Collect all new permissions first (batch approach for better performance)
            var newPermissions = new List<Domain.Entities.Permission>();
            var existingPermissionNames = _permissionRepository.Query()
                .Select(p => p.Name)
                .ToList();
            
            foreach (var handlerType in handlerTypes)
            {
                var handlerName = handlerType.Name
                    .Replace("Handler", "")
                    .Replace("Command", "")
                    .Replace("Query", "");

                // Check if permission exists (permissions are global, unique by name only)
                if (!existingPermissionNames.Contains(handlerName))
                {
                    var description = Regex.Replace(handlerName, "(?<=.)([A-Z])", " $1");
                    newPermissions.Add(new Domain.Entities.Permission
                    {
                        Name = handlerName,
                        Description = description
                    });
                }
            }
            
            // Deduplicate by name (multiple handlers may resolve to the same permission name)
            newPermissions = newPermissions
                .GroupBy(p => p.Name)
                .Select(g => g.First())
                .ToList();

            // Batch insert permissions (single database round-trip)
            if (newPermissions.Any())
            {
                await _permissionRepository.BulkAddAsync(newPermissions);
                
                // Get permission IDs after bulk insert (EFCore.BulkExtensions sets IDs automatically)
                // Re-fetch permissions by name to get their IDs
                var permissionNames = newPermissions.Select(p => p.Name).ToList();
                var insertedPermissions = _permissionRepository.Query()
                    .Where(p => permissionNames.Contains(p.Name))
                    .ToList();
                
                // Get existing role permissions to avoid duplicates
                var existingRolePermissions = _rolePermissionRepository.Query()
                    .Where(rp => rp.RoleId == role.Id && rp.ClientId == clientId)
                    .Select(rp => rp.PermissionId)
                    .ToList();
                
                // Create RolePermissions in batch (only for new permissions)
                var newRolePermissions = insertedPermissions
                    .Where(p => !existingRolePermissions.Contains(p.Id))
                    .Select(permission => new RolePermission
                    {
                        RoleId = role.Id,
                        PermissionId = permission.Id,
                        ClientId = clientId
                    })
                    .ToList();
                
                if (newRolePermissions.Any())
                {
                    await _rolePermissionRepository.BulkAddAsync(newRolePermissions);
                }
            }
        
            return Unit.Value;
        }
    }
}
