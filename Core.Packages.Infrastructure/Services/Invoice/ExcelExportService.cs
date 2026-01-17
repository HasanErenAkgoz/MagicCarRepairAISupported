using ClosedXML.Excel;
using MagicCarRepairAISupported.Application.Common.Services.Invoice;
using MagicCarRepairAISupported.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Infrastructure.Services.Invoice
{
    public class ExcelExportService : IExcelExportService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public ExcelExportService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<byte[]> ExportInvoiceToExcelAsync(int invoiceId, CancellationToken cancellationToken = default)
        {
            var invoice = await _invoiceRepository.Query()
                .Include(i => i.Items)
                .Include(i => i.Customer)
                .Include(i => i.WorkOrder)
                .ThenInclude(wo => wo!.Vehicle)
                .FirstOrDefaultAsync(i => i.Id == invoiceId, cancellationToken);

            if (invoice == null)
            {
                throw new InvalidOperationException("Invoice not found");
            }

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Fatura");

            // Başlık
            worksheet.Cell(1, 1).Value = "FATURA";
            worksheet.Cell(1, 1).Style.Font.Bold = true;
            worksheet.Cell(1, 1).Style.Font.FontSize = 16;
            worksheet.Range(1, 1, 1, 5).Merge();

            // Fatura Bilgileri
            worksheet.Cell(3, 1).Value = "Fatura No:";
            worksheet.Cell(3, 2).Value = invoice.InvoiceNumber;
            worksheet.Cell(4, 1).Value = "Fatura Tarihi:";
            worksheet.Cell(4, 2).Value = invoice.InvoiceDate.ToString("dd.MM.yyyy");
            if (invoice.DueDate.HasValue)
            {
                worksheet.Cell(5, 1).Value = "Vade Tarihi:";
                worksheet.Cell(5, 2).Value = invoice.DueDate.Value.ToString("dd.MM.yyyy");
            }
            worksheet.Cell(6, 1).Value = "Durum:";
            worksheet.Cell(6, 2).Value = invoice.Status.ToString();

            // Müşteri Bilgileri
            int row = 8;
            worksheet.Cell(row, 1).Value = "MÜŞTERİ BİLGİLERİ";
            worksheet.Cell(row, 1).Style.Font.Bold = true;
            row++;
            if (invoice.Customer != null)
            {
                worksheet.Cell(row, 1).Value = "Ad Soyad:";
                worksheet.Cell(row, 2).Value = invoice.Customer.FullName;
                row++;
                worksheet.Cell(row, 1).Value = "Email:";
                worksheet.Cell(row, 2).Value = invoice.Customer.Email;
                row++;
                worksheet.Cell(row, 1).Value = "Telefon:";
                worksheet.Cell(row, 2).Value = invoice.Customer.PhoneNumber;
                row++;
                worksheet.Cell(row, 1).Value = "Adres:";
                worksheet.Cell(row, 2).Value = invoice.Customer.Address;
                row++;
            }

            // İş Emri Bilgileri
            if (invoice.WorkOrder != null)
            {
                row++;
                worksheet.Cell(row, 1).Value = "İŞ EMRI BİLGİLERİ";
                worksheet.Cell(row, 1).Style.Font.Bold = true;
                row++;
                worksheet.Cell(row, 1).Value = "İş Emri No:";
                worksheet.Cell(row, 2).Value = invoice.WorkOrder.WorkOrderNumber;
                row++;
                if (invoice.WorkOrder.Vehicle != null)
                {
                    worksheet.Cell(row, 1).Value = "Araç:";
                    worksheet.Cell(row, 2).Value = $"{invoice.WorkOrder.Vehicle.Brand} {invoice.WorkOrder.Vehicle.Model} - {invoice.WorkOrder.Vehicle.LicensePlate}";
                }
                row++;
            }

            // Fatura Kalemleri
            row += 2;
            worksheet.Cell(row, 1).Value = "Açıklama";
            worksheet.Cell(row, 2).Value = "Miktar";
            worksheet.Cell(row, 3).Value = "Birim Fiyat";
            worksheet.Cell(row, 4).Value = "KDV %";
            worksheet.Cell(row, 5).Value = "Toplam";
            var headerRange = worksheet.Range(row, 1, row, 5);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

            row++;
            foreach (var item in invoice.Items ?? new List<Domain.Entities.InvoiceItem>())
            {
                worksheet.Cell(row, 1).Value = item.Description;
                worksheet.Cell(row, 2).Value = item.Quantity;
                worksheet.Cell(row, 3).Value = item.UnitPrice;
                worksheet.Cell(row, 4).Value = item.TaxRate;
                worksheet.Cell(row, 5).Value = item.TotalAmount;
                row++;
            }

            // Toplamlar
            row++;
            worksheet.Cell(row, 4).Value = "Ara Toplam:";
            worksheet.Cell(row, 4).Style.Font.Bold = true;
            worksheet.Cell(row, 5).Value = invoice.SubTotal;
            row++;
            worksheet.Cell(row, 4).Value = "KDV Toplamı:";
            worksheet.Cell(row, 4).Style.Font.Bold = true;
            worksheet.Cell(row, 5).Value = invoice.TaxAmount;
            row++;
            worksheet.Cell(row, 4).Value = "GENEL TOPLAM:";
            worksheet.Cell(row, 4).Style.Font.Bold = true;
            worksheet.Cell(row, 5).Value = invoice.TotalAmount;
            worksheet.Cell(row, 5).Style.Font.Bold = true;

            if (invoice.PaidAmount > 0)
            {
                row++;
                worksheet.Cell(row, 4).Value = "Ödenen:";
                worksheet.Cell(row, 4).Style.Font.Bold = true;
                worksheet.Cell(row, 5).Value = invoice.PaidAmount;
                row++;
                worksheet.Cell(row, 4).Value = "Kalan:";
                worksheet.Cell(row, 4).Style.Font.Bold = true;
                worksheet.Cell(row, 5).Value = invoice.TotalAmount - invoice.PaidAmount;
            }

            // Kolon genişliklerini ayarla
            worksheet.Column(1).Width = 40;
            worksheet.Column(2).Width = 12;
            worksheet.Column(3).Width = 15;
            worksheet.Column(4).Width = 12;
            worksheet.Column(5).Width = 15;

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}






