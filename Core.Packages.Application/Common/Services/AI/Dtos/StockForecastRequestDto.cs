namespace MagicCarRepairAISupported.Application.Common.Services.AI.Dtos
{
    /// <summary>
    /// Stok tahmin isteği DTO
    /// </summary>
    public class StockForecastRequestDto
    {
        /// <summary>
        /// Parça ID (null ise tüm parçalar için tahmin yapılır)
        /// </summary>
        public int? PartId { get; set; }

        /// <summary>
        /// Tahmin periyodu (gün sayısı - varsayılan: 30)
        /// </summary>
        public int ForecastPeriodDays { get; set; } = 30;

        /// <summary>
        /// Geçmiş veri analizi için gün sayısı (varsayılan: 90)
        /// </summary>
        public int HistoricalDataDays { get; set; } = 90;

        /// <summary>
        /// Minimum stok seviyesi önerisi dahil edilsin mi?
        /// </summary>
        public bool IncludeMinimumStockRecommendation { get; set; } = true;

        /// <summary>
        /// Sipariş önerisi dahil edilsin mi?
        /// </summary>
        public bool IncludeOrderRecommendation { get; set; } = true;
    }
}
