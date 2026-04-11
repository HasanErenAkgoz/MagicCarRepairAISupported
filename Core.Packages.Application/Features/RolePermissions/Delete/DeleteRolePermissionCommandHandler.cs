using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MagicCarRepairAISupported.Application.Features.RolePermissions.Delete
{
    public class DeleteRolePermissionCommandHandler : IRequestHandler<DeleteRolePermissionCommand, IResult>
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly RoleManager<Role> _roleManager;
        private readonly ITenantService _tenantService;

        public DeleteRolePermissionCommandHandler(
            IRolePermissionRepository rolePermissionRepository,
            RoleManager<Role> roleManager,
            ITenantService tenantService)
        {
            _rolePermissionRepository = rolePermissionRepository;
            _roleManager = roleManager;
            _tenantService = tenantService;
        }

        public async Task<IResult> Handle(DeleteRolePermissionCommand request, CancellationToken cancellationToken)
        {
            var clientId = request.ClientId ?? _tenantService.GetCurrentClientId() ?? 1;

            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            if (role == null)
                return new ErrorResult("Rol bulunamadı.");

            if (role.ClientId != clientId)
                return new ErrorResult("Bu rol bu işletmeye ait değil.");

            var rolePermission = _rolePermissionRepository.Query()
                .FirstOrDefault(rp =>
                    rp.RoleId == request.RoleId &&
                    rp.PermissionId == request.PermissionId &&
                    rp.ClientId == clientId);

            if (rolePermission == null)
                return new ErrorResult("Rol izni bulunamadı.");

            _rolePermissionRepository.Delete(rolePermission);
            await _rolePermissionRepository.SaveChangesAsync();

            return new SuccessResult("Rol izni başarıyla kaldırıldı.");
        }
    }
}
