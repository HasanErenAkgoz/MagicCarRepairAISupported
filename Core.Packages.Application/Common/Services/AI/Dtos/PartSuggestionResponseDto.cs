namespace MagicCarRepairAISupported.Application.Common.Services.AI.Dtos
{
    /// <summary>
    /// Parça önerisi yanıtı DTO
    /// </summary>
    public class PartSuggestionResponseDto
    {
        /// <summary>
        /// Önerilen parçalar
        /// </summary>
        public List<PartSuggestionDto> Suggestions { get; set; } = new();

        /// <summary>
        /// Öneri açıklaması
        /// </summary>
        public string Explanation { get; set; } = string.Empty;

        /// <summary>
        /// Analiz tarihi
        /// </summary>
        public DateTime AnalysisDate { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// Parça önerisi DTO
    /// </summary>
    public class PartSuggestionDto
    {
        /// <summary>
        /// Parça ID
        /// </summary>
        public int PartId { get; set; }

        /// <summary>
        /// Parça adı
        /// </summary>
        public string PartName { get; set; } = string.Empty;

        /// <summary>
        /// Parça kodu
        /// </summary>
        public string PartCode { get; set; } = string.Empty;

        /// <summary>
        /// Parça kategorisi
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Marka tipi (Orijinal/Emsal)
        /// </summary>
        public string BrandType { get; set; } = string.Empty;

        /// <summary>
        /// Marka
        /// </summary>
        public string? Brand { get; set; }

        /// <summary>
        /// OEM numarası
        /// </summary>
        public string? OEMNumber { get; set; }

        /// <summary>
        /// Satış fiyatı
        /// </summary>
        public decimal SalePrice { get; set; }

        /// <summary>
        /// Mevcut stok
        /// </summary>
        public int CurrentStock { get; set; }

        /// <summary>
        /// Öneri nedeni
        /// </summary>
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// Uygunluk skoru (0-100)
        /// </summary>
        public int SuitabilityScore { get; set; }

        /// <summary>
        /// Öncelik sırası
        /// </summary>
        public int Priority { get; set; }
    }
}
