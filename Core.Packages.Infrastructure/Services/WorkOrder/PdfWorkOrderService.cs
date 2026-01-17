using MagicCarRepairAISupported.Application.Common.Services.WorkOrder;
using MagicCarRepairAISupported.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace MagicCarRepairAISupported.Infrastructure.Services.WorkOrder
{
    public class PdfWorkOrderService : IPdfWorkOrderService
    {
        private readonly IWorkOrderRepository _workOrderRepository;

        public PdfWorkOrderService(IWorkOrderRepository workOrderRepository)
        {
            _workOrderRepository = workOrderRepository;
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public async Task<byte[]> GenerateWorkOrderPdfAsync(int workOrderId, CancellationToken cancellationToken = default)
        {
            var workOrder = await _workOrderRepository.Query()
                .Include(wo => wo.Items)
                .Include(wo => wo.Labors)
                .Include(wo => wo.Customer)
                .Include(wo => wo.Vehicle)
                .Include(wo => wo.AssignedEmployee)
                .FirstOrDefaultAsync(wo => wo.Id == workOrderId, cancellationToken);

            if (workOrder == null)
            {
                throw new InvalidOperationException("WorkOrder not found");
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
                                column.Item().Text($"İŞ EMRİ #{workOrder.WorkOrderNumber}")
                                    .FontSize(20)
                                    .Bold()
                                    .FontColor(Colors.Blue.Darken3);
                            });

                            row.ConstantItem(100).AlignRight().Text($"Tarih: {workOrder.EntryDate:dd.MM.yyyy}")
                                .FontSize(10);
                        });

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column =>
                        {
                            // Müşteri ve Araç Bilgileri
                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Column(c =>
                                {
                                    c.Item().Text("MÜŞTERİ BİLGİLERİ").Bold().FontSize(12);
                                    c.Item().Text(workOrder.Customer?.FullName ?? "N/A");
                                    c.Item().Text(workOrder.Customer?.PhoneNumber ?? "");
                                    c.Item().Text(workOrder.Customer?.Email ?? "");
                                });

                                row.RelativeItem().Column(c =>
                                {
                                    c.Item().Text("ARAÇ BİLGİLERİ").Bold().FontSize(12);
                                    c.Item().Text($"{workOrder.Vehicle?.Brand} {workOrder.Vehicle?.Model}");
                                    c.Item().Text($"Plaka: {workOrder.Vehicle?.LicensePlate ?? "N/A"}");
                                    c.Item().Text($"Km: {workOrder.Kilometers?.ToString("N0") ?? "N/A"}");
                                    c.Item().Text($"Yakıt: %{workOrder.FuelLevel ?? 0}");
                                });
                            });

                            column.Item().PaddingTop(10);

                            // Durum ve Öncelik
                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Text($"Durum: {workOrder.Status}").Bold();
                                row.RelativeItem().Text($"Öncelik: {workOrder.Priority}").Bold();
                                if (workOrder.AssignedEmployee != null)
                                {
                                    row.RelativeItem().Text($"Sorumlu: {workOrder.AssignedEmployee.FullName}").Bold();
                                }
                            });

                            column.Item().PaddingTop(10);

                            // Müşteri Şikayetleri
                            if (!string.IsNullOrWhiteSpace(workOrder.CustomerComplaints))
                            {
                                column.Item().Text("MÜŞTERİ ŞİKAYETLERİ").Bold().FontSize(12);
                                column.Item().Text(workOrder.CustomerComplaints);
                                column.Item().PaddingTop(5);
                            }

                            // Parçalar
                            if (workOrder.Items != null && workOrder.Items.Any())
                            {
                                column.Item().Text("KULLANILAN PARÇALAR").Bold().FontSize(12);
                                column.Item().Table(table =>
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
                                        header.Cell().Element(CellStyle).Text("Parça").Bold();
                                        header.Cell().Element(CellStyle).AlignRight().Text("Miktar").Bold();
                                        header.Cell().Element(CellStyle).AlignRight().Text("Birim Fiyat").Bold();
                                        header.Cell().Element(CellStyle).AlignRight().Text("KDV").Bold();
                                        header.Cell().Element(CellStyle).AlignRight().Text("Toplam").Bold();
                                    });

                                    foreach (var item in workOrder.Items)
                                    {
                                        table.Cell().Element(CellStyle).Text(item.Description ?? "N/A");
                                        table.Cell().Element(CellStyle).AlignRight().Text(item.Quantity.ToString("N2"));
                                        table.Cell().Element(CellStyle).AlignRight().Text(item.UnitPrice.ToString("N2") + " TL");
                                        table.Cell().Element(CellStyle).AlignRight().Text(item.TaxAmount.ToString("N2") + " TL");
                                        table.Cell().Element(CellStyle).AlignRight().Text(item.TotalAmount.ToString("N2") + " TL");
                                    }
                                });
                            }

                            // İşçilikler
                            if (workOrder.Labors != null && workOrder.Labors.Any())
                            {
                                column.Item().PaddingTop(10);
                                column.Item().Text("İŞÇİLİKLER").Bold().FontSize(12);
                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(3);
                                        columns.RelativeColumn(1);
                                        columns.RelativeColumn(1);
                                        columns.RelativeColumn(1);
                                    });

                                    table.Header(header =>
                                    {
                                        header.Cell().Element(CellStyle).Text("İşlem").Bold();
                                        header.Cell().Element(CellStyle).AlignRight().Text("Süre (saat)").Bold();
                                        header.Cell().Element(CellStyle).AlignRight().Text("Saat Ücreti").Bold();
                                        header.Cell().Element(CellStyle).AlignRight().Text("Toplam").Bold();
                                    });

                                    foreach (var labor in workOrder.Labors)
                                    {
                                        var laborDescription = !string.IsNullOrEmpty(labor.Description) ? labor.Description : labor.OperationName;
                                        table.Cell().Element(CellStyle).Text(laborDescription ?? "N/A");
                                        table.Cell().Element(CellStyle).AlignRight().Text((labor.DurationHours ?? 0).ToString("N2"));
                                        table.Cell().Element(CellStyle).AlignRight().Text(labor.HourlyRate.ToString("N2") + " TL");
                                        table.Cell().Element(CellStyle).AlignRight().Text(labor.TotalAmount.ToString("N2") + " TL");
                                    }
                                });
                            }

                            column.Item().PaddingTop(20);

                            // Özet
                            column.Item().AlignRight().Column(summary =>
                            {
                                summary.Item().Row(row =>
                                {
                                    row.RelativeItem().Text("Ara Toplam:").Bold();
                                    row.ConstantItem(100).AlignRight().Text(workOrder.SubTotal.ToString("N2") + " TL");
                                });

                                if (workOrder.DiscountAmount > 0)
                                {
                                    summary.Item().Row(row =>
                                    {
                                        row.RelativeItem().Text($"İndirim ({workOrder.DiscountPercentage}%):").Bold();
                                        row.ConstantItem(100).AlignRight().Text("-" + workOrder.DiscountAmount.ToString("N2") + " TL");
                                    });
                                }

                                summary.Item().Row(row =>
                                {
                                    row.RelativeItem().Text("KDV:").Bold();
                                    row.ConstantItem(100).AlignRight().Text(workOrder.TaxAmount.ToString("N2") + " TL");
                                });

                                summary.Item().Row(row =>
                                {
                                    row.RelativeItem().Text("TOPLAM:").Bold().FontSize(14);
                                    row.ConstantItem(100).AlignRight().Text(workOrder.TotalAmount.ToString("N2") + " TL").Bold().FontSize(14);
                                });

                                summary.Item().Row(row =>
                                {
                                    row.RelativeItem().Text("Ödeme Durumu:").Bold();
                                    row.ConstantItem(100).AlignRight().Text(workOrder.PaymentStatus.ToString());
                                });
                            });

                            // Notlar
                            if (!string.IsNullOrWhiteSpace(workOrder.Notes))
                            {
                                column.Item().PaddingTop(20).Text("NOTLAR").Bold().FontSize(12);
                                column.Item().Text(workOrder.Notes);
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Bu iş emri elektronik ortamda oluşturulmuştur.")
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
