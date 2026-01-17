using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.Import
{
    public class ImportPartsCommand : IRequest<ImportPartsResponse>
    {
        public byte[] FileData { get; set; } = Array.Empty<byte>();
        public string FileName { get; set; } = string.Empty;
        public string Format { get; set; } = "Excel"; // Excel, CSV
    }
}
