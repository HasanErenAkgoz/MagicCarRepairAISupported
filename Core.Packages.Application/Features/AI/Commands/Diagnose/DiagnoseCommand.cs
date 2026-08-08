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
        /// <summary>Server-issued AI diagnosis draft assets. Raw URLs and paths are not accepted.</summary>
        public List<int>? MediaAssetIds { get; set; }
        /// <summary>ISO 639-1 language code from Accept-Language header (e.g. "tr", "en"). Default: "tr".</summary>
        public string Language { get; set; } = "tr";
    }
}
