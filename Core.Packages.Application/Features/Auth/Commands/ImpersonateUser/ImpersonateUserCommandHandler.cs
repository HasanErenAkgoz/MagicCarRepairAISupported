using MagicCarRepairAISupported.Application.Common.Models.JWT;
using MagicCarRepairAISupported.Application.Common.Services.JWT;
using MagicCarRepairAISupported.Application.Features.Auth.Login.Commands;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Auth.Commands.ImpersonateUser
{
    public class ImpersonateUserCommandHandler : IRequestHandler<ImpersonateUserCommand, IDataResult<AccessToken>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ImpersonateUserCommandHandler(
            UserManager<UserEntity> userManager,
            ITokenService tokenService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IDataResult<AccessToken>> Handle(ImpersonateUserCommand request, CancellationToken cancellationToken)
        {
            // Çağıranın SystemAdmin olduğunu doğrula
            var callerTypeClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserType")?.Value;
            if (callerTypeClaim != "1")
            {
                return new ErrorDataResult<AccessToken>("Bu işlem yalnızca Süper Admin tarafından gerçekleştirilebilir.");
            }

            // Hedef kullanıcıyı bul
            var targetUser = await _userManager.FindByIdAsync(request.TargetUserId.ToString());
            if (targetUser == null)
            {
                return new ErrorDataResult<AccessToken>("Kullanıcı bulunamadı.");
            }

            // SystemAdmin taklit edilemez
            if (targetUser.UserType == UserType.SystemAdmin)
            {
                return new ErrorDataResult<AccessToken>("Süper Admin hesabına giriş yapılamaz.");
            }

            // Hedef kullanıcı için token üret (session oluşturulmaz — geçici erişim)
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
                $"'{targetUser.FirstName} {targetUser.LastName}' kullanıcısı olarak giriş yapıldı.");
        }
    }
}
