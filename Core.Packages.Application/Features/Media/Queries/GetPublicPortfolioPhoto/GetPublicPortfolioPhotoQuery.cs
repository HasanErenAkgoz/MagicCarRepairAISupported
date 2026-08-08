using MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivateVehiclePhoto;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Media.Queries.GetPublicPortfolioPhoto;

/// <summary>Public media is exposed only when a published portfolio explicitly references the photo.</summary>
public sealed class GetPublicPortfolioPhotoQuery : IRequest<MediaFile?>
{
    public int ClientId { get; init; }
    public int PortfolioId { get; init; }
    public int PhotoId { get; init; }
}
