namespace MagicCarRepairAISupported.Application.Common.Services.AI.Dtos
{
    /// <summary>
    /// AI fotoğraf analizi sonuç DTO'su
    /// </summary>
    public class PhotoAnalysisResultDto
    {
        /// <summary>
        /// Tespit edilen hasarlar
        /// </summary>
        public List<DamageDetectionDto> DetectedDamages { get; set; } = new();

        /// <summary>
        /// Hasar şiddeti skoru (0-100)
        /// </summary>
        public int DamageSeverityScore { get; set; }

        /// <summary>
        /// Önerilen parçalar
        /// </summary>
        public List<RecommendedPartDto> RecommendedParts { get; set; } = new();

        /// <summary>
        /// Önerilen işçilikler
        /// </summary>
        public List<RecommendedLaborDto> RecommendedLabors { get; set; } = new();

        /// <summary>
        /// Fotoğraf kalitesi skoru (0-100)
        /// </summary>
        public int PhotoQualityScore { get; set; }

        /// <summary>
        /// Fotoğraf kalitesi notları
        /// </summary>
        public string? PhotoQualityNotes { get; set; }

        /// <summary>
        /// Öneriler
        /// </summary>
        public string? Recommendations { get; set; }

        /// <summary>
        /// Sigorta hasarı için otomatik rapor (JSON formatında)
        /// </summary>
        public string? InsuranceReportJson { get; set; }
    }

    /// <summary>
    /// Tespit edilen hasar
    /// </summary>
    public class DamageDetectionDto
    {
        public string DamageType { get; set; } = string.Empty; // Çizik, Çökme, Yırtılma, vb.
        public string? Location { get; set; } // Ön kaput, ön tampon, vb.
        public int SeverityScore { get; set; } // 0-100
        public decimal? EstimatedRepairCost { get; set; }
        public string? Description { get; set; }
        public List<CoordinateDto>? Coordinates { get; set; } // Fotoğraf üzerindeki koordinatlar
    }

    /// <summary>
    /// Koordinat
    /// </summary>
    public class CoordinateDto
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}

