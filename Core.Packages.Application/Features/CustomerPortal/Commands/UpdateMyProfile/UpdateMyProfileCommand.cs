using MediatR;

namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Commands.UpdateMyProfile
{
    public class UpdateMyProfileCommand : IRequest<UpdateMyProfileResponse>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Language { get; set; }
    }
}






