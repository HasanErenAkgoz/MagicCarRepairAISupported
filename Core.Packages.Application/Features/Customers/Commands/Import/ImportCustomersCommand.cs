using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Customers.Commands.Import
{
    public class ImportCustomersCommand : IRequest<ImportCustomersResponse>
    {
        public byte[] FileData { get; set; } = Array.Empty<byte>();
        public string FileName { get; set; } = string.Empty;
        public string Format { get; set; } = "Excel"; // Excel, CSV
    }
}
