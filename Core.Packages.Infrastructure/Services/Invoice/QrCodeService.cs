using MagicCarRepairAISupported.Application.Common.Services.Invoice;
using QRCoder;

namespace MagicCarRepairAISupported.Infrastructure.Services.Invoice
{
    public class QrCodeService : IQrCodeService
    {
        public byte[] GenerateQrCode(string data, int size = 300)
        {
            using var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);
            return qrCode.GetGraphic(20);
        }

        public string GenerateInvoiceQrCodeData(int invoiceId, decimal amount)
        {
            // QR Code içeriği: Invoice ID ve Amount
            // Gerçek uygulamada bu daha detaylı olabilir (URL, ödeme linki, vb.)
            return $"INVOICE:{invoiceId}|AMOUNT:{amount:N2}";
        }
    }
}






