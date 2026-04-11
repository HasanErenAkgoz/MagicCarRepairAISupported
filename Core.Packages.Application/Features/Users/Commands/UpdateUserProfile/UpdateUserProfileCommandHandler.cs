using MagicCarRepairAISupported.Application.Features.CustomerPortal.Commands.UpdateMyProfile;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Users.Commands.UpdateUserProfile
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, UpdateMyProfileResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<UserEntity> _userManager;

        public UpdateUserProfileCommandHandler(
            IHttpContextAccessor httpContextAccessor,
            UserManager<UserEntity> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<UpdateMyProfileResponse> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                throw new UnauthorizedAccessException("User not authenticated");

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new UnauthorizedAccessException("User not found");

            if (!string.IsNullOrWhiteSpace(request.FirstName))
                user.FirstName = request.FirstName;

            if (!string.IsNullOrWhiteSpace(request.LastName))
                user.LastName = request.LastName;

            if (request.PhoneNumber != null)
                user.PhoneNumber = request.PhoneNumber;

            if (request.Address != null)
                user.Address = request.Address;

            if (request.IdentityNo != null)
                user.IdentityNo = string.IsNullOrWhiteSpace(request.IdentityNo) ? null : request.IdentityNo.Trim();

            await _userManager.UpdateAsync(user);

            return new UpdateMyProfileResponse
            {
                Id = user.Id,
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName ?? string.Empty,
                FullName = $"{user.FirstName ?? string.Empty} {user.LastName ?? string.Empty}".Trim(),
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                Address = user.Address ?? string.Empty,
                Language = user.Language ?? "tr",
                IdentityNo = user.IdentityNo
            };
        }
    }
}
