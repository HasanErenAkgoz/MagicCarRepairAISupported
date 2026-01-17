using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Commands.Diagnose
{
    public class DiagnoseCommandHandler : IRequestHandler<DiagnoseCommand, IDataResult<DiagnosisResultDto>>
    {
        private readonly IAIDiagnosisService _aiDiagnosisService;

        public DiagnoseCommandHandler(IAIDiagnosisService aiDiagnosisService)
        {
            _aiDiagnosisService = aiDiagnosisService;
        }

        public async Task<IDataResult<DiagnosisResultDto>> Handle(DiagnoseCommand request, CancellationToken cancellationToken)
        {
            DiagnosisResultDto result;

            if (request.IsVoice && request.VoiceData != null && request.VoiceData.Length > 0)
            {
                result = await _aiDiagnosisService.DiagnoseFromVoiceAsync(request.VoiceData, request.VehicleId, cancellationToken);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(request.Complaint))
                {
                    return new ErrorDataResult<DiagnosisResultDto>("Şikayet metni boş olamaz");
                }

                result = await _aiDiagnosisService.DiagnoseFromTextAsync(request.Complaint, request.VehicleId, cancellationToken);
            }

            return new SuccessDataResult<DiagnosisResultDto>(result, "Arıza tespiti başarıyla tamamlandı");
        }
    }
}

