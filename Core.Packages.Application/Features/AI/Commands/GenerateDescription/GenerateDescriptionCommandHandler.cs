using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Commands.GenerateDescription
{
    public class GenerateDescriptionCommandHandler : IRequestHandler<GenerateDescriptionCommand, IDataResult<GenerateDescriptionResultDto>>
    {
        private readonly IAIDiagnosisService _aiDiagnosisService;

        public GenerateDescriptionCommandHandler(IAIDiagnosisService aiDiagnosisService)
        {
            _aiDiagnosisService = aiDiagnosisService;
        }

        public async Task<IDataResult<GenerateDescriptionResultDto>> Handle(GenerateDescriptionCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.ShopName))
                return new ErrorDataResult<GenerateDescriptionResultDto>("Servis adı boş olamaz.");

            var result = await _aiDiagnosisService.GenerateShopDescriptionAsync(
                request.ShopName,
                request.Address,
                request.Phone,
                cancellationToken);

            return new SuccessDataResult<GenerateDescriptionResultDto>(result, "Açıklama başarıyla oluşturuldu.");
        }
    }
}
