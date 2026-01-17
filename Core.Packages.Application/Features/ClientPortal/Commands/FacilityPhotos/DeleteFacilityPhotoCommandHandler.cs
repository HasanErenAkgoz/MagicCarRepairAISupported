using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.FacilityPhotos
{
    public class DeleteFacilityPhotoCommandHandler : IRequestHandler<DeleteFacilityPhotoCommand, DeleteFacilityPhotoResponse>
    {
        private readonly IFacilityPhotoRepository _facilityPhotoRepository;
        private readonly ITenantService _tenantService;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFacilityPhotoCommandHandler(
            IFacilityPhotoRepository facilityPhotoRepository,
            ITenantService tenantService,
            IUnitOfWork unitOfWork)
        {
            _facilityPhotoRepository = facilityPhotoRepository;
            _tenantService = tenantService;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteFacilityPhotoResponse> Handle(DeleteFacilityPhotoCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            var facilityPhoto = await _facilityPhotoRepository.GetByIdAsync(request.Id);
            if (facilityPhoto == null || facilityPhoto.ClientId != clientId)
            {
                throw new DomainException("FACILITY_PHOTO_NOT_FOUND");
            }

            // Soft delete
            _facilityPhotoRepository.Delete(facilityPhoto);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new DeleteFacilityPhotoResponse
            {
                Id = request.Id,
                Success = true,
                Message = "Facility photo deleted successfully"
            };
        }
    }
}
