using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Commands.Update
{
    public class UpdateVehicleResponse
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public long Kilometers { get; set; }
        public VehicleStatus Status { get; set; }
        public string StatusName { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

