using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetMyProfile
{
    public class GetMyProfileQueryHandler : IRequestHandler<GetMyProfileQuery, GetMyProfileResponse>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IFacilityPhotoRepository _facilityPhotoRepository;
        private readonly ITenantService _tenantService;

        public GetMyProfileQueryHandler(
            IClientRepository clientRepository,
            IFacilityPhotoRepository facilityPhotoRepository,
            ITenantService tenantService)
        {
            _clientRepository = clientRepository;
            _facilityPhotoRepository = facilityPhotoRepository;
            _tenantService = tenantService;
        }

        public async Task<GetMyProfileResponse> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            var client = await _clientRepository.GetAsync(c => c.Id == clientId, cancellationToken)
                ?? throw new DomainException("CLIENT_NOT_FOUND");

            var photos = await _facilityPhotoRepository.GetByClientIdAsync(clientId, cancellationToken);

            return new GetMyProfileResponse
            {
                Id = client.Id,
                Name = client.Name,
                Code = client.Code,
                Description = client.Description,
                AboutUs = client.AboutUs,
                ContactEmail = client.ContactEmail,
                ContactPhone = client.ContactPhone,
                Address = client.Address,
                WebsiteUrl = client.WebsiteUrl,
                LogoUrl = client.LogoUrl,
                WorkingHours = client.WorkingHours,
                Services = client.Services,
                SocialMediaLinks = client.SocialMediaLinks,
                IsPublicProfileEnabled = client.IsPublicProfileEnabled,
                FacilityPhotos = photos.Select(p => new FacilityPhotoDto
                {
                    Id = p.Id,
                    PhotoPath = p.PhotoPath,
                    Title = p.Title,
                    DisplayOrder = p.DisplayOrder,
                    UploadDate = p.UploadDate
                }).ToList()
            };
        }
    }
}
