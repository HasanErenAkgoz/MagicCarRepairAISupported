using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Queries.SuggestParts
{
    public class SuggestPartsQuery : IRequest<SuggestPartsResponse>
    {
        public int? WorkOrderId { get; set; }
        public int? VehicleId { get; set; }
        public string? CustomerComplaint { get; set; }
        public string? PartCategory { get; set; }
        public int NumberOfSuggestions { get; set; } = 10;
        public bool PreferInStock { get; set; } = true;
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
