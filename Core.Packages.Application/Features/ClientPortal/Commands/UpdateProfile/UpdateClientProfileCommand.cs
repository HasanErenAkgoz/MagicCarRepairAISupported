using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.UpdateProfile
{
    public class UpdateClientProfileCommand : IRequest<UpdateClientProfileResponse>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? AboutUs { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? LogoUrl { get; set; }
        public string? WorkingHours { get; set; }
        public string? Services { get; set; }
        public string? SocialMediaLinks { get; set; }
        public bool? IsPublicProfileEnabled { get; set; }
    }
}
