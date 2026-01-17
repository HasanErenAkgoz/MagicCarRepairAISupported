using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Queries.Export
{
    public class ExportPartsQuery : IRequest<byte[]>
    {
        public string Format { get; set; } = "Excel"; // Excel, CSV
        public bool IncludeStock { get; set; } = true; // Stok bilgilerini dahil et
    }
}
