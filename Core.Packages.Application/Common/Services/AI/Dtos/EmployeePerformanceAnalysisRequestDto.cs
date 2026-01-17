namespace MagicCarRepairAISupported.Application.Common.Services.AI.Dtos
{
    /// <summary>
    /// Personel performans analizi isteği DTO
    /// </summary>
    public class EmployeePerformanceAnalysisRequestDto
    {
        /// <summary>
        /// Personel ID (null ise tüm personeller için analiz yapılır)
        /// </summary>
        public int? EmployeeId { get; set; }

        /// <summary>
        /// Analiz periyodu (gün sayısı - varsayılan: 90)
        /// </summary>
        public int AnalysisPeriodDays { get; set; } = 90;

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
