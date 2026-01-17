namespace MagicCarRepairAISupported.Application.Common.Services.AI.Dtos
{
    /// <summary>
    /// Personel performans analizi yanıtı DTO
    /// </summary>
    public class EmployeePerformanceAnalysisResponseDto
    {
        /// <summary>
        /// Personel performans analizleri
        /// </summary>
        public List<EmployeePerformanceAnalysisDto> Analyses { get; set; } = new();

        /// <summary>
        /// Genel özet
        /// </summary>
        public string Summary { get; set; } = string.Empty;

        /// <summary>
        /// Analiz tarihi
        /// </summary>
        public DateTime AnalysisDate { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// Personel performans analizi DTO
    /// </summary>
    public class EmployeePerformanceAnalysisDto
    {
        /// <summary>
        /// Personel ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Personel adı
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// Personel pozisyonu
        /// </summary>
        public string Position { get; set; } = string.Empty;

        /// <summary>
        /// Performans seviyesi (Mükemmel, İyi, Orta, Düşük)
        /// </summary>
        public string PerformanceLevel { get; set; } = string.Empty;

        /// <summary>
        /// Performans skoru (0-100)
        /// </summary>
        public int PerformanceScore { get; set; }

        /// <summary>
        /// Toplam atanan iş emri sayısı
        /// </summary>
        public int TotalAssignedWorkOrders { get; set; }

        /// <summary>
        /// Tamamlanan iş emri sayısı
        /// </summary>
        public int CompletedWorkOrders { get; set; }

        /// <summary>
        /// Toplam çalışma saati
        /// </summary>
        public double TotalWorkHours { get; set; }

        /// <summary>
        /// Ortalama iş emri tamamlanma süresi (gün)
        /// </summary>
        public double AverageCompletionDays { get; set; }

        /// <summary>
        /// Toplam gelir katkısı
        /// </summary>
        public decimal TotalRevenueContribution { get; set; }

        /// <summary>
        /// Ortalama müşteri memnuniyeti (1-5)
        /// </summary>
        public double AverageCustomerSatisfaction { get; set; }

        /// <summary>
        /// Zamanında teslim oranı (%)
        /// </summary>
        public double OnTimeDeliveryRate { get; set; }

        /// <summary>
        /// Verimlilik skoru (0-100)
        /// </summary>
        public int EfficiencyScore { get; set; }

        /// <summary>
        /// Trend (Artan, Azalan, Stabil)
        /// </summary>
        public string Trend { get; set; } = string.Empty;

        /// <summary>
        /// Güçlü yönler
        /// </summary>
        public List<string> Strengths { get; set; } = new();

        /// <summary>
        /// Gelişim alanları
        /// </summary>
        public List<string> ImprovementAreas { get; set; } = new();

        /// <summary>
        /// Öneriler
        /// </summary>
        public List<string> Recommendations { get; set; } = new();

        /// <summary>
        /// Açıklama
        /// </summary>
        public string Explanation { get; set; } = string.Empty;

        /// <summary>
        /// Güven skoru (0-100)
        /// </summary>
        public int ConfidenceScore { get; set; }
    }
}
