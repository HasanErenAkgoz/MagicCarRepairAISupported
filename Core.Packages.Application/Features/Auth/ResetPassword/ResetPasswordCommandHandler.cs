using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Auth.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, IResult>
    {
        private readonly UserManager<UserEntity> _userManager;

        public ResetPasswordCommandHandler(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return new ErrorResult("Bu e-posta adresine sahip bir kullan�c� bulunamad�.");

            if (request.NewPassword == request.ConfirmPassword)
            {

                var resetResult = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
                if (!resetResult.Succeeded)
                    return new ErrorResult(string.Join(", ", resetResult.Errors.Select(e => e.Description)));

                return new SuccessResult("�ifreniz ba�ar�yla g�ncellendi.");
            }
            return new ErrorResult("�ifreler uyu�muyor.");

        }
    }
}
