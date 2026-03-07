using MagicCarRepairAISupported.Application.Common.Services.JWT;
using MagicCarRepairAISupported.Application.Common.Services.Auth;
using MagicCarRepairAISupported.Application.Features.Auth.Login.Commands;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Auth.Commands.Complete2FALogin
{
    /// <summary>
    /// 2FA doğrulandıktan sonra login'i tamamlar ve token döner
    /// </summary>
    public class Complete2FALoginCommand : IRequest<IDataResult<AccessToken>>
    {
        public string Email { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }

    public class Complete2FALoginCommandHandler : IRequestHandler<Complete2FALoginCommand, IDataResult<AccessToken>>
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<Domain.Entities.User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly Application.Common.Services.Auth.ITwoFactorService _twoFactorService;

        public Complete2FALoginCommandHandler(
            Microsoft.AspNetCore.Identity.UserManager<Domain.Entities.User> userManager,
            ITokenService tokenService,
            Application.Common.Services.Auth.ITwoFactorService twoFactorService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _twoFactorService = twoFactorService;
        }

        public async Task<IDataResult<AccessToken>> Handle(Complete2FALoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new ErrorDataResult<AccessToken>("Kullanıcı bulunamadı.");
            }

            if (!user.TwoFactorEnabled || string.IsNullOrEmpty(user.TwoFactorSecret))
            {
                return new ErrorDataResult<AccessToken>("2FA aktif değil.");
            }

            // 2FA kodunu doğrula
            var isValid = await _twoFactorService.VerifyCodeAsync(user.TwoFactorSecret, request.Code);
            if (!isValid)
            {
                // Recovery code kontrolü
                if (!string.IsNullOrEmpty(user.RecoveryCodes))
                {
                    var recoveryCodes = System.Text.Json.JsonSerializer.Deserialize<List<string>>(user.RecoveryCodes) ?? new List<string>();
                    var recoveryValid = await _twoFactorService.VerifyRecoveryCodeAsync(recoveryCodes, request.Code);
                    
                    if (recoveryValid)
                    {
                        // Recovery code kullanıldı, listeyi güncelle
                        user.RecoveryCodes = System.Text.Json.JsonSerializer.Serialize(recoveryCodes);
                        await _userManager.UpdateAsync(user);
                    }
                    else
                    {
                        return new ErrorDataResult<AccessToken>("Geçersiz kod.");
                    }
                }
                else
                {
                    return new ErrorDataResult<AccessToken>("Geçersiz kod.");
                }
            }

            // Token oluştur
            var tokens = await _tokenService.CreateToken<AccessToken>(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            return new SuccessDataResult<AccessToken>(new AccessToken
            {
                Token = tokens.Token,
                RefreshToken = tokens.RefreshToken,
                Claims = tokens.Claims,
                Expiration = tokens.Expiration,
                User = new LoginUserInfo
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserType = user.UserType,
                    Roles = userRoles.ToList(),
                    ClientId = user.ClientId,
                    RequiresTwoFactor = false,
                    HasCompletedOnboarding = user.HasCompletedOnboarding
                }
            });
        }
    }
}
