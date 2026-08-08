using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetById
{
    public class GetWorkOrderByIdResponse
    {
        public int Id { get; set; }
        public string WorkOrderNumber { get; set; }
        public int VehicleId { get; set; }
        public VehicleDetailDto Vehicle { get; set; }
        public int CustomerId { get; set; }
        public CustomerDetailDto Customer { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public DateTime? ActualDeliveryDate { get; set; }
        public WorkOrderStatus Status { get; set; }
        public string StatusName { get; set; }
        public WorkOrderPriority Priority { get; set; }
        public string PriorityName { get; set; }
        public long? Kilometers { get; set; }
        public long? ExitKilometers { get; set; }
        public int? FuelLevel { get; set; }
        public string? CustomerComplaints { get; set; }
        public string? SpecialRequests { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal? EstimatedCost { get; set; }
        public decimal PaidAmount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string PaymentStatusName { get; set; }
        public int? AssignedEmployeeId { get; set; }
        public string? AssignedEmployeeName { get; set; }
        public string? Notes { get; set; }
        // Flat convenience properties
        public string? VehicleLicensePlate => Vehicle?.LicensePlate;
        public string? VehicleBrand => Vehicle?.Brand;
        public string? VehicleModel => Vehicle?.Model;
        public string? CustomerName => Customer?.FullName;
        public List<WorkOrderItemDto> Items { get; set; } = new();
        public List<WorkOrderLaborDto> Labors { get; set; } = new();
        public List<WorkOrderTimelineDto> Timeline { get; set; } = new();
        public List<WorkOrderPhotoDto> Photos { get; set; } = new();
    }

    public class CustomerDetailDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string? Avatar { get; set; }
    }

    public class VehicleDetailDto
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int? Year { get; set; }
        public string? Vin { get; set; }
        public string? ModelVariant { get; set; }
        public string? Trim { get; set; }
        public List<VehiclePhotoDto> Photos { get; set; } = new List<VehiclePhotoDto>();
    }

    public class VehiclePhotoDto
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        /// <summary>Tenant-authorized vehicle-photo endpoint.</summary>
        public string MediaUrl { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? PhotoType { get; set; }
        public DateTime UploadDate { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class WorkOrderItemDto
    {
        public int Id { get; set; }
        public WorkOrderItemType ItemType { get; set; }
        public string ItemTypeName { get; set; }
        public int? PartId { get; set; }
        public string? PartName { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public PartBrandType? BrandType { get; set; }
    }

    public class WorkOrderLaborDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string OperationName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal? DurationHours { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class WorkOrderTimelineDto
    {
        public int Id { get; set; }
        public DateTime EventDate { get; set; }
        public WorkOrderStatus? OldStatus { get; set; }
        public WorkOrderStatus? NewStatus { get; set; }
        public string StatusChangeText { get; set; }
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string Description { get; set; }
        public string EventType { get; set; }
    }

    public class WorkOrderPhotoDto
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string MediaUrl { get; set; } = string.Empty;
        public string? Description { get; set; }
        public WorkOrderPhotoType PhotoType { get; set; }
        public string PhotoTypeName { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
