using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetMyProfile;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetMyFacilityPhotos
{
    public class GetMyFacilityPhotosQueryHandler : IRequestHandler<GetMyFacilityPhotosQuery, List<FacilityPhotoDto>>
    {
        private readonly IFacilityPhotoRepository _facilityPhotoRepository;
        private readonly ITenantService _tenantService;

        public GetMyFacilityPhotosQueryHandler(
            IFacilityPhotoRepository facilityPhotoRepository,
            ITenantService tenantService)
        {
            _facilityPhotoRepository = facilityPhotoRepository;
            _tenantService = tenantService;
        }

        public async Task<List<FacilityPhotoDto>> Handle(GetMyFacilityPhotosQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            var photos = await _facilityPhotoRepository.GetByClientIdAsync(clientId, cancellationToken);

            return photos.Select(p => new FacilityPhotoDto
            {
                Id = p.Id,
                PhotoPath = p.PhotoPath,
                Title = p.Title,
                DisplayOrder = p.DisplayOrder,
                UploadDate = p.UploadDate
            }).ToList();
        }
    }
}
