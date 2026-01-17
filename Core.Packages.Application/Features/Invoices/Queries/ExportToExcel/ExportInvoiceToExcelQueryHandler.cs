using MagicCarRepairAISupported.Application.Common.Services.Invoice;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Queries.ExportToExcel
{
    public class ExportInvoiceToExcelQueryHandler : IRequestHandler<ExportInvoiceToExcelQuery, byte[]>
    {
        private readonly IExcelExportService _excelExportService;

        public ExportInvoiceToExcelQueryHandler(IExcelExportService excelExportService)
        {
            _excelExportService = excelExportService;
        }

        public async Task<byte[]> Handle(ExportInvoiceToExcelQuery request, CancellationToken cancellationToken)
        {
            return await _excelExportService.ExportInvoiceToExcelAsync(request.InvoiceId, cancellationToken);
        }
    }
}






