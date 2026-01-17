using ClosedXML.Excel;
using MagicCarRepairAISupported.Application.Common.Services.Import;
using System.Reflection;
using System.Text;

namespace MagicCarRepairAISupported.Infrastructure.Services.Import
{
    public class ImportService : IImportService
    {
        public Task<ImportResult<T>> ImportFromExcelAsync<T>(byte[] fileData, CancellationToken cancellationToken = default) where T : class, new()
        {
            var result = new ImportResult<T>();

            using var stream = new MemoryStream(fileData);
            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheets.First();

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite)
                .ToList();

            // Header mapping
            var headerRow = worksheet.FirstRow();
            var columnMapping = new Dictionary<string, int>();
            for (int col = 1; col <= headerRow.CellsUsed().Count(); col++)
            {
                var headerValue = headerRow.Cell(col).GetString().Trim();
                columnMapping[headerValue] = col;
            }

            result.TotalRows = worksheet.RowsUsed().Count() - 1; // Header hariç

            // Data rows
            int rowNumber = 2;
            foreach (var row in worksheet.RowsUsed().Skip(1))
            {
                try
                {
                    var item = new T();
                    bool hasError = false;

                    foreach (var prop in properties)
                    {
                        if (columnMapping.TryGetValue(prop.Name, out int colIndex))
                        {
                            try
                            {
                                var cellValue = row.Cell(colIndex).GetString();
                                SetPropertyValue(item, prop, cellValue);
                            }
                            catch (Exception ex)
                            {
                                result.Errors.Add(new ImportError
                                {
                                    RowNumber = rowNumber,
                                    Field = prop.Name,
                                    ErrorMessage = ex.Message
                                });
                                hasError = true;
                            }
                        }
                    }

                    if (!hasError)
                    {
                        result.SuccessItems.Add(item);
                        result.SuccessCount++;
                    }
                    else
                    {
                        result.ErrorCount++;
                    }
                }
                catch (Exception ex)
                {
                    result.Errors.Add(new ImportError
                    {
                        RowNumber = rowNumber,
                        Field = "General",
                        ErrorMessage = ex.Message
                    });
                    result.ErrorCount++;
                }

                rowNumber++;
            }

            return Task.FromResult(result);
        }

        public Task<ImportResult<T>> ImportFromCsvAsync<T>(byte[] fileData, CancellationToken cancellationToken = default) where T : class, new()
        {
            var result = new ImportResult<T>();
            var csvContent = Encoding.UTF8.GetString(fileData);
            var lines = csvContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length < 2)
            {
                result.Errors.Add(new ImportError
                {
                    RowNumber = 0,
                    Field = "General",
                    ErrorMessage = "CSV file must contain at least a header row and one data row"
                });
                return Task.FromResult(result);
            }

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite)
                .ToList();

            // Parse header
            var headers = ParseCsvLine(lines[0]);
            var columnMapping = new Dictionary<string, int>();
            for (int i = 0; i < headers.Length; i++)
            {
                columnMapping[headers[i].Trim()] = i;
            }

            result.TotalRows = lines.Length - 1;

            // Parse data rows
            for (int rowIndex = 1; rowIndex < lines.Length; rowIndex++)
            {
                try
                {
                    var values = ParseCsvLine(lines[rowIndex]);
                    var item = new T();
                    bool hasError = false;

                    foreach (var prop in properties)
                    {
                        if (columnMapping.TryGetValue(prop.Name, out int colIndex) && colIndex < values.Length)
                        {
                            try
                            {
                                SetPropertyValue(item, prop, values[colIndex]);
                            }
                            catch (Exception ex)
                            {
                                result.Errors.Add(new ImportError
                                {
                                    RowNumber = rowIndex + 1,
                                    Field = prop.Name,
                                    ErrorMessage = ex.Message
                                });
                                hasError = true;
                            }
                        }
                    }

                    if (!hasError)
                    {
                        result.SuccessItems.Add(item);
                        result.SuccessCount++;
                    }
                    else
                    {
                        result.ErrorCount++;
                    }
                }
                catch (Exception ex)
                {
                    result.Errors.Add(new ImportError
                    {
                        RowNumber = rowIndex + 1,
                        Field = "General",
                        ErrorMessage = ex.Message
                    });
                    result.ErrorCount++;
                }
            }

            return Task.FromResult(result);
        }

        public Task<byte[]> GenerateImportTemplateAsync<T>(CancellationToken cancellationToken = default) where T : class, new()
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Template");

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite)
                .ToList();

            // Header
            for (int i = 0; i < properties.Count; i++)
            {
                worksheet.Cell(1, i + 1).Value = properties[i].Name;
                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightGray;
            }

            // Example row (optional - can be removed)
            var exampleItem = new T();
            for (int i = 0; i < properties.Count; i++)
            {
                var value = properties[i].GetValue(exampleItem);
                worksheet.Cell(2, i + 1).Value = value?.ToString() ?? string.Empty;
                worksheet.Cell(2, i + 1).Style.Font.Italic = true;
                worksheet.Cell(2, i + 1).Style.Font.FontColor = XLColor.Gray;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return Task.FromResult(stream.ToArray());
        }

        private void SetPropertyValue<T>(T item, PropertyInfo property, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                if (property.PropertyType.IsValueType && Nullable.GetUnderlyingType(property.PropertyType) == null)
                {
                    return; // Skip non-nullable value types
                }
                property.SetValue(item, null);
                return;
            }

            var propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

            if (propertyType == typeof(string))
            {
                property.SetValue(item, value);
            }
            else if (propertyType == typeof(int))
            {
                if (int.TryParse(value, out int intValue))
                    property.SetValue(item, intValue);
            }
            else if (propertyType == typeof(decimal))
            {
                if (decimal.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal decimalValue))
                    property.SetValue(item, decimalValue);
            }
            else if (propertyType == typeof(DateTime))
            {
                if (DateTime.TryParse(value, out DateTime dateValue))
                    property.SetValue(item, dateValue);
            }
            else if (propertyType == typeof(bool))
            {
                if (bool.TryParse(value, out bool boolValue))
                    property.SetValue(item, boolValue);
            }
            else if (propertyType.IsEnum)
            {
                if (Enum.TryParse(propertyType, value, true, out object? enumValue))
                    property.SetValue(item, enumValue);
            }
        }

        private string[] ParseCsvLine(string line)
        {
            var values = new List<string>();
            var currentValue = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        currentValue.Append('"');
                        i++; // Skip next quote
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == ',' && !inQuotes)
                {
                    values.Add(currentValue.ToString());
                    currentValue.Clear();
                }
                else
                {
                    currentValue.Append(c);
                }
            }

            values.Add(currentValue.ToString());
            return values.ToArray();
        }
    }
}
