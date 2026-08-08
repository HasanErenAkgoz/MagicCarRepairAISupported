using MagicCarRepairAISupported.Application.Common.Models.JWT;
using MagicCarRepairAISupported.Application.Common.Services.JWT;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MagicCarRepairAISupported.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Auth.Login.Commands
{
    public class LoginCommand : IRequest<IDataResult<AccessToken>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
        public string? DeviceId { get; set; }
        public string? DeviceName { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, IDataResult<AccessToken>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserDeviceRepository _userDeviceRepository;
        private readonly IUserSessionRepository _userSessionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public LoginCommandHandler(
            UserManager<UserEntity> userManager,
            ITokenService tokenService,
            IConfiguration configuration,
            IClientRepository clientRepository,
            IUserRepository userRepository,
            IUserDeviceRepository userDeviceRepository,
            IUserSessionRepository userSessionRepository,
            IHttpContextAccessor httpContextAccessor,
            ISubscriptionRepository subscriptionRepository)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _configuration = configuration;
            _clientRepository = clientRepository;
            _userRepository = userRepository;
            _userDeviceRepository = userDeviceRepository;
            _userSessionRepository = userSessionRepository;
            _httpContextAccessor = httpContextAccessor;
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<IDataResult<AccessToken>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByEmailForAuthAsync(
                _userManager.NormalizeEmail(request.Email),
                cancellationToken);

            if (user == null)
            {
                return new ErrorDataResult<AccessToken>("User not found or password is incorrect");
            }

            // Tamirhane (Manager/Employee) kullanıcıları için Client aktiflik kontrolü
            // Customer ve SystemAdmin bu kontrolden muaf
            if ((user.UserType == UserType.Manager || user.UserType == UserType.Employee) && user.ClientId > 0)
            {
                var client = await _clientRepository.GetByIdForAuthAsync(user.ClientId, cancellationToken);
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

            // Abonelik planını al (Manager/Employee için clientId bazlı, diğerleri null)
            SubscriptionPlan? subscriptionPlan = null;
            if (user.ClientId > 0 &&
                (user.UserType == UserType.Manager || user.UserType == UserType.Employee))
            {
                var subscription = await _subscriptionRepository.GetActiveSubscriptionAsync(user.ClientId);
                subscriptionPlan = subscription?.Plan;
            }

            // Token oluştur (Remember Me'ye göre expiration süreleri ayarlanır)
            var tokens = await _tokenService.CreateToken<AccessToken>(user, request.RememberMe);
            var userRoles = await _userManager.GetRolesAsync(user);

            var refreshTokenExpiration = request.RememberMe
                ? DateTime.UtcNow.AddDays(30)
                : DateTime.UtcNow.AddDays(7);

            await _userRepository.UpdateRefreshTokenAsync(
                user.Id,
                tokens.RefreshToken,
                refreshTokenExpiration,
                cancellationToken);

            // Token'dan JTI (JWT ID) claim'ini al - Session tracking için
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(tokens.Token);
            var jtiClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "jti");
            var tokenId = jtiClaim?.Value ?? Guid.NewGuid().ToString();

            // IP Address ve User Agent bilgilerini al
            var ipAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            var userAgent = _httpContextAccessor.HttpContext?.Request?.Headers["User-Agent"].ToString();

            if (!string.IsNullOrEmpty(request.DeviceId))
            {
                await _userDeviceRepository.UpsertLoginDeviceAsync(
                    new UserDevice
                    {
                        UserId = user.Id,
                        DeviceId = request.DeviceId,
                        DeviceName = request.DeviceName,
                        IsTrusted = request.RememberMe,
                        LastLoginAt = DateTime.UtcNow,
                        ClientId = user.ClientId,
                        Status = Status.Active,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = user.Id
                    },
                    cancellationToken);
            }

            // Session Management - Her login'de yeni session kaydet

            var userSession = new UserSession
            {
                UserId = user.Id,
                TokenId = tokenId,
                DeviceId = request.DeviceId,
                DeviceName = request.DeviceName,
                IpAddress = ipAddress,
                UserAgent = userAgent,
                IsRemembered = request.RememberMe,
                ExpiresAt = refreshTokenExpiration,
                LastActivityAt = DateTime.UtcNow,
                ClientId = user.ClientId,
                Status = Status.Active,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = user.Id
            };
            await _userSessionRepository.AddAsync(userSession, cancellationToken);
            
            // Claims listesini oluştur
            var claimsList = jsonToken.Claims.Select(c => $"{c.Type}:{c.Value}").ToList();
            
            return new SuccessDataResult<AccessToken>(new AccessToken
            {
                Token = tokens.Token,
                RefreshToken = tokens.RefreshToken,
                Claims = claimsList,
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
                    HasCompletedOnboarding = user.HasCompletedOnboarding,
                    SubscriptionPlan = subscriptionPlan
                }
            });
        }
    }

}
