using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Parts.Queries.GetLowStockParts
{
    public class GetLowStockPartsResponse
    {
        public List<LowStockPartItem> Items { get; set; } = new();
        public int TotalCount { get; set; }
    }

    public class LowStockPartItem
    {
        public int Id { get; set; }
        public string PartCode { get; set; }
        public string Name { get; set; }
        public PartCategory Category { get; set; }
        public PartBrandType BrandType { get; set; }
        public int CurrentStock { get; set; }
        public int MinimumStockLevel { get; set; }
        public string? Location { get; set; }
        public string? SupplierName { get; set; }
        public bool IsLowStockAlertEnabled { get; set; }
    }
}

