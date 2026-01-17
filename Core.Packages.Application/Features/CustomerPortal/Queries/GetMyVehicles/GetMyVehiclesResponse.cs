namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Queries.GetMyVehicles
{
    public class GetMyVehiclesResponse
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public long Kilometers { get; set; }
        public string Status { get; set; }
        public string StatusName { get; set; }
    }
}






