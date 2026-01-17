using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Commands.AnalyzePhoto
{
    public class AnalyzePhotoCommand : IRequest<IDataResult<PhotoAnalysisResultDto>>
    {
        public byte[] PhotoData { get; set; } = Array.Empty<byte>();
        public string? FileName { get; set; }
        public List<byte[]>? MultiplePhotos { get; set; }
    }
}

