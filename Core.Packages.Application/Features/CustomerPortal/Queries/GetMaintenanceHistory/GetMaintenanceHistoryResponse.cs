namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Queries.GetMaintenanceHistory
{
    public class GetMaintenanceHistoryResponse
    {
        public int WorkOrderId { get; set; }
        public string WorkOrderNumber { get; set; }
        public int VehicleId { get; set; }
        public string VehicleLicensePlate { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleModel { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Description { get; set; }
    }
}






