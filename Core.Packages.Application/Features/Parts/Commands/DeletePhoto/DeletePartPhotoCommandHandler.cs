using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.DeletePhoto
{
    public class DeletePartPhotoCommandHandler : IRequestHandler<DeletePartPhotoCommand, IResult>
    {
        private readonly IPartRepository _partRepository;
        private readonly IEntityRepository<PartPhoto, int> _partPhotoRepository;
        private readonly ITenantService _tenantService;
        private readonly IFileStorageService _fileStorageService;

        public DeletePartPhotoCommandHandler(
            IPartRepository partRepository,
            IEntityRepository<PartPhoto, int> partPhotoRepository,
            ITenantService tenantService,
            IFileStorageService fileStorageService)
        {
            _partRepository = partRepository;
            _partPhotoRepository = partPhotoRepository;
            _tenantService = tenantService;
            _fileStorageService = fileStorageService;
        }

        public async Task<IResult> Handle(DeletePartPhotoCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            var part = await _partRepository.GetByIdAsync(request.PartId);
            if (part == null)
                throw new DomainException("PART_NOT_FOUND", new { Id = request.PartId });

            if (part.ClientId != clientId)
                throw new DomainException("PART_NOT_BELONG_TO_CLIENT", new { PartId = request.PartId });

            var photo = await _partPhotoRepository.GetByIdAsync(request.PhotoId);
            if (photo == null || photo.PartId != request.PartId)
                return new ErrorResult("Fotoğraf bulunamadı.");

            if (!string.IsNullOrEmpty(photo.FilePath))
            {
                try
                {
                    var pathParts = photo.FilePath.TrimStart('/').Split('/');
                    if (pathParts.Length >= 3)
                    {
                        var containerName = $"{pathParts[1]}/{pathParts[2]}";
                        var fileName = pathParts[3];
                        await _fileStorageService.DeleteFileAsync(fileName, containerName, cancellationToken);
                    }
                }
                catch { /* dosya silme hatası olsa bile devam et */ }
            }

            _partPhotoRepository.Delete(photo);
            await _partPhotoRepository.SaveChangesAsync();

            return new SuccessResult("Fotoğraf başarıyla silindi.");
        }
    }
}
