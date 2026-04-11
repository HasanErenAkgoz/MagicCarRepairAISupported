using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Commands.Create
{
    public class CreateVehicleCommand : IRequest<CreateVehicleResponse>
    {
        public int CustomerId { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Color { get; set; } = string.Empty;
        public long Kilometers { get; set; } = 0;
        public string? FuelType { get; set; }
        public string? Vin { get; set; }
        public VehicleStatus Status { get; set; } = VehicleStatus.Registered;
        public VehicleType VehicleType { get; set; } = VehicleType.Unspecified;
    }
}

