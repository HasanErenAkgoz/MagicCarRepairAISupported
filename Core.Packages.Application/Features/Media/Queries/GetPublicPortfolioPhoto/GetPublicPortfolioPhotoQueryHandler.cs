using System.Text.Json;
using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivateVehiclePhoto;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Media.Queries.GetPublicPortfolioPhoto;

public sealed class GetPublicPortfolioPhotoQueryHandler : IRequestHandler<GetPublicPortfolioPhotoQuery, MediaFile?>
{
    private readonly IServicePortfolioRepository _portfolios;
    private readonly IEntityRepository<WorkOrderPhoto, int> _photos;
    private readonly IFileStorageService _files;

    public GetPublicPortfolioPhotoQueryHandler(IServicePortfolioRepository portfolios, IEntityRepository<WorkOrderPhoto, int> photos, IFileStorageService files)
        => (_portfolios, _photos, _files) = (portfolios, photos, files);

    public async Task<MediaFile?> Handle(GetPublicPortfolioPhotoQuery request, CancellationToken cancellationToken)
    {
        var portfolio = (await _portfolios.GetPublishedPortfoliosAsync(request.ClientId, cancellationToken))
            .SingleOrDefault(p => p.Id == request.PortfolioId);
        if (portfolio is null || !ContainsPhoto(portfolio.FeaturedPhotoIds, request.PhotoId)) return null;

        var photo = await _photos.GetByIdAsync(request.PhotoId, cancellationToken);
        if (photo is null || photo.ClientId != request.ClientId || !TryParseUploadPath(photo.FilePath, out var container, out var name)) return null;
        var content = await _files.GetFileAsync(name, container);
        return content is null ? null : new MediaFile { Content = content, DownloadName = name, ContentType = ContentType(name) };
    }

    private static bool ContainsPhoto(string? value, int photoId)
    {
        try { return (JsonSerializer.Deserialize<List<int>>(value ?? "[]") ?? []).Contains(photoId); }
        catch (JsonException) { return false; }
    }

    private static bool TryParseUploadPath(string? value, out string container, out string fileName)
    {
        container = fileName = string.Empty;
        var parts = value?.TrimStart('/').Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (parts is null || parts.Length < 3 || !string.Equals(parts[0], "uploads", StringComparison.OrdinalIgnoreCase)) return false;
        container = string.Join('/', parts.Skip(1).Take(parts.Length - 2));
        fileName = parts[^1];
        return true;
    }

    private static string ContentType(string name) => Path.GetExtension(name).ToLowerInvariant() switch
    {
        ".jpg" or ".jpeg" => "image/jpeg", ".png" => "image/png", ".webp" => "image/webp", ".gif" => "image/gif",
        _ => "application/octet-stream"
    };
}
