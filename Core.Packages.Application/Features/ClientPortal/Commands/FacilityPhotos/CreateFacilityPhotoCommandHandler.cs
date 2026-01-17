using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.FacilityPhotos
{
    public class CreateFacilityPhotoCommandHandler : IRequestHandler<CreateFacilityPhotoCommand, CreateFacilityPhotoResponse>
    {
        private readonly IFacilityPhotoRepository _facilityPhotoRepository;
        private readonly ITenantService _tenantService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateFacilityPhotoCommandHandler(
            IFacilityPhotoRepository facilityPhotoRepository,
            ITenantService tenantService,
            IUnitOfWork unitOfWork)
        {
            _facilityPhotoRepository = facilityPhotoRepository;
            _tenantService = tenantService;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateFacilityPhotoResponse> Handle(CreateFacilityPhotoCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            var facilityPhoto = new FacilityPhoto
            {
                PhotoPath = request.PhotoPath,
                Title = request.Title,
                Description = request.Description,
                Category = request.Category,
                IsPublic = request.IsPublic,
                DisplayOrder = request.DisplayOrder,
                UploadDate = DateTime.UtcNow,
                ClientId = clientId
            };

            await _facilityPhotoRepository.AddAsync(facilityPhoto, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreateFacilityPhotoResponse
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
