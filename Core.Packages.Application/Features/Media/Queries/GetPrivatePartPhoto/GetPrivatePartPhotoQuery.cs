using MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivateVehiclePhoto;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivatePartPhoto;

public sealed class GetPrivatePartPhotoQuery : IRequest<MediaFile?>
{
    public int PartId { get; init; }
    public int PhotoId { get; init; }
}
