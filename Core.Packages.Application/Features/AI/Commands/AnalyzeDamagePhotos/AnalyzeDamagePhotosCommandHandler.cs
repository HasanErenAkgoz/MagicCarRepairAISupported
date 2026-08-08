using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Commands.AnalyzeDamagePhotos
{
    public class AnalyzeDamagePhotosCommandHandler : IRequestHandler<AnalyzeDamagePhotosCommand, IDataResult<AnalyzeDamagePhotosResponse>>
    {
        public async Task<IDataResult<AnalyzeDamagePhotosResponse>> Handle(AnalyzeDamagePhotosCommand request, CancellationToken cancellationToken)
        {
            // No code path may turn caller-supplied URLs or filesystem paths into bytes.
            return await Task.FromResult(new ErrorDataResult<AnalyzeDamagePhotosResponse>(
                "Damage photo analysis requires authorized media asset IDs and is not enabled yet."));
        }
    }
}
