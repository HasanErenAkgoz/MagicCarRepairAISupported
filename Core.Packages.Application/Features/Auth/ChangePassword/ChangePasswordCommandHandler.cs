using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Auth.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, IResult>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChangePasswordCommandHandler(
            UserManager<UserEntity> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            // Get current user ID from HttpContext
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return new ErrorResult("Kullanıcı bilgisi bulunamadı.");
            }

            // Validate passwords
            if (string.IsNullOrWhiteSpace(request.CurrentPassword))
            {
                return new ErrorResult("Mevcut şifre gereklidir.");
            }

            if (string.IsNullOrWhiteSpace(request.NewPassword))
            {
                return new ErrorResult("Yeni şifre gereklidir.");
            }

            if (request.NewPassword.Length < 6)
            {
                return new ErrorResult("Yeni şifre en az 6 karakter olmalıdır.");
            }

            if (request.NewPassword != request.ConfirmPassword)
            {
                return new ErrorResult("Yeni şifre ve şifre onayı eşleşmiyor.");
            }

            if (request.CurrentPassword == request.NewPassword)
            {
                return new ErrorResult("Yeni şifre mevcut şifre ile aynı olamaz.");
            }

            // Get user
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return new ErrorResult("Kullanıcı bulunamadı.");
            }

            // Verify current password
            var isCurrentPasswordValid = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
            if (!isCurrentPasswordValid)
            {
                return new ErrorResult("Mevcut şifre yanlış.");
            }

            // Change password
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                var errors = string.Join(", ", changePasswordResult.Errors.Select(e => e.Description));
                return new ErrorResult($"Şifre değiştirilemedi: {errors}");
            }

            return new SuccessResult("Şifre başarıyla değiştirildi.");
        }
    }
}
