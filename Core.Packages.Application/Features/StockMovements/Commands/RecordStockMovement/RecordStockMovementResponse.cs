using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.StockMovements.Commands.RecordStockMovement
{
    public class RecordStockMovementResponse
    {
        public int MovementId { get; set; }
        public int PartId { get; set; }
        public string PartCode { get; set; }
        public string PartName { get; set; }
        public StockMovementType MovementType { get; set; }
        public int Quantity { get; set; }
        public int PreviousStock { get; set; }
        public int CurrentStock { get; set; }
        public bool IsLowStock { get; set; }
    }
}

