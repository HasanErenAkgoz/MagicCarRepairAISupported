using MagicCarRepairAISupported.Application.Common.Services.Invoice;
using MagicCarRepairAISupported.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace MagicCarRepairAISupported.Infrastructure.Services.Invoice
{
    public class PdfInvoiceService : IPdfInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public PdfInvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public async Task<byte[]> GenerateInvoicePdfAsync(int invoiceId, CancellationToken cancellationToken = default)
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

            var pdfBytes = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header()
                        .Row(row =>
                        {
                            row.RelativeItem().Column(column =>
                            {
                                column.Item().Text($"FATURA #{invoice.InvoiceNumber}")
                                    .FontSize(20)
                                    .Bold()
                                    .FontColor(Colors.Blue.Darken3);
                            });

                            row.ConstantItem(100).AlignRight().Text($"Tarih: {invoice.InvoiceDate:dd.MM.yyyy}")
                                .FontSize(10);
                        });

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column =>
                        {
                            // Müşteri Bilgileri
                            column.Item().PaddingBottom(10).Row(row =>
                            {
                                row.RelativeItem().Column(c =>
                                {
                                    c.Item().Text("FATURA EDİLEN").Bold().FontSize(12);
                                    c.Item().Text(invoice.Customer?.FullName ?? "N/A");
                                    c.Item().Text(invoice.Customer?.Email ?? "");
                                    c.Item().Text(invoice.Customer?.PhoneNumber ?? "");
                                    c.Item().Text(invoice.Customer?.Address ?? "");
                                });

                                row.RelativeItem().Column(c =>
                                {
                                    c.Item().Text("FATURA BİLGİLERİ").Bold().FontSize(12);
                                    c.Item().Text($"Fatura No: {invoice.InvoiceNumber}");
                                    c.Item().Text($"Fatura Tarihi: {invoice.InvoiceDate:dd.MM.yyyy}");
                                    if (invoice.DueDate.HasValue)
                                    {
                                        c.Item().Text($"Vade Tarihi: {invoice.DueDate.Value:dd.MM.yyyy}");
                                    }
                                    c.Item().Text($"Durum: {invoice.Status}");
                                });
                            });

                            // İş Emri Bilgileri (varsa)
                            if (invoice.WorkOrder != null)
                            {
                                column.Item().PaddingBottom(10).Column(c =>
                                {
                                    c.Item().Text("İŞ EMRI BİLGİLERİ").Bold().FontSize(12);
                                    c.Item().Text($"İş Emri No: {invoice.WorkOrder.WorkOrderNumber}");
                                    if (invoice.WorkOrder.Vehicle != null)
                                    {
                                        c.Item().Text($"Araç: {invoice.WorkOrder.Vehicle.Brand} {invoice.WorkOrder.Vehicle.Model} - {invoice.WorkOrder.Vehicle.LicensePlate}");
                                    }
                                });
                            }

                            // Fatura Kalemleri
                            column.Item().PaddingTop(10).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(3);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Element(CellStyle).Text("Açıklama").Bold();
                                    header.Cell().Element(CellStyle).AlignRight().Text("Miktar").Bold();
                                    header.Cell().Element(CellStyle).AlignRight().Text("Birim Fiyat").Bold();
                                    header.Cell().Element(CellStyle).AlignRight().Text("KDV %").Bold();
                                    header.Cell().Element(CellStyle).AlignRight().Text("Toplam").Bold();
                                });

                                foreach (var item in invoice.Items ?? new List<Domain.Entities.InvoiceItem>())
                                {
                                    table.Cell().Element(CellStyle).Text(item.Description);
                                    table.Cell().Element(CellStyle).AlignRight().Text(item.Quantity.ToString("N2"));
                                    table.Cell().Element(CellStyle).AlignRight().Text(item.UnitPrice.ToString("N2") + " ₺");
                                    table.Cell().Element(CellStyle).AlignRight().Text(item.TaxRate.ToString("N0") + "%");
                                    table.Cell().Element(CellStyle).AlignRight().Text(item.TotalAmount.ToString("N2") + " ₺");
                                }
                            });

                            // Toplamlar
                            column.Item().PaddingTop(20).AlignRight().Column(c =>
                            {
                                c.Item().Row(row =>
                                {
                                    row.ConstantItem(150).Text("Ara Toplam:").Bold();
                                    row.ConstantItem(100).AlignRight().Text(invoice.SubTotal.ToString("N2") + " ₺");
                                });
                                c.Item().Row(row =>
                                {
                                    row.ConstantItem(150).Text("KDV Toplamı:").Bold();
                                    row.ConstantItem(100).AlignRight().Text(invoice.TaxAmount.ToString("N2") + " ₺");
                                });
                                c.Item().Row(row =>
                                {
                                    row.ConstantItem(150).Text("GENEL TOPLAM:").Bold().FontSize(14);
                                    row.ConstantItem(100).AlignRight().Text(invoice.TotalAmount.ToString("N2") + " ₺").Bold().FontSize(14);
                                });
                                if (invoice.PaidAmount > 0)
                                {
                                    c.Item().Row(row =>
                                    {
                                        row.ConstantItem(150).Text("Ödenen:").Bold();
                                        row.ConstantItem(100).AlignRight().Text(invoice.PaidAmount.ToString("N2") + " ₺");
                                    });
                                    c.Item().Row(row =>
                                    {
                                        row.ConstantItem(150).Text("Kalan:").Bold();
                                        row.ConstantItem(100).AlignRight().Text((invoice.TotalAmount - invoice.PaidAmount).ToString("N2") + " ₺");
                                    });
                                }
                            });

                            // Açıklama
                            if (!string.IsNullOrWhiteSpace(invoice.Description))
                            {
                                column.Item().PaddingTop(20).Text("Açıklama:").Bold();
                                column.Item().Text(invoice.Description);
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Bu fatura elektronik ortamda oluşturulmuştur.")
                                .FontSize(8)
                                .FontColor(Colors.Grey.Medium);
                        });
                });
            })
            .GeneratePdf();

            return pdfBytes;
        }

        private static IContainer CellStyle(IContainer container)
        {
            return container
                .BorderBottom(1)
                .BorderColor(Colors.Grey.Lighten2)
                .PaddingVertical(5)
                .PaddingHorizontal(5);
        }
    }
}

