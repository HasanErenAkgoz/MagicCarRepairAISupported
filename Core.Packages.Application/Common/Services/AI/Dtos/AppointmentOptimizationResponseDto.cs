namespace MagicCarRepairAISupported.Application.Common.Services.AI.Dtos
{
    /// <summary>
    /// Randevu optimizasyon yanıtı DTO
    /// </summary>
    public class AppointmentOptimizationResponseDto
    {
        /// <summary>
        /// Önerilen randevu saatleri
        /// </summary>
        public List<AppointmentSuggestionDto> Suggestions { get; set; } = new();

        /// <summary>
        /// Optimizasyon açıklaması
        /// </summary>
        public string Explanation { get; set; } = string.Empty;
    }

    /// <summary>
    /// Randevu önerisi DTO
    /// </summary>
    public class AppointmentSuggestionDto
    {
        /// <summary>
        /// Önerilen tarih
        /// </summary>
        public DateTime SuggestedDate { get; set; }

        /// <summary>
        /// Önerilen başlangıç saati
        /// </summary>
        public TimeSpan SuggestedStartTime { get; set; }

        /// <summary>
        /// Önerilen bitiş saati
        /// </summary>
        public TimeSpan SuggestedEndTime { get; set; }

        /// <summary>
        /// Önerilen personel ID (opsiyonel)
        /// </summary>
        public int? SuggestedEmployeeId { get; set; }

        /// <summary>
        /// Önerilen personel adı
        /// </summary>
        public string? SuggestedEmployeeName { get; set; }

        /// <summary>
        /// Uygunluk skoru (0-100)
        /// </summary>
        public int SuitabilityScore { get; set; }

        /// <summary>
        /// Öneri nedeni
        /// </summary>
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// Personel yükü (mevcut randevu sayısı)
        /// </summary>
        public int EmployeeWorkload { get; set; }
    }
}
