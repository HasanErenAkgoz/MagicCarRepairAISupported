using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Customers.Queries.Export
{
    public class ExportCustomersQuery : IRequest<byte[]>
    {
        public string Format { get; set; } = "Excel"; // Excel, CSV
    }
}
