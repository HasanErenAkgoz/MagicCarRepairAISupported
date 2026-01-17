namespace MagicCarRepairAISupported.Application.Common.Services.AI.Dtos
{
    /// <summary>
    /// Müşteri analizi yanıtı DTO
    /// </summary>
    public class CustomerAnalysisResponseDto
    {
        /// <summary>
        /// Müşteri analizleri
        /// </summary>
        public List<CustomerAnalysisDto> Analyses { get; set; } = new();

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
    /// Müşteri analizi DTO
    /// </summary>
    public class CustomerAnalysisDto
    {
        /// <summary>
        /// Müşteri ID
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Müşteri adı
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// Müşteri segmenti (VIP, Sadık, Yeni, Risk Altında, vb.)
        /// </summary>
        public string CustomerSegment { get; set; } = string.Empty;

        /// <summary>
        /// Müşteri değeri (Yüksek, Orta, Düşük)
        /// </summary>
        public string CustomerValue { get; set; } = string.Empty;

        /// <summary>
        /// Toplam iş emri sayısı
        /// </summary>
        public int TotalWorkOrders { get; set; }

        /// <summary>
        /// Toplam fatura tutarı
        /// </summary>
        public decimal TotalRevenue { get; set; }

        /// <summary>
        /// Ortalama iş emri tutarı
        /// </summary>
        public decimal AverageWorkOrderAmount { get; set; }

        /// <summary>
        /// Ortalama memnuniyet skoru (1-5)
        /// </summary>
        public double AverageRating { get; set; }

        /// <summary>
        /// Toplam araç sayısı
        /// </summary>
        public int TotalVehicles { get; set; }

        /// <summary>
        /// Son ziyaret tarihi
        /// </summary>
        public DateTime? LastVisitDate { get; set; }

        /// <summary>
        /// Son iş emri tarihinden bu yana geçen gün sayısı
        /// </summary>
        public int DaysSinceLastVisit { get; set; }

        /// <summary>
        /// Trend (Artan, Azalan, Stabil)
        /// </summary>
        public string Trend { get; set; } = string.Empty;

        /// <summary>
        /// Risk faktörleri (Churn risk, ödeme gecikmesi, vb.)
        /// </summary>
        public List<string> RiskFactors { get; set; } = new();

        /// <summary>
        /// Öneriler
        /// </summary>
        public List<string> Recommendations { get; set; } = new();

        /// <summary>
        /// Ödeme davranışı (İyi, Orta, Kötü)
        /// </summary>
        public string PaymentBehavior { get; set; } = string.Empty;

        /// <summary>
        /// Ortalama ödeme gecikmesi (gün)
        /// </summary>
        public double AveragePaymentDelayDays { get; set; }

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
