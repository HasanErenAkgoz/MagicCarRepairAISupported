namespace MagicCarRepairAISupported.Application.Features.AI.Queries.SuggestParts
{
    public class SuggestPartsResponse
    {
        public List<PartSuggestionDto> Suggestions { get; set; } = new();
        public string Explanation { get; set; } = string.Empty;
        public DateTime AnalysisDate { get; set; } = DateTime.UtcNow;
    }

    public class PartSuggestionDto
    {
        public int PartId { get; set; }
        public string PartName { get; set; } = string.Empty;
        public string PartCode { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string BrandType { get; set; } = string.Empty;
        public string? Brand { get; set; }
        public string? OEMNumber { get; set; }
        public decimal SalePrice { get; set; }
        public int CurrentStock { get; set; }
        public string Reason { get; set; } = string.Empty;
        public int SuitabilityScore { get; set; }
        public int Priority { get; set; }
    }
}
