using MagicCarRepairAISupported.Application.Common.Models.JWT;
using MagicCarRepairAISupported.Application.Common.Services.JWT;
using MagicCarRepairAISupported.Application.Common.Tenant;
using MagicCarRepairAISupported.Application.Features.Auth.Login.Commands;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Auth.Commands.ImpersonateClient
{
    public class ImpersonateClientCommandHandler : IRequestHandler<ImpersonateClientCommand, IDataResult<AccessToken>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<ImpersonateClientCommandHandler> _logger;

        public ImpersonateClientCommandHandler(
            UserManager<UserEntity> userManager,
            RoleManager<Role> roleManager,
            ITokenService tokenService,
            IHttpContextAccessor httpContextAccessor,
            IClientRepository clientRepository,
            ILogger<ImpersonateClientCommandHandler> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
            _clientRepository = clientRepository;
            _logger = logger;
        }

        public async Task<IDataResult<AccessToken>> Handle(ImpersonateClientCommand request, CancellationToken cancellationToken)
        {
            var callerTypeClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserType")?.Value;
            if (callerTypeClaim != "1")
            {
                return new ErrorDataResult<AccessToken>("Bu işlem yalnızca Süper Admin tarafından gerçekleştirilebilir.");
            }

            var client = await _clientRepository.GetByIdAsync(request.ClientId, cancellationToken);
            if (client == null)
            {
                return new ErrorDataResult<AccessToken>("Servis (tenant) bulunamadı.");
            }

            var shadowEmail = TenantShadowUser.GetEmail(request.ClientId);
            var targetUser = await _userManager.FindByEmailAsync(shadowEmail);

            if (targetUser == null)
            {
                targetUser = new UserEntity
                {
                    FirstName = "Sistem",
                    LastName = "Erişim",
                    IdentityNo = string.Empty,
                    Email = shadowEmail,
                    UserName = shadowEmail,
                    PhoneNumber = null,
                    UserType = UserType.Manager,
                    ClientId = request.ClientId,
                    EmailConfirmed = true,
                    HasCompletedOnboarding = true,
                };

                var password = GenerateStrongPassword();
                var createResult = await _userManager.CreateAsync(targetUser, password);
                if (!createResult.Succeeded)
                {
                    var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                    _logger.LogWarning("Shadow user create failed for ClientId={ClientId}: {Errors}", request.ClientId, errors);
                    return new ErrorDataResult<AccessToken>($"Teknik servis hesabı oluşturulamadı: {errors}");
                }

                const string roleName = "Manager";
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new Role
                    {
                        Name = roleName,
                        NormalizedName = roleName.ToUpper(),
                        ClientId = request.ClientId,
                    });
                }

                await _userManager.AddToRoleAsync(targetUser, roleName);
                _logger.LogInformation("Shadow shop user created for ClientId={ClientId}, UserId={UserId}", request.ClientId, targetUser.Id);
            }
            else
            {
                if (targetUser.ClientId != request.ClientId)
                {
                    return new ErrorDataResult<AccessToken>("Teknik hesap bu servise ait değil.");
                }

                if (targetUser.UserType == UserType.SystemAdmin)
                {
                    return new ErrorDataResult<AccessToken>("Geçersiz teknik hesap durumu.");
                }
            }

            var accessToken = await _tokenService.CreateToken<AccessToken>(targetUser, false);
            var userRoles = await _userManager.GetRolesAsync(targetUser);

            accessToken.User = new LoginUserInfo
            {
                Id = targetUser.Id,
                Email = targetUser.Email ?? string.Empty,
                FirstName = targetUser.FirstName,
                LastName = targetUser.LastName,
                UserType = targetUser.UserType,
                Roles = userRoles.ToList(),
                ClientId = targetUser.ClientId > 0 ? targetUser.ClientId : null,
                RequiresTwoFactor = false,
                HasCompletedOnboarding = targetUser.HasCompletedOnboarding,
            };

            return new SuccessDataResult<AccessToken>(accessToken,
                $"'{client.Name}' servis paneline teknik erişimle girildi.");
        }

        private static string GenerateStrongPassword()
        {
            var bytes = new byte[24];
            RandomNumberGenerator.Fill(bytes);
            var core = Convert.ToBase64String(bytes);
            return $"Aa!{core}9zX";
        }
    }
}
