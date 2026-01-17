namespace MagicCarRepairAISupported.Application.Common.Services.AI.Dtos
{
    /// <summary>
    /// Stok tahmin yanıtı DTO
    /// </summary>
    public class StockForecastResponseDto
    {
        /// <summary>
        /// Parça stok tahminleri
        /// </summary>
        public List<PartStockForecastDto> Forecasts { get; set; } = new();

        /// <summary>
        /// Genel açıklama
        /// </summary>
        public string Summary { get; set; } = string.Empty;

        /// <summary>
        /// Analiz tarihi
        /// </summary>
        public DateTime AnalysisDate { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// Parça stok tahmini DTO
    /// </summary>
    public class PartStockForecastDto
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
        /// Mevcut stok miktarı
        /// </summary>
        public int CurrentStock { get; set; }

        /// <summary>
        /// Minimum stok seviyesi
        /// </summary>
        public int MinimumStockLevel { get; set; }

        /// <summary>
        /// Tahmin edilen tüketim (belirtilen periyot için)
        /// </summary>
        public int PredictedConsumption { get; set; }

        /// <summary>
        /// Tahmin edilen son stok (periyot sonunda)
        /// </summary>
        public int PredictedEndStock { get; set; }

        /// <summary>
        /// Stok tükenme riski (yüksek, orta, düşük)
        /// </summary>
        public string StockoutRisk { get; set; } = string.Empty;

        /// <summary>
        /// Stok tükenme tahmini tarihi
        /// </summary>
        public DateTime? PredictedStockoutDate { get; set; }

        /// <summary>
        /// Önerilen minimum stok seviyesi
        /// </summary>
        public int? RecommendedMinimumStock { get; set; }

        /// <summary>
        /// Önerilen sipariş miktarı
        /// </summary>
        public int? RecommendedOrderQuantity { get; set; }

        /// <summary>
        /// Trend (Artan, Azalan, Stabil)
        /// </summary>
        public string Trend { get; set; } = string.Empty;

        /// <summary>
        /// Güven skoru (0-100)
        /// </summary>
        public int ConfidenceScore { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        public string Explanation { get; set; } = string.Empty;

        /// <summary>
        /// Son 30 günlük ortalama günlük tüketim
        /// </summary>
        public double AverageDailyConsumption { get; set; }
    }
}
