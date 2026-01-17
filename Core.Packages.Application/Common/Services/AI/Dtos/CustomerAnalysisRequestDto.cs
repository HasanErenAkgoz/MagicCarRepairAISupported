namespace MagicCarRepairAISupported.Application.Common.Services.AI.Dtos
{
    /// <summary>
    /// Müşteri analizi isteği DTO
    /// </summary>
    public class CustomerAnalysisRequestDto
    {
        /// <summary>
        /// Müşteri ID (null ise tüm müşteriler için analiz yapılır)
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Analiz periyodu (gün sayısı - varsayılan: 365)
        /// </summary>
        public int AnalysisPeriodDays { get; set; } = 365;

        /// <summary>
        /// Detaylı analiz dahil edilsin mi?
        /// </summary>
        public bool IncludeDetailedAnalysis { get; set; } = true;

        /// <summary>
        /// Öneriler dahil edilsin mi?
        /// </summary>
        public bool IncludeRecommendations { get; set; } = true;
    }
}
