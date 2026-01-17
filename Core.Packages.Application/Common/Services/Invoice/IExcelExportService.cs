namespace MagicCarRepairAISupported.Application.Common.Services.Invoice
{
    public interface IExcelExportService
    {
        Task<byte[]> ExportInvoiceToExcelAsync(int invoiceId, CancellationToken cancellationToken = default);
    }
}






