namespace MagicCarRepairAISupported.Application.Common.Services.Invoice
{
    public interface IPdfInvoiceService
    {
        Task<byte[]> GenerateInvoicePdfAsync(int invoiceId, CancellationToken cancellationToken = default);
    }
}






