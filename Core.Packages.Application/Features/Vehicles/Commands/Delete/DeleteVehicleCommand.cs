using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Commands.Delete
{
    public class DeleteVehicleCommand : IRequest<DeleteVehicleResponse>
    {
        public int Id { get; set; }
    }
}

