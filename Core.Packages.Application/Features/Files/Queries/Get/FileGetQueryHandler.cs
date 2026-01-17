using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Files.Queries.Get
{
    public class FileGetQueryHandler : IRequestHandler<FileGetQuery, UploadedFile?>
    {
        private readonly IUploadedFileRepository _fileRepository;

        public FileGetQueryHandler(IUploadedFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<UploadedFile?> Handle(FileGetQuery request, CancellationToken cancellationToken)
        {
            return await _fileRepository.GetAsync(x => x.Id == request.FileId,cancellationToken);
        }
    }

}
