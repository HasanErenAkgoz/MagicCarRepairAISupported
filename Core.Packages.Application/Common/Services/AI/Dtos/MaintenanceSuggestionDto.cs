namespace MagicCarRepairAISupported.Application.Common.Services.AI.Dtos
{
    /// <summary>
    /// Bakım önerisi DTO'su
    /// </summary>
    public class MaintenanceSuggestionDto
    {
        public string MaintenanceType { get; set; } = string.Empty; // Periyodik Bakım, Önleyici Bakım, vb.
        public string? Description { get; set; }
        public string? RecommendedParts { get; set; }
        public int? RecommendedKilometers { get; set; }
        public DateTime? RecommendedDate { get; set; }
        public int UrgencyLevel { get; set; } // 1-5 (1: Düşük, 5: Çok Acil)
        public string? Reason { get; set; }
        public decimal? EstimatedCost { get; set; }
    }

    /// <summary>
    /// Parça ömrü tahmini DTO'su
    /// </summary>
    public class PartLifespanEstimateDto
    {
        public int PartId { get; set; }
        public string PartName { get; set; } = string.Empty;
        public int EstimatedRemainingKilometers { get; set; }
        public int EstimatedRemainingMonths { get; set; }
        public DateTime? EstimatedReplacementDate { get; set; }
        public int ConditionScore { get; set; } // 0-100
        public string? Recommendations { get; set; }
    }
}

