using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivateVehiclePhoto;

public sealed class GetPrivateVehiclePhotoQueryHandler : IRequestHandler<GetPrivateVehiclePhotoQuery, MediaFile?>
{
    private readonly IEntityRepository<VehiclePhoto, int> _photos;
    private readonly ITenantService _tenantService;
    private readonly IFileStorageService _files;

    public GetPrivateVehiclePhotoQueryHandler(IEntityRepository<VehiclePhoto, int> photos, ITenantService tenantService, IFileStorageService files)
        => (_photos, _tenantService, _files) = (photos, tenantService, files);

    public async Task<MediaFile?> Handle(GetPrivateVehiclePhotoQuery request, CancellationToken cancellationToken)
    {
        var clientId = _tenantService.GetRequiredClientId();
        var photo = await _photos.Query().AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == request.PhotoId && p.VehicleId == request.VehicleId && p.ClientId == clientId, cancellationToken);
        if (photo is null)
            throw new DomainException("VEHICLE_PHOTO_NOT_FOUND", new { request.VehicleId, request.PhotoId });

        var storedPath = photo.FilePath;
        if (!TryParseUploadPath(storedPath, out var container, out var fileName))
            throw new DomainException("VEHICLE_PHOTO_STORAGE_INVALID", new { request.VehicleId, request.PhotoId });

        var content = await _files.GetFileAsync(fileName, container);
        if (content is null)
            return null;

        return new MediaFile { Content = content, DownloadName = fileName, ContentType = GetContentType(fileName) };
    }

    private static bool TryParseUploadPath(string? value, out string container, out string fileName)
    {
        container = fileName = string.Empty;
        if (string.IsNullOrWhiteSpace(value)) return false;
        var segments = value.TrimStart('/').Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (segments.Length < 3 || !string.Equals(segments[0], "uploads", StringComparison.OrdinalIgnoreCase)) return false;
        fileName = segments[^1];
        container = string.Join('/', segments.Skip(1).Take(segments.Length - 2));
        return true;
    }

    private static string GetContentType(string name) => Path.GetExtension(name).ToLowerInvariant() switch
    {
        ".jpg" or ".jpeg" => "image/jpeg", ".png" => "image/png", ".webp" => "image/webp", ".gif" => "image/gif",
        _ => "application/octet-stream"
    };
}
