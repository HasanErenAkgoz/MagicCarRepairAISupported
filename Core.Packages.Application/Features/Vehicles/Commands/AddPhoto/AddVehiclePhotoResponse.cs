namespace MagicCarRepairAISupported.Application.Features.Vehicles.Commands.AddPhoto
{
    public class AddVehiclePhotoResponse
    {
        public int PhotoId { get; set; }
        public int VehicleId { get; set; }
        public string FilePath { get; set; }
        public string? PhotoType { get; set; }
        public int DisplayOrder { get; set; }
    }
}
