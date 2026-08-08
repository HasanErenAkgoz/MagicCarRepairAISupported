using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivateVehiclePhoto;

/// <summary>Resolves a vehicle photo from its owning resource, never from a caller-supplied disk path.</summary>
public sealed class GetPrivateVehiclePhotoQuery : IRequest<MediaFile?>
{
    public int VehicleId { get; init; }
    public int PhotoId { get; init; }
}

public sealed class MediaFile
{
    public required Stream Content { get; init; }
    public required string ContentType { get; init; }
    public required string DownloadName { get; init; }
}
