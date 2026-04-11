using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.UpdateMobile
{
    /// <summary>
    /// Mobil uygulama için iş emri güncelleme command'ı
    /// Doküman: work-orders-api.md
    /// </summary>
    public class UpdateMobileWorkOrderCommand : IRequest<UpdateMobileWorkOrderResponse>
    {
        public string Id { get; set; } = string.Empty;
        public string ServiceTitle { get; set; } = string.Empty;
        public string? ServiceDescription { get; set; }
        public string? TechnicianNotes { get; set; }
        public int? TechnicianId { get; set; }
        public string Status { get; set; } = string.Empty; // pending, inProgress, completed, cancelled
        public List<PartDto> Parts { get; set; } = new();
        public List<LaborDto> Labor { get; set; } = new();
        public decimal TaxRate { get; set; }
    }

    public class PartDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class LaborDto
    {
        public string Description { get; set; } = string.Empty;
        public decimal Hours { get; set; }
        public decimal HourlyRate { get; set; }
    }
}
