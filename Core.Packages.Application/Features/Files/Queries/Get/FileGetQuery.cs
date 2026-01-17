using MagicCarRepairAISupported.Domain.Entities;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Files.Queries.Get
{
    public class FileGetQuery : IRequest<UploadedFile?>
    {
        public int FileId { get; set; }

        public FileGetQuery(int fileId)
        {
            FileId = fileId;
        }
    }

}
