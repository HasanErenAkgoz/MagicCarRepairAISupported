using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.AddPhoto
{
    public class AddPartPhotoCommand : IRequest<AddPartPhotoResponse>
    {
        public int PartId { get; set; }
        public string FilePath { get; set; }
        public int? UploadedFileId { get; set; }
        public string? Description { get; set; }
        public int DisplayOrder { get; set; } = 0;
    }
}
