using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Roles.Commands.Delete
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, IResult>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly ITenantService _tenantService;

        public DeleteRoleCommandHandler(
            RoleManager<Role> roleManager,
            UserManager<UserEntity> userManager,
            ITenantService tenantService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _tenantService = tenantService;
        }

        public async Task<IResult> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var clientId = request.ClientId ?? _tenantService.GetRequiredClientId();

            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            if (role == null)
                return new ErrorResult("Rol bulunamadı.");

            if (role.ClientId != clientId)
                return new ErrorResult("Bu rol bu işletmeye ait değil.");

            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name!);
            if (usersInRole.Any())
                return new ErrorResult($"Bu role atanmış {usersInRole.Count} kullanıcı var. Önce kullanıcılardan rolü kaldırın.");

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
                return new ErrorResult(string.Join(", ", result.Errors.Select(e => e.Description)));

            return new SuccessResult("Rol başarıyla silindi.");
        }
    }
}
