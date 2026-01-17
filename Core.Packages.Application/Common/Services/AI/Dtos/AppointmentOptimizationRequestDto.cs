namespace MagicCarRepairAISupported.Application.Common.Services.AI.Dtos
{
    /// <summary>
    /// Randevu optimizasyon isteği DTO
    /// </summary>
    public class AppointmentOptimizationRequestDto
    {
        /// <summary>
        /// Müşteri ID
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Araç ID (opsiyonel)
        /// </summary>
        public int? VehicleId { get; set; }

        /// <summary>
        /// Randevu türü
        /// </summary>
        public string AppointmentType { get; set; } = string.Empty;

        /// <summary>
        /// Tercih edilen tarih aralığı (başlangıç)
        /// </summary>
        public DateTime? PreferredStartDate { get; set; }

        /// <summary>
        /// Tercih edilen tarih aralığı (bitiş)
        /// </summary>
        public DateTime? PreferredEndDate { get; set; }

        /// <summary>
        /// Tercih edilen saat aralığı (başlangıç)
        /// </summary>
        public TimeSpan? PreferredStartTime { get; set; }

        /// <summary>
        /// Tercih edilen saat aralığı (bitiş)
        /// </summary>
        public TimeSpan? PreferredEndTime { get; set; }

        /// <summary>
        /// Tahmini süre (dakika)
        /// </summary>
        public int? EstimatedDurationMinutes { get; set; }

        /// <summary>
        /// Öncelik seviyesi
        /// </summary>
        public string? Priority { get; set; }

        /// <summary>
        /// Önerilecek randevu sayısı (varsayılan: 5)
        /// </summary>
        public int NumberOfSuggestions { get; set; } = 5;
    }
}
