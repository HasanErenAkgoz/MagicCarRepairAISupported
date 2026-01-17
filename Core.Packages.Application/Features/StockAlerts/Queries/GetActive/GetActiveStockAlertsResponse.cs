using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.StockAlerts.Queries.GetActive
{
    public class GetActiveStockAlertsResponse
    {
        public int Id { get; set; }
        public int PartId { get; set; }
        public string PartCode { get; set; } = string.Empty;
        public string PartName { get; set; } = string.Empty;
        public StockAlertType AlertType { get; set; }
        public string AlertTypeName { get; set; } = string.Empty;
        public decimal CurrentStock { get; set; }
        public decimal MinimumStock { get; set; }
        public decimal? RecommendedOrderQuantity { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime FirstAlertDate { get; set; }
        public bool AutoOrderCreated { get; set; }
        public int? AutoOrderId { get; set; }
    }
}

