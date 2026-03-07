using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Queries.GetAll
{
    public class GetAllVehiclesResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public long Kilometers { get; set; }
        public VehicleStatus Status { get; set; }
        public string StatusName { get; set; }
        public VehicleType VehicleType { get; set; }
        public string VehicleTypeName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

