using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivateVehiclePhoto;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivateWorkOrderPhoto;

public sealed class GetPrivateWorkOrderPhotoQueryHandler : IRequestHandler<GetPrivateWorkOrderPhotoQuery, MediaFile?>
{
    private readonly IEntityRepository<WorkOrderPhoto, int> _photos;
    private readonly ITenantService _tenants;
    private readonly IFileStorageService _files;
    public GetPrivateWorkOrderPhotoQueryHandler(IEntityRepository<WorkOrderPhoto, int> photos, ITenantService tenants, IFileStorageService files)
        => (_photos, _tenants, _files) = (photos, tenants, files);

    public async Task<MediaFile?> Handle(GetPrivateWorkOrderPhotoQuery request, CancellationToken cancellationToken)
    {
        var photo = await _photos.GetByIdAsync(request.PhotoId, cancellationToken);
        if (photo is null || photo.WorkOrderId != request.WorkOrderId || photo.ClientId != _tenants.GetRequiredClientId())
            throw new DomainException("WORK_ORDER_PHOTO_NOT_FOUND", new { request.WorkOrderId, request.PhotoId });
        if (!StoredMediaPath.TryParse(photo.FilePath, out var container, out var name))
            throw new DomainException("WORK_ORDER_PHOTO_STORAGE_INVALID", new { request.WorkOrderId, request.PhotoId });
        var content = await _files.GetFileAsync(name, container);
        return content is null ? null : new MediaFile { Content = content, DownloadName = name, ContentType = StoredMediaPath.ContentType(name) };
    }
}
