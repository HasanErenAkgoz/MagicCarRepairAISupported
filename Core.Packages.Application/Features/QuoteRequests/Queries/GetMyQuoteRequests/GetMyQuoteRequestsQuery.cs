using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.QuoteRequests.Queries.GetMyQuoteRequests
{
    public class GetMyQuoteRequestsQuery : IRequest<IDataResult<List<QuoteRequestDto>>>
    {
        public int CustomerId { get; set; }
    }

    public class QuoteRequestDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int? VehicleId { get; set; }
        public List<string> PhotoPaths { get; set; } = new List<string>();
        public string? Description { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal? EstimatedCost { get; set; }
        public string? EstimatedDescription { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
