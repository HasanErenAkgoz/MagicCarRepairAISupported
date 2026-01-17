namespace MagicCarRepairAISupported.Application.Common.Services.Export
{
    /// <summary>
    /// Genel export servisi interface'i
    /// </summary>
    public interface IExportService
    {
        /// <summary>
        /// Verileri Excel formatında export eder
        /// </summary>
        Task<byte[]> ExportToExcelAsync<T>(IEnumerable<T> data, string sheetName = "Data", CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Verileri CSV formatında export eder
        /// </summary>
        Task<byte[]> ExportToCsvAsync<T>(IEnumerable<T> data, CancellationToken cancellationToken = default) where T : class;

        /// <summary>
        /// Verileri PDF formatında export eder (gelecekte implement edilebilir)
        /// </summary>
        Task<byte[]> ExportToPdfAsync<T>(IEnumerable<T> data, string title, CancellationToken cancellationToken = default) where T : class;
    }
}
