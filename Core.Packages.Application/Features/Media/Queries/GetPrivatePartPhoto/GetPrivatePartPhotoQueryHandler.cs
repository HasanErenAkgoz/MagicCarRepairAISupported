using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivateVehiclePhoto;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivatePartPhoto;

public sealed class GetPrivatePartPhotoQueryHandler : IRequestHandler<GetPrivatePartPhotoQuery, MediaFile?>
{
    private readonly IPartRepository _parts;
    private readonly IEntityRepository<PartPhoto, int> _photos;
    private readonly ITenantService _tenants;
    private readonly IFileStorageService _files;
    public GetPrivatePartPhotoQueryHandler(IPartRepository parts, IEntityRepository<PartPhoto, int> photos, ITenantService tenants, IFileStorageService files)
        => (_parts, _photos, _tenants, _files) = (parts, photos, tenants, files);

    public async Task<MediaFile?> Handle(GetPrivatePartPhotoQuery request, CancellationToken cancellationToken)
    {
        var clientId = _tenants.GetRequiredClientId();
        var part = await _parts.GetByIdAsync(request.PartId, cancellationToken);
        var photo = await _photos.GetByIdAsync(request.PhotoId, cancellationToken);
        if (part is null || photo is null || part.ClientId != clientId || photo.ClientId != clientId || photo.PartId != part.Id)
            throw new DomainException("PART_PHOTO_NOT_FOUND", new { request.PartId, request.PhotoId });
        if (!StoredMediaPath.TryParse(photo.FilePath, out var container, out var name))
            throw new DomainException("PART_PHOTO_STORAGE_INVALID", new { request.PartId, request.PhotoId });
        var content = await _files.GetFileAsync(name, container);
        return content is null ? null : new MediaFile { Content = content, DownloadName = name, ContentType = StoredMediaPath.ContentType(name) };
    }
}
