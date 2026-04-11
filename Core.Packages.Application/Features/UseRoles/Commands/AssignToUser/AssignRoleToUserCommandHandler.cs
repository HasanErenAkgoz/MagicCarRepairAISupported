using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.UseRoles.Commands.AssignToUser
{
    public class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommand, IResult>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ITenantService _tenantService;

        public AssignRoleToUserCommandHandler(
            UserManager<UserEntity> userManager,
            RoleManager<Role> roleManager,
            ITenantService tenantService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tenantService = tenantService;
        }

        public async Task<IResult> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var clientId = request.ClientId ?? _tenantService.GetCurrentClientId() ?? 1;

            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            if (role == null)
                return new ErrorResult("Rol bulunamadı.");

            if (role.ClientId != clientId)
                return new ErrorResult("Bu rol bu işletmeye ait değil.");

            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return new ErrorResult("Kullanıcı bulunamadı.");

            if (user.ClientId != clientId)
                return new ErrorResult("Bu kullanıcı bu işletmeye ait değil.");

            var isAlreadyInRole = await _userManager.IsInRoleAsync(user, role.Name!);
            if (isAlreadyInRole)
                return new ErrorResult("Kullanıcı zaten bu rolde.");

            var result = await _userManager.AddToRoleAsync(user, role.Name!);
            if (!result.Succeeded)
                return new ErrorResult(string.Join(", ", result.Errors.Select(e => e.Description)));

            return new SuccessResult("Rol kullanıcıya başarıyla atandı.");
        }
    }
}
