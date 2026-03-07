using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Commands.AddWorkOrderPhoto
{
    public class AddWorkOrderPhotoCommand : IRequest<AddWorkOrderPhotoResponse>
    {
        public int WorkOrderId { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public int? UploadedFileId { get; set; }
        public string? Description { get; set; }
        public WorkOrderPhotoType PhotoType { get; set; } = WorkOrderPhotoType.Damage;
    }

    public class AddWorkOrderPhotoResponse
    {
        public int PhotoId { get; set; }
        public int WorkOrderId { get; set; }
        public string FilePath { get; set; } = string.Empty;
    }
}
