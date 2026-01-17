using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddPhoto
{
    public class AddWorkOrderPhotoCommand : IRequest<AddWorkOrderPhotoResponse>
    {
        public int WorkOrderId { get; set; }
        public string FilePath { get; set; }
        public int? UploadedFileId { get; set; }
        public string? Description { get; set; }
        public WorkOrderPhotoType PhotoType { get; set; } = WorkOrderPhotoType.Process;
        public int? TimelineId { get; set; }
        public int? UploadedByEmployeeId { get; set; }
    }
}

