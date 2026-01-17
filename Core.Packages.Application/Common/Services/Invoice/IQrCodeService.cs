namespace MagicCarRepairAISupported.Application.Common.Services.Invoice
{
    public interface IQrCodeService
    {
        byte[] GenerateQrCode(string data, int size = 300);
        string GenerateInvoiceQrCodeData(int invoiceId, decimal amount);
    }
}






