using MagicCarRepairAISupported.Application.Features.CustomerPortal.Commands.UpdateMyProfile;
using MagicCarRepairAISupported.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Users.Queries.GetMyProfile
{
    public class GetMyProfileQueryHandler : IRequestHandler<GetMyProfileQuery, UpdateMyProfileResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<UserEntity> _userManager;

        public GetMyProfileQueryHandler(
            IHttpContextAccessor httpContextAccessor,
            UserManager<UserEntity> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<UpdateMyProfileResponse> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
        {
            // Get current user ID from HttpContext
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("User not authenticated");
            }

            // Get user information
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found");
            }

            // Return User information directly (for Admin, Manager, Employee)
            return new UpdateMyProfileResponse
            {
                Id = user.Id,
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName ?? string.Empty,
                FullName = $"{user.FirstName ?? string.Empty} {user.LastName ?? string.Empty}".Trim(),
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                Address = user.Address ?? string.Empty,
                Language = user.Language ?? "tr"
            };
        }
    }
}
