using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Commands.AnalyzePhoto
{
    public class AnalyzePhotoCommandHandler : IRequestHandler<AnalyzePhotoCommand, IDataResult<PhotoAnalysisResultDto>>
    {
        private readonly IAIPhotoAnalysisService _aiPhotoAnalysisService;

        public AnalyzePhotoCommandHandler(IAIPhotoAnalysisService aiPhotoAnalysisService)
        {
            _aiPhotoAnalysisService = aiPhotoAnalysisService;
        }

        public async Task<IDataResult<PhotoAnalysisResultDto>> Handle(AnalyzePhotoCommand request, CancellationToken cancellationToken)
        {
            PhotoAnalysisResultDto result;

            if (request.MultiplePhotos != null && request.MultiplePhotos.Count > 0)
            {
                result = await _aiPhotoAnalysisService.AnalyzeMultiplePhotosAsync(request.MultiplePhotos, cancellationToken);
            }
            else
            {
                if (request.PhotoData == null || request.PhotoData.Length == 0)
                {
                    return new ErrorDataResult<PhotoAnalysisResultDto>("Fotoğraf verisi boş olamaz");
                }

                result = await _aiPhotoAnalysisService.AnalyzePhotoAsync(request.PhotoData, request.FileName, cancellationToken);
            }

            return new SuccessDataResult<PhotoAnalysisResultDto>(result, "Fotoğraf analizi başarıyla tamamlandı");
        }
    }
}

