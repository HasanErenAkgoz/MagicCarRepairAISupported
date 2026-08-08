using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Enums;
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
        public decimal? EstimatedCostMin { get; set; }
        public decimal? EstimatedCostMax { get; set; }
        public string? EstimatedDescription { get; set; }
        /// <summary>Hangi tamirhanelere gönderilecek (null ise tüm public tamirhanelere)</summary>
        public List<int>? TargetClientIds { get; set; }
        public QuoteRequestType RequestType { get; set; } = QuoteRequestType.Other;
        public UrgencyLevel UrgencyLevel { get; set; } = UrgencyLevel.Normal;
        /// <summary>
        /// AI teşhis ekranından oluşturuldu mu?
        /// true → QuoteDeadline = +1 gün, shop teklifleri de 1 gün geçerli.
        /// </summary>
        public bool IsAiGenerated { get; set; } = false;
    }
}
