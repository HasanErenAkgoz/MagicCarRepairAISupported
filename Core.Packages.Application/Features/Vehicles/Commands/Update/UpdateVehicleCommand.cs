using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Commands.Update
{
    public class UpdateVehicleCommand : IRequest<UpdateVehicleResponse>
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }
        public string? Color { get; set; }
        public string? LicensePlate { get; set; }
        public long? Kilometers { get; set; }
        public VehicleStatus? Status { get; set; }
        public VehicleType? VehicleType { get; set; }
    }
}

