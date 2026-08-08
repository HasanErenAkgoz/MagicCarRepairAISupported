namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Queries.GetMyWorkOrderDetails
{
    public class GetMyWorkOrderDetailsResponse
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public string Status { get; set; }
        public string StatusName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public DateTime? ActualDeliveryDate { get; set; }
        public WorkOrderVehicleDto Vehicle { get; set; }
        public string ServiceTitle { get; set; }
        public string? ServiceDescription { get; set; }
        public string? TechnicianNotes { get; set; }
        public string? ShopName { get; set; }
        public int? ShopUserId { get; set; }
        public string? ShopUserName { get; set; }
        public List<WorkOrderPartDto> Parts { get; set; } = new();
        public List<WorkOrderLaborDto> Labor { get; set; } = new();
        public decimal PartsSubtotal { get; set; }
        public decimal LaborSubtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Total { get; set; }
        public string PaymentStatus { get; set; }
        public bool RequiresCustomerApproval { get; set; }
        public List<WorkOrderTimelineDto> Timeline { get; set; } = new();
        public List<WorkOrderPhotoDto> Photos { get; set; } = new();
    }

    public class WorkOrderVehicleDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Plate { get; set; }
    }

    public class WorkOrderPartDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }

    public class WorkOrderLaborDto
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public decimal Hours { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal Total { get; set; }
    }

    public class WorkOrderTimelineDto
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }

    public class WorkOrderPhotoDto
    {
        public int Id { get; set; }
        /// <summary>Customer-authorized media endpoint.</summary>
        public string MediaUrl { get; set; } = string.Empty;
        /// <summary>Legacy storage path retained only during migration.</summary>
        public string Url { get; set; }
        public int PhotoType { get; set; }
        public string? Description { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
