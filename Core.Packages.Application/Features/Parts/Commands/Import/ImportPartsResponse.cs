using MagicCarRepairAISupported.Application.Common.Services.Import;

namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.Import
{
    public class ImportPartsResponse
    {
        public int TotalRows { get; set; }
        public int SuccessCount { get; set; }
        public int ErrorCount { get; set; }
        public List<ImportError> Errors { get; set; } = new();
    }
}
