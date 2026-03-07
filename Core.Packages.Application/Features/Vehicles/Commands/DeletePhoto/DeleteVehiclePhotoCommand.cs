using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Commands.DeletePhoto
{
    public class DeleteVehiclePhotoCommand : IRequest<IResult>
    {
        public int VehicleId { get; set; }
        public int PhotoId { get; set; }
    }
}
