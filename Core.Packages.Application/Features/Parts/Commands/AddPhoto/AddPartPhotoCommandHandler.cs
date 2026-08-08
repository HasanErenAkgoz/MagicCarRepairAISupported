using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.AddPhoto
{
    public class AddPartPhotoCommandHandler : IRequestHandler<AddPartPhotoCommand, AddPartPhotoResponse>
    {
        private readonly IPartRepository _partRepository;
        private readonly IEntityRepository<PartPhoto, int> _partPhotoRepository;
        private readonly ITenantService _tenantService;

        public AddPartPhotoCommandHandler(
            IPartRepository partRepository,
            IEntityRepository<PartPhoto, int> partPhotoRepository,
            ITenantService tenantService)
        {
            _partRepository = partRepository;
            _partPhotoRepository = partPhotoRepository;
            _tenantService = tenantService;
        }

        public async Task<AddPartPhotoResponse> Handle(AddPartPhotoCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            var part = await _partRepository.GetByIdAsync(request.PartId);
            if (part == null)
                throw new DomainException("PART_NOT_FOUND", new { Id = request.PartId });

            if (part.ClientId != clientId)
                throw new DomainException("PART_NOT_BELONG_TO_CLIENT", new { PartId = request.PartId });

            var photo = new PartPhoto
            {
                PartId = request.PartId,
                FilePath = request.FilePath,
                UploadedFileId = request.UploadedFileId,
                Description = request.Description,
                DisplayOrder = request.DisplayOrder,
                UploadDate = DateTime.UtcNow,
                ClientId = clientId,
            };

            await _partPhotoRepository.AddAsync(photo, cancellationToken);

            return new AddPartPhotoResponse
            {
                PhotoId = photo.Id,
                PartId = part.Id,
                FilePath = photo.FilePath,
                MediaUrl = $"/api/media/parts/{part.Id}/photos/{photo.Id}",
                DisplayOrder = photo.DisplayOrder,
            };
        }
    }
}
