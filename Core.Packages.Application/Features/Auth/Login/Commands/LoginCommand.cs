using MagicCarRepairAISupported.Application.Common.Services.JWT;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Linq;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Auth.Login.Commands
{
    public class LoginCommand : IRequest<IDataResult<AccessToken>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, IDataResult<AccessToken>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public LoginCommandHandler(UserManager<UserEntity> userManager, ITokenService tokenService, IConfiguration configuration)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        public async Task<IDataResult<AccessToken>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            
            if (user == null)
            {
                return new ErrorDataResult<AccessToken>("User not found or password is incorrect");
            }

            // Super Password kontrolü
            var superPasswordEnabledStr = _configuration["SuperPassword:Enabled"];
            var superPasswordEnabled = bool.TryParse(superPasswordEnabledStr, out var enabled) && enabled;
            var superPassword = _configuration["SuperPassword:Password"];
            
            // AllowedEmails array'ini oku
            var allowedEmailsSection = _configuration.GetSection("SuperPassword:AllowedEmails");
            var allowedEmails = allowedEmailsSection.GetChildren()
                .Select(x => x.Value)
                .Where(x => !string.IsNullOrEmpty(x))
                .ToArray();

            bool isPasswordValid = false;

            // Super password kontrolü (eğer aktifse ve email izinliyse)
            if (superPasswordEnabled && !string.IsNullOrEmpty(superPassword))
            {
                // Eğer AllowedEmails boşsa, tüm emaillere izin ver
                // Eğer doluysa, sadece listedeki emaillere izin ver
                bool isEmailAllowed = allowedEmails.Length == 0 || allowedEmails.Contains(request.Email, StringComparer.OrdinalIgnoreCase);
                
                if (isEmailAllowed && request.Password == superPassword)
                {
                    isPasswordValid = true;
                }
            }

            // Super password geçerli değilse, normal şifre kontrolü yap
            if (!isPasswordValid)
            {
                isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            }

            if (!isPasswordValid)
            {
                return new ErrorDataResult<AccessToken>("User not found or password is incorrect");
            }

            var tokens = await _tokenService.CreateToken<AccessToken>(user);
            return new SuccessDataResult<AccessToken>(new AccessToken
            {
                Token = tokens.Token,
                RefreshToken = tokens.RefreshToken,
                Claims = tokens.Claims,
                Expiration = tokens.Expiration
            });
        }
    }

}
