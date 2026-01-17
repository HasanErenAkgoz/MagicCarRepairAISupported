namespace MagicCarRepairAISupported.Application.Common.Services.AI.Dtos
{
    /// <summary>
    /// Fiyat tahmini istek DTO'su
    /// </summary>
    public class PriceEstimationRequestDto
    {
        public int WorkOrderId { get; set; }
        public int? CustomerId { get; set; }
        public int? VehicleId { get; set; }
        public string? ProblemDescription { get; set; }
        public List<int>? PartIds { get; set; }
        public List<string>? LaborTypes { get; set; }
    }
}

