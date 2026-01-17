using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.StockMovements.Queries.GetStockMovementHistory
{
    public class GetStockMovementHistoryResponse
    {
        public List<StockMovementHistoryItem> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }

    public class StockMovementHistoryItem
    {
        public int Id { get; set; }
        public int PartId { get; set; }
        public string? PartCode { get; set; }
        public string? PartName { get; set; }
        public StockMovementType MovementType { get; set; }
        public int Quantity { get; set; }
        public DateTime MovementDate { get; set; }
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? Description { get; set; }
        public string? ReferenceNumber { get; set; }
        public string? ReferenceType { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}

