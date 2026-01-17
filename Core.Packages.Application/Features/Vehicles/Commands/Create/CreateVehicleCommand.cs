using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Commands.Create
{
    public class CreateVehicleCommand : IRequest<CreateVehicleResponse>
    {
        public int CustomerId { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public long Kilometers { get; set; } = 0;
        public VehicleStatus Status { get; set; } = VehicleStatus.Registered;
    }
}

