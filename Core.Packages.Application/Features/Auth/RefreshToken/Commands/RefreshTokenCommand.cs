using MagicCarRepairAISupported.Application.Common.Models.JWT;
using MagicCarRepairAISupported.Application.Common.Services.JWT;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Auth.RefreshToken.Commands
{
    public class RefreshTokenCommand : IRequest<IDataResult<RefreshTokenResponse>>
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }

    public class RefreshTokenResponse
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, IDataResult<RefreshTokenResponse>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ITokenService _tokenService;

        public RefreshTokenCommandHandler(UserManager<UserEntity> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<IDataResult<RefreshTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            // Expired token'dan email çıkar (expiry doğrulaması yapma)
            JwtSecurityToken? jwtToken;
            try
            {
                jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(request.Token);
            }
            catch
            {
                return new ErrorDataResult<RefreshTokenResponse>("Geçersiz token.");
            }

            var email = jwtToken.Claims
                .FirstOrDefault(c => c.Type == "email" || c.Type == System.Security.Claims.ClaimTypes.Email)
                ?.Value;

            if (string.IsNullOrEmpty(email))
                return new ErrorDataResult<RefreshTokenResponse>("Token geçersiz.");

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new ErrorDataResult<RefreshTokenResponse>("Kullanıcı bulunamadı.");

            if (user.RefreshToken != request.RefreshToken)
                return new ErrorDataResult<RefreshTokenResponse>("Refresh token geçersiz.");

            if (user.RefreshTokenExpiryTime == null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
                return new ErrorDataResult<RefreshTokenResponse>("Oturum süresi dolmuş. Lütfen tekrar giriş yapın.");

            // Yeni token üret
            var newTokens = await _tokenService.CreateToken<AccessToken>(user);

            // Refresh token rotasyonu — her kullanımda yeni refresh token
            user.RefreshToken = newTokens.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return new SuccessDataResult<RefreshTokenResponse>(new RefreshTokenResponse
            {
                Token = newTokens.Token,
                RefreshToken = newTokens.RefreshToken,
            });
        }
    }
}
