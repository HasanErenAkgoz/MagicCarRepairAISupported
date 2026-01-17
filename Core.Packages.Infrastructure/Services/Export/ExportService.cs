using ClosedXML.Excel;
using MagicCarRepairAISupported.Application.Common.Services.Export;
using System.Reflection;
using System.Text;

namespace MagicCarRepairAISupported.Infrastructure.Services.Export
{
    public class ExportService : IExportService
    {
        public Task<byte[]> ExportToExcelAsync<T>(IEnumerable<T> data, string sheetName = "Data", CancellationToken cancellationToken = default) where T : class
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(sheetName);

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && (p.PropertyType.IsPrimitive || p.PropertyType == typeof(string) || p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?) || p.PropertyType == typeof(decimal) || p.PropertyType == typeof(decimal?)))
                .ToList();

            // Header
            for (int i = 0; i < properties.Count; i++)
            {
                worksheet.Cell(1, i + 1).Value = properties[i].Name;
                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightGray;
            }

            // Data
            int row = 2;
            foreach (var item in data)
            {
                for (int i = 0; i < properties.Count; i++)
                {
                    var value = properties[i].GetValue(item);
                    worksheet.Cell(row, i + 1).Value = value?.ToString() ?? string.Empty;
                }
                row++;
            }

            // Auto-fit columns
            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return Task.FromResult(stream.ToArray());
        }

        public Task<byte[]> ExportToCsvAsync<T>(IEnumerable<T> data, CancellationToken cancellationToken = default) where T : class
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && (p.PropertyType.IsPrimitive || p.PropertyType == typeof(string) || p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?) || p.PropertyType == typeof(decimal) || p.PropertyType == typeof(decimal?)))
                .ToList();

            var csv = new StringBuilder();

            // Header
            csv.AppendLine(string.Join(",", properties.Select(p => EscapeCsvValue(p.Name))));

            // Data
            foreach (var item in data)
            {
                var values = properties.Select(p =>
                {
                    var value = p.GetValue(item);
                    return EscapeCsvValue(value?.ToString() ?? string.Empty);
                });
                csv.AppendLine(string.Join(",", values));
            }

            return Task.FromResult(Encoding.UTF8.GetBytes(csv.ToString()));
        }

        public Task<byte[]> ExportToPdfAsync<T>(IEnumerable<T> data, string title, CancellationToken cancellationToken = default) where T : class
        {
            // PDF export için QuestPDF kullanılabilir, şimdilik NotImplementedException
            throw new NotImplementedException("PDF export will be implemented using QuestPDF");
        }

        private string EscapeCsvValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            // Eğer değer virgül, tırnak veya yeni satır içeriyorsa tırnak içine al
            if (value.Contains(',') || value.Contains('"') || value.Contains('\n') || value.Contains('\r'))
            {
                return $"\"{value.Replace("\"", "\"\"")}\"";
            }

            return value;
        }
    }
}
