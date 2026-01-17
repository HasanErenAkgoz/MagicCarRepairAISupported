using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.FacilityPhotos
{
    public class UpdateFacilityPhotoCommandHandler : IRequestHandler<UpdateFacilityPhotoCommand, UpdateFacilityPhotoResponse>
    {
        private readonly IFacilityPhotoRepository _facilityPhotoRepository;
        private readonly ITenantService _tenantService;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateFacilityPhotoCommandHandler(
            IFacilityPhotoRepository facilityPhotoRepository,
            ITenantService tenantService,
            IUnitOfWork unitOfWork)
        {
            _facilityPhotoRepository = facilityPhotoRepository;
            _tenantService = tenantService;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateFacilityPhotoResponse> Handle(UpdateFacilityPhotoCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            var facilityPhoto = await _facilityPhotoRepository.GetByIdAsync(request.Id);
            if (facilityPhoto == null || facilityPhoto.ClientId != clientId)
            {
                throw new DomainException("FACILITY_PHOTO_NOT_FOUND");
            }

            if (request.PhotoPath != null)
            {
                facilityPhoto.PhotoPath = request.PhotoPath;
            }
            facilityPhoto.Title = request.Title;
            facilityPhoto.Description = request.Description;
            facilityPhoto.Category = request.Category;
            facilityPhoto.IsPublic = request.IsPublic;
            facilityPhoto.DisplayOrder = request.DisplayOrder;

            _facilityPhotoRepository.Update(facilityPhoto);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new UpdateFacilityPhotoResponse
            {
                Id = facilityPhoto.Id,
                PhotoPath = facilityPhoto.PhotoPath,
                Title = facilityPhoto.Title,
                Description = facilityPhoto.Description,
                Category = facilityPhoto.Category,
                IsPublic = facilityPhoto.IsPublic,
                DisplayOrder = facilityPhoto.DisplayOrder
            };
        }
    }
}
