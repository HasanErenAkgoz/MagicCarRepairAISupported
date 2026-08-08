using MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivateVehiclePhoto;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Media.Queries.GetCustomerWorkOrderPhoto;

/// <summary>Customer-scoped media request. The customer id identifies the owned work order, never a storage path.</summary>
public sealed class GetCustomerWorkOrderPhotoQuery : IRequest<MediaFile?>
{
    public int CustomerId { get; init; }
    public int WorkOrderId { get; init; }
    public int PhotoId { get; init; }
}
