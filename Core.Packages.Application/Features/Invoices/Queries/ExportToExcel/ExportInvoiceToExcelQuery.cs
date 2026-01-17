using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Queries.ExportToExcel
{
    public class ExportInvoiceToExcelQuery : IRequest<byte[]>
    {
        public int InvoiceId { get; set; }
    }
}






