using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Commands.AddPhoto
{
    public class AddVehiclePhotoCommand : IRequest<AddVehiclePhotoResponse>
    {
        public int VehicleId { get; set; }
        public string FilePath { get; set; }
        public int? UploadedFileId { get; set; }
        public string? Description { get; set; }
        public string? PhotoType { get; set; }
        public int DisplayOrder { get; set; } = 0;
    }
}
