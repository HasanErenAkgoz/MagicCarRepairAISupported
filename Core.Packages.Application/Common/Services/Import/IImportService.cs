namespace MagicCarRepairAISupported.Application.Common.Services.Import
{
    /// <summary>
    /// Import sonuç modeli
    /// </summary>
    public class ImportResult<T>
    {
        public int TotalRows { get; set; }
        public int SuccessCount { get; set; }
        public int ErrorCount { get; set; }
        public List<ImportError> Errors { get; set; } = new();
        public List<T> SuccessItems { get; set; } = new();
    }

    /// <summary>
    /// Import hatası
    /// </summary>
    public class ImportError
    {
        public int RowNumber { get; set; }
        public string Field { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
    }

    /// <summary>
    /// Genel import servisi interface'i
    /// </summary>
    public interface IImportService
    {
        /// <summary>
        /// Excel dosyasından veri import eder
        /// </summary>
        Task<ImportResult<T>> ImportFromExcelAsync<T>(byte[] fileData, CancellationToken cancellationToken = default) where T : class, new();

        /// <summary>
        /// CSV dosyasından veri import eder
        /// </summary>
        Task<ImportResult<T>> ImportFromCsvAsync<T>(byte[] fileData, CancellationToken cancellationToken = default) where T : class, new();

        /// <summary>
        /// Import template'i oluşturur (boş Excel dosyası)
        /// </summary>
        Task<byte[]> GenerateImportTemplateAsync<T>(CancellationToken cancellationToken = default) where T : class, new();
    }
}
