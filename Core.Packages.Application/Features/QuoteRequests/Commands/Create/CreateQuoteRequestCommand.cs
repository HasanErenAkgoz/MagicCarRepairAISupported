using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.QuoteRequests.Commands.Create
{
    public class CreateQuoteRequestCommand : IRequest<IDataResult<CreateQuoteRequestResponse>>
    {
        public int CustomerId { get; set; }
        public int? VehicleId { get; set; }
        public List<string> PhotoPaths { get; set; } = new List<string>();
        public string? Description { get; set; }
        public decimal? EstimatedCost { get; set; }
        public string? EstimatedDescription { get; set; }
        public List<int>? TargetClientIds { get; set; } // Hangi tamirhanelere gönderilecek (null ise tüm public tamirhanelere)
    }
}
