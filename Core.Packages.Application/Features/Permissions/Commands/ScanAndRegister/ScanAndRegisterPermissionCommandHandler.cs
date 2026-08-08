using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Auth.Register.Commands;
using MagicCarRepairAISupported.Application.Features.Roles.Commands.Create;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MagicCarRepairAISupported.Application.Features.Permissions.Commands.ScanAndRegister
{
    public class ScanAndRegisterPermissionCommandHandler : IRequestHandler<ScanAndRegisterPermissionsCommand, Unit>
    {
        private const string SystemAdminRoleName = "System Admin";

        private readonly IPermissionRepository _permissionRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMediator _mediator;
        private readonly ITenantService _tenantService;
        
        public ScanAndRegisterPermissionCommandHandler(
            IPermissionRepository permissionRepository,
            IRolePermissionRepository rolePermissionRepository,
            IRoleRepository roleRepository,
            IMediator mediator,
            ITenantService tenantService)
        {
            _permissionRepository = permissionRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _roleRepository = roleRepository;
            _mediator = mediator;
            _tenantService = tenantService;
        }

        public async Task<Unit> Handle(ScanAndRegisterPermissionsCommand request, CancellationToken cancellationToken)
        {
            if (request.PermissionsOnly)
            {
                await ScanAndInsertPermissionsAsync(cancellationToken);
                return Unit.Value;
            }

            var clientId = request.ClientId ?? _tenantService.GetRequiredClientId();
            _tenantService.SetCurrentClientId(clientId);

            await ScanAndInsertPermissionsAsync(cancellationToken);
            await LinkPermissionsToSystemAdminRoleAsync(clientId, request.SkipBootstrapUser, cancellationToken);

            return Unit.Value;
        }

        private async Task ScanAndInsertPermissionsAsync(CancellationToken cancellationToken)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var handlerTypes = assembly.GetTypes()
                .Where(t => t.Name.EndsWith("Handler") && t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
                .ToList();

            var existingPermissionNames = await _permissionRepository.Query()
                .Select(p => p.Name)
                .ToListAsync(cancellationToken);

            var newPermissions = new List<Domain.Entities.Permission>();

            foreach (var handlerType in handlerTypes)
            {
                var handlerName = handlerType.Name
                    .Replace("Handler", "")
                    .Replace("Command", "")
                    .Replace("Query", "");

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

            newPermissions = newPermissions
                .GroupBy(p => p.Name)
                .Select(g => g.First())
                .ToList();

            if (newPermissions.Count == 0)
                return;

            await _permissionRepository.BulkAddAsync(newPermissions);
        }

        private async Task LinkPermissionsToSystemAdminRoleAsync(
            int clientId,
            bool skipBootstrapUser,
            CancellationToken cancellationToken)
        {
            IDataResult<int> roleResult = await _mediator.Send(
                new CreateRoleCommand { Name = SystemAdminRoleName, ClientId = clientId },
                cancellationToken);

            var roleId = roleResult.Data;
            if (roleId == 0)
                return;

            var role = await _roleRepository.Query()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(r => r.Id == roleId && r.ClientId == clientId, cancellationToken);

            if (role == null)
                return;

            if (!skipBootstrapUser)
            {
                await _mediator.Send(new RegisterCommand
                {
                    FirstName = "System",
                    LastName = "Admin",
                    Email = "SystemAdmin@CorePackages.com",
                    Password = "SystemAdmin123.",
                    ConfirmPassword = "SystemAdmin123.",
                    IdentityNo = "12345678910",
                }, cancellationToken);
            }

            var allPermissions = await _permissionRepository.Query()
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var existingRolePermissions = await _rolePermissionRepository.Query()
                .Where(rp => rp.RoleId == role.Id && rp.ClientId == clientId)
                .Select(rp => rp.PermissionId)
                .ToListAsync(cancellationToken);

            var newRolePermissions = allPermissions
                .Where(p => !existingRolePermissions.Contains(p.Id))
                .Select(permission => new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = permission.Id,
                    ClientId = clientId
                })
                .ToList();

            if (newRolePermissions.Count > 0)
                await _rolePermissionRepository.BulkAddAsync(newRolePermissions);
        }
    }
}
