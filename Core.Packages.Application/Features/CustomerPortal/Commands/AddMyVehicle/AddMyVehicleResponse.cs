namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Commands.AddMyVehicle
{
    public class AddMyVehicleResponse
    {
        public int Id { get; set; }
        public string Plate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string? Color { get; set; }
        public string? Vin { get; set; }
        public string? FuelType { get; set; }
    }
}
