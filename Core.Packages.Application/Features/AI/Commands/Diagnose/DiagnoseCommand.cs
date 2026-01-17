using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Commands.Diagnose
{
    public class DiagnoseCommand : IRequest<IDataResult<DiagnosisResultDto>>
    {
        public string Complaint { get; set; } = string.Empty;
        public int? VehicleId { get; set; }
        public bool IsVoice { get; set; } = false;
        public byte[]? VoiceData { get; set; }
    }
}

