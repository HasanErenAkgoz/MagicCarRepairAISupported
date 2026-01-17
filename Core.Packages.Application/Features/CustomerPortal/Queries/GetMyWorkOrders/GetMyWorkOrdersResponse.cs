namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Queries.GetMyWorkOrders
{
    public class GetMyWorkOrdersResponse
    {
        public int Id { get; set; }
        public string WorkOrderNumber { get; set; }
        public int VehicleId { get; set; }
        public string VehicleLicensePlate { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleModel { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public DateTime? ActualDeliveryDate { get; set; }
        public string Status { get; set; }
        public string StatusName { get; set; }
        public string Priority { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; }
    }
}






