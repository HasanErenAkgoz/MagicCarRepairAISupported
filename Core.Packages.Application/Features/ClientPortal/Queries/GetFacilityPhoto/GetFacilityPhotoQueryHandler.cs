using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetFacilityPhoto
{
    public class GetFacilityPhotoQueryHandler : IRequestHandler<GetFacilityPhotoQuery, GetFacilityPhotoResponse>
    {
        private readonly IFacilityPhotoRepository _facilityPhotoRepository;
        private readonly ITenantService _tenantService;

        public GetFacilityPhotoQueryHandler(
            IFacilityPhotoRepository facilityPhotoRepository,
            ITenantService tenantService)
        {
            _facilityPhotoRepository = facilityPhotoRepository;
            _tenantService = tenantService;
        }

        public async Task<GetFacilityPhotoResponse> Handle(GetFacilityPhotoQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            var facilityPhoto = await _facilityPhotoRepository.GetByIdAsync(request.Id);
            if (facilityPhoto == null || facilityPhoto.ClientId != clientId)
            {
                throw new DomainException("FACILITY_PHOTO_NOT_FOUND");
            }

            return new GetFacilityPhotoResponse
            {
                Id = facilityPhoto.Id,
                PhotoPath = facilityPhoto.PhotoPath,
                Title = facilityPhoto.Title,
                Description = facilityPhoto.Description,
                Category = facilityPhoto.Category,
                IsPublic = facilityPhoto.IsPublic,
                DisplayOrder = facilityPhoto.DisplayOrder,
                UploadDate = facilityPhoto.UploadDate
            };
        }
    }
}
