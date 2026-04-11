namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Queries.GetMyWorkOrders
{
    public class GetMyWorkOrdersResponse
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleModel { get; set; }
        public string VehiclePlate { get; set; }
        public string ServiceTitle { get; set; }
        public string? ShopName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public DateTime? ActualDeliveryDate { get; set; }
        public string Status { get; set; }
        public string StatusName { get; set; }
        public decimal Total { get; set; }
        public string PaymentStatus { get; set; }
        public bool RequiresCustomerApproval { get; set; }
    }
}
