namespace MagicCarRepairAISupported.Application.Common.Services.AI.Dtos
{
    /// <summary>
    /// AI arıza tespiti sonuç DTO'su
    /// </summary>
    public class DiagnosisResultDto
    {
        /// <summary>
        /// Tespit edilen arıza türleri (olasılık skorları ile)
        /// </summary>
        public List<DiagnosisItemDto> PossibleIssues { get; set; } = new();

        /// <summary>
        /// Önerilen parçalar
        /// </summary>
        public List<RecommendedPartDto> RecommendedParts { get; set; } = new();

        /// <summary>
        /// Önerilen işçilikler
        /// </summary>
        public List<RecommendedLaborDto> RecommendedLabors { get; set; } = new();

        /// <summary>
        /// Tahmini süre (gün)
        /// </summary>
        public int? EstimatedDays { get; set; }

        /// <summary>
        /// Tahmini maliyet
        /// </summary>
        public decimal? EstimatedCost { get; set; }

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
    /// Tespit edilen arıza öğesi
    /// </summary>
    public class DiagnosisItemDto
    {
        public string IssueName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int ProbabilityScore { get; set; } // 0-100
        public string? Category { get; set; } // Motor, Elektrik, Şanzıman, vb.
    }

    /// <summary>
    /// Önerilen parça
    /// </summary>
    public class RecommendedPartDto
    {
        public string PartName { get; set; } = string.Empty;
        public string? PartCode { get; set; }
        public string? Category { get; set; }
        public decimal? EstimatedPrice { get; set; }
        public int Quantity { get; set; } = 1;
        public int ProbabilityScore { get; set; } // 0-100
    }

    /// <summary>
    /// Önerilen işçilik
    /// </summary>
    public class RecommendedLaborDto
    {
        public string LaborName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal? EstimatedPrice { get; set; }
        public decimal? EstimatedHours { get; set; }
        public int ProbabilityScore { get; set; } // 0-100
    }
}

