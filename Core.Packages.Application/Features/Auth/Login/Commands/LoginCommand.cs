using MagicCarRepairAISupported.Application.Common.Services.JWT;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
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
        private readonly IClientRepository _clientRepository;

        public LoginCommandHandler(
            UserManager<UserEntity> userManager,
            ITokenService tokenService,
            IConfiguration configuration,
            IClientRepository clientRepository)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _configuration = configuration;
            _clientRepository = clientRepository;
        }

        public async Task<IDataResult<AccessToken>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            
            if (user == null)
            {
                return new ErrorDataResult<AccessToken>("User not found or password is incorrect");
            }

            // Tamirhane (Manager/Employee) kullanıcıları için Client aktiflik kontrolü
            // Customer ve SystemAdmin bu kontrolden muaf
            if ((user.UserType == UserType.Manager || user.UserType == UserType.Employee) && user.ClientId > 0)
            {
                var client = await _clientRepository.GetByIdAsync(user.ClientId, cancellationToken);
                if (client != null && !client.IsActive)
                {
                    return new ErrorDataResult<AccessToken>("ACCOUNT_PENDING_APPROVAL");
                }
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

            // 2FA kontrolü - eğer aktifse token verme, 2FA doğrulaması gerektiğini belirt
            if (user.TwoFactorEnabled && !string.IsNullOrEmpty(user.TwoFactorSecret))
            {
                // 2FA gerekiyor - token vermeden önce 2FA doğrulaması yapılmalı
                return new ErrorDataResult<AccessToken>("TwoFactorRequired")
                {
                    Data = new AccessToken
                    {
                        User = new LoginUserInfo
                        {
                            Id = user.Id,
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            UserType = user.UserType,
                            Roles = (await _userManager.GetRolesAsync(user)).ToList(),
                            ClientId = user.ClientId,
                            RequiresTwoFactor = true
                        }
                    }
                };
            }

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
