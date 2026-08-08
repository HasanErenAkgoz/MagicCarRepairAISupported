using System.Text.Json;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivateVehiclePhoto;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivateQuoteRequestPhoto;

public sealed class GetPrivateQuoteRequestPhotoQueryHandler : IRequestHandler<GetPrivateQuoteRequestPhotoQuery, MediaFile?>
{
    private readonly IQuoteRequestRepository _requests;
    private readonly ITenantService _tenants;
    private readonly IFileStorageService _files;
    public GetPrivateQuoteRequestPhotoQueryHandler(IQuoteRequestRepository requests, ITenantService tenants, IFileStorageService files)
        => (_requests, _tenants, _files) = (requests, tenants, files);

    public async Task<MediaFile?> Handle(GetPrivateQuoteRequestPhotoQuery request, CancellationToken cancellationToken)
    {
        var quoteRequest = await _requests.GetByIdAsync(request.QuoteRequestId, cancellationToken);
        if (quoteRequest is null || quoteRequest.ClientId != _tenants.GetRequiredClientId())
            throw new DomainException("QUOTE_REQUEST_PHOTO_NOT_FOUND", new { request.QuoteRequestId, request.PhotoId });
        var paths = TryReadPaths(quoteRequest.PhotoPaths);
        if (request.PhotoId < 1 || request.PhotoId > paths.Count || !StoredMediaPath.TryParse(paths[request.PhotoId - 1], out var container, out var name))
            throw new DomainException("QUOTE_REQUEST_PHOTO_NOT_FOUND", new { request.QuoteRequestId, request.PhotoId });
        var content = await _files.GetFileAsync(name, container);
        return content is null ? null : new MediaFile { Content = content, DownloadName = name, ContentType = StoredMediaPath.ContentType(name) };
    }

    private static List<string> TryReadPaths(string? value)
    {
        try { return JsonSerializer.Deserialize<List<string>>(value ?? "[]") ?? []; }
        catch (JsonException) { return []; }
    }
}
