using MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivateVehiclePhoto;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivateWorkOrderPhoto;

public sealed class GetPrivateWorkOrderPhotoQuery : IRequest<MediaFile?>
{
    public int WorkOrderId { get; init; }
    public int PhotoId { get; init; }
}
