using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.UseRoles.Commands.RemoveFromUser
{
    public class RemoveRoleFromUserCommandHandler : IRequestHandler<RemoveRoleFromUserCommand, IResult>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ITenantService _tenantService;

        public RemoveRoleFromUserCommandHandler(
            UserManager<UserEntity> userManager,
            RoleManager<Role> roleManager,
            ITenantService tenantService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tenantService = tenantService;
        }

        public async Task<IResult> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
        {
            var clientId = request.ClientId ?? _tenantService.GetRequiredClientId();

            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            if (role == null)
                return new ErrorResult("Rol bulunamadı.");

            if (role.ClientId != clientId)
                return new ErrorResult("Bu rol bu işletmeye ait değil.");

            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return new ErrorResult("Kullanıcı bulunamadı.");

            var result = await _userManager.RemoveFromRoleAsync(user, role.Name!);
            if (!result.Succeeded)
                return new ErrorResult(string.Join(", ", result.Errors.Select(e => e.Description)));

            return new SuccessResult("Rol kullanıcıdan başarıyla kaldırıldı.");
        }
    }
}
