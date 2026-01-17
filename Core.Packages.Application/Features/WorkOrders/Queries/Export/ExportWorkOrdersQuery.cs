using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.Export
{
    public class ExportWorkOrdersQuery : IRequest<byte[]>
    {
        public string Format { get; set; } = "Excel"; // Excel, CSV, PDF
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
    }
}
