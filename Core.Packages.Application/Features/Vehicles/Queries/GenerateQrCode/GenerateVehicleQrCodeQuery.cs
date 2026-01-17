using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Queries.GenerateQrCode
{
    public class GenerateVehicleQrCodeQuery : IRequest<byte[]>
    {
        public int VehicleId { get; set; }
    }
}
