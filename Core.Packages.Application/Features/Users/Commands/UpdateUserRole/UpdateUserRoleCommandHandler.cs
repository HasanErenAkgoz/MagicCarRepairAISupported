using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Application.Features.Users.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, IResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UpdateUserRoleCommandHandler> _logger;

        public UpdateUserRoleCommandHandler(
            UserManager<User> userManager,
            ILogger<UpdateUserRoleCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IResult> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            if (!Enum.IsDefined(typeof(UserType), request.UserType))
                return new ErrorResult("Geçersiz rol değeri. (1=SystemAdmin, 2=Manager, 3=Employee, 4=Customer)");

            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return new ErrorResult("Kullanıcı bulunamadı.");

            // Manager yalnızca kendi clientId'sindeki kullanıcıları değiştirebilir
            if (request.CallerClientId.HasValue && user.ClientId != request.CallerClientId.Value)
                return new ErrorResult("Bu kullanıcının rolünü değiştirme yetkiniz yok.");

            var oldType = user.UserType;
            user.UserType = (UserType)request.UserType;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                var errors = string.Join(", ", updateResult.Errors.Select(e => e.Description));
                return new ErrorResult($"Güncelleme başarısız: {errors}");
            }

            _logger.LogInformation(
                "UserType updated: UserId={UserId}, {OldType} → {NewType}",
                user.Id, oldType, (UserType)request.UserType);

            return new SuccessResult("Kullanıcı rolü başarıyla güncellendi.");
        }
    }
}
