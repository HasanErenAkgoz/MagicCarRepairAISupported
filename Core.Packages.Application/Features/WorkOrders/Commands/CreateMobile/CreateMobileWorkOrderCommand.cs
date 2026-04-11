using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.CreateMobile
{
    /// <summary>
    /// Mobil uygulama için iş emri oluşturma command'ı
    /// Doküman: WORK_ORDER_CREATE_REQUIREMENTS.md
    /// Müşteri ve araç inline olarak gönderilir
    /// </summary>
    public class CreateMobileWorkOrderCommand : IRequest<CreateMobileWorkOrderResponse>
    {
        // Customer — ID varsa direkt kullan, yoksa telefona göre bul/oluştur
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string? CustomerEmail { get; set; }

        // Vehicle — ID varsa direkt kullan, yoksa plakaya göre bul/oluştur
        public int? VehicleId { get; set; }
        public string VehiclePlate { get; set; } = string.Empty;
        public string VehicleBrand { get; set; } = string.Empty;
        public string VehicleModel { get; set; } = string.Empty;
        public int VehicleYear { get; set; }
        public string? VehicleFuelType { get; set; }
        public string? VehicleVin { get; set; }

        // Service
        public string ServiceTitle { get; set; } = string.Empty;
        public string? ServiceDescription { get; set; }
        public string? TechnicianNotes { get; set; }
        public int? TechnicianId { get; set; }
        public string Status { get; set; } = "pending";

        // Parts & Labor
        public List<WorkOrderPartRequest> Parts { get; set; } = new();
        public List<WorkOrderLaborRequest> Labor { get; set; } = new();
        public decimal TaxRate { get; set; }
    }

    public class WorkOrderPartRequest
    {
        public string Name { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class WorkOrderLaborRequest
    {
        public string Description { get; set; } = string.Empty;
        public decimal Hours { get; set; }
        public decimal HourlyRate { get; set; }
    }
}
