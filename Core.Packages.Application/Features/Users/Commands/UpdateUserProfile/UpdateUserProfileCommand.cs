using MagicCarRepairAISupported.Application.Features.CustomerPortal.Commands.UpdateMyProfile;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Users.Commands.UpdateUserProfile
{
    public class UpdateUserProfileCommand : IRequest<UpdateMyProfileResponse>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? IdentityNo { get; set; }
    }
}
