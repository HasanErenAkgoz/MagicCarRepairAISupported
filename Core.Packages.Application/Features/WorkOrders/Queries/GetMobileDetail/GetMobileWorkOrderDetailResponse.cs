namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetMobileDetail
{
    /// <summary>
    /// Mobil uygulama için iş emri detay response'u
    /// Doküman: WORK_ORDER_REQUIREMENTS.md
    /// </summary>
    public class GetMobileWorkOrderDetailResponse
    {
        public string Id { get; set; } = string.Empty;
        public string OrderNo { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // pending, inProgress, completed, cancelled
        public string CreatedAt { get; set; } = string.Empty; // ISO 8601
        public string UpdatedAt { get; set; } = string.Empty; // ISO 8601

        public CustomerDto? Customer { get; set; }
        public VehicleDto? Vehicle { get; set; }

        public string ServiceTitle { get; set; } = string.Empty;
        public string ServiceDescription { get; set; } = string.Empty;
        public string? TechnicianNotes { get; set; }

        public TechnicianDto? Technician { get; set; }

        public List<PartDto> Parts { get; set; } = new();
        public List<LaborDto> Labor { get; set; } = new();

        public decimal PartsSubtotal { get; set; }
        public decimal LaborSubtotal { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Total { get; set; }

        public List<TimelineItemDto> Timeline { get; set; } = new();

        public List<WorkOrderPhotoDto> Photos { get; set; } = new();
    }

    public class WorkOrderPhotoDto
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public string MediaUrl { get; set; } = string.Empty;
        public int PhotoType { get; set; }
        public string PhotoTypeName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string UploadedAt { get; set; } = string.Empty;
    }

    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Email { get; set; }
    }

    public class VehicleDto
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int? Year { get; set; }
        public string Plate { get; set; } = string.Empty;
        public string? Vin { get; set; }
        public string? FuelType { get; set; }
        public string? Trim { get; set; }
        public List<string> Photos { get; set; } = new(); // URL listesi
    }

    public class TechnicianDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class PartDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }

    public class LaborDto
    {
        public string Id { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Hours { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal Total { get; set; }
    }

    public class TimelineItemDto
    {
        public string Id { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // created, assigned, inProgress, completed, cancelled
        public string Note { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty; // ISO 8601
        public string CreatedBy { get; set; } = string.Empty;
    }
}
