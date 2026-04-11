using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Auth.Commands.Disable2FA
{
    public class Disable2FACommandHandler : IRequestHandler<Disable2FACommand, IDataResult<Disable2FAResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<UserEntity> _userManager;

        public Disable2FACommandHandler(
            IUserRepository userRepository,
            UserManager<UserEntity> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<IDataResult<Disable2FAResponse>> Handle(Disable2FACommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
            {
                return new ErrorDataResult<Disable2FAResponse>("User not found.");
            }

            // Zaten pasif ise hata döndür
            if (!user.TwoFactorEnabled)
            {
                return new ErrorDataResult<Disable2FAResponse>("Two-factor authentication is already disabled.");
            }

            // Şifre doğrulaması (opsiyonel ama önerilir)
            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
                if (!passwordValid)
                {
                    return new ErrorDataResult<Disable2FAResponse>("Invalid password. Please try again.");
                }
            }

            // 2FA'yı devre dışı bırak
            user.TwoFactorEnabled = false;
            user.TwoFactorSecret = null; // Secret'ı temizle
            user.RecoveryCodes = null; // Recovery codes'ları temizle

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            var response = new Disable2FAResponse
            {
                RequiresTwoFactor = false
            };

            return new SuccessDataResult<Disable2FAResponse>(response, "Two-factor authentication has been disabled successfully");
        }
    }
}
