using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Auth.SetEmployeePassword
{
    public class SetEmployeePasswordCommandHandler : IRequestHandler<SetEmployeePasswordCommand, IResult>
    {
        private readonly UserManager<UserEntity> _userManager;

        public SetEmployeePasswordCommandHandler(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IResult> Handle(SetEmployeePasswordCommand request, CancellationToken cancellationToken)
        {
            if (request.NewPassword != request.ConfirmPassword)
                return new ErrorResult("Şifreler eşleşmiyor.");

            var email = (request.Email ?? string.Empty).Trim();
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new ErrorResult("Kullanıcı bulunamadı.");

            var token = (request.Token ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(token))
                return new ErrorResult("Token zorunludur.");

            // Identity, token’ı GeneratePasswordResetTokenAsync çıktısı ile birebir karşılaştırır.
            // Ekstra Uri.Unescape / “normalize” adımları geçerli token’ı bozup Invalid token üretebilir (özellikle web formu).
            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);

            if (!result.Succeeded)
                return new ErrorResult(string.Join(", ", result.Errors.Select(e => e.Description)));

            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);

            return new SuccessResult("Şifreniz başarıyla belirlendi. Giriş yapabilirsiniz.");
        }
    }
}
