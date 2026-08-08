using MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivateVehiclePhoto;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivateQuoteRequestPhoto;

/// <summary>Photo index is resolved against the request's server-stored PhotoPaths JSON; it is not a file path.</summary>
public sealed class GetPrivateQuoteRequestPhotoQuery : IRequest<MediaFile?>
{
    public int QuoteRequestId { get; init; }
    public int PhotoId { get; init; }
}
