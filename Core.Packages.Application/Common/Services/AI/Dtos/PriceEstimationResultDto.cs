namespace MagicCarRepairAISupported.Application.Common.Services.AI.Dtos
{
    /// <summary>
    /// Fiyat tahmini sonuç DTO'su
    /// </summary>
    public class PriceEstimationResultDto
    {
        /// <summary>
        /// Tahmini toplam tutar
        /// </summary>
        public decimal EstimatedTotalAmount { get; set; }

        /// <summary>
        /// Tahmini parça tutarı
        /// </summary>
        public decimal EstimatedPartsAmount { get; set; }

        /// <summary>
        /// Tahmini işçilik tutarı
        /// </summary>
        public decimal EstimatedLaborAmount { get; set; }

        /// <summary>
        /// Tahmini KDV tutarı
        /// </summary>
        public decimal EstimatedTaxAmount { get; set; }

        /// <summary>
        /// Önerilen indirim tutarı
        /// </summary>
        public decimal? RecommendedDiscountAmount { get; set; }

        /// <summary>
        /// Önerilen indirim yüzdesi
        /// </summary>
        public decimal? RecommendedDiscountPercentage { get; set; }

        /// <summary>
        /// İndirim önerisi nedeni
        /// </summary>
        public string? DiscountReason { get; set; }

        /// <summary>
        /// Piyasa fiyat analizi
        /// </summary>
        public MarketPriceAnalysisDto? MarketPriceAnalysis { get; set; }

        /// <summary>
        /// Güven skoru (0-100)
        /// </summary>
        public int ConfidenceScore { get; set; }

        /// <summary>
        /// Öneriler
        /// </summary>
        public string? Recommendations { get; set; }
    }

    /// <summary>
    /// Piyasa fiyat analizi
    /// </summary>
    public class MarketPriceAnalysisDto
    {
        public decimal AverageMarketPrice { get; set; }
        public decimal MinMarketPrice { get; set; }
        public decimal MaxMarketPrice { get; set; }
        public decimal OurPrice { get; set; }
        public decimal? PriceDifference { get; set; }
        public string? CompetitivenessLevel { get; set; } // Çok Rekabetçi, Rekabetçi, Orta, Yüksek
    }
}

