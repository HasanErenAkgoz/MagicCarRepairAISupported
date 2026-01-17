using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.QuoteRequests.Commands.Reject
{
    public class RejectQuoteResponseCommand : IRequest<IResult>
    {
        /// <summary>
        /// Teklif talebi ID
        /// </summary>
        public int QuoteRequestId { get; set; }

        /// <summary>
        /// Reddedilecek teklif ID
        /// </summary>
        public int QuoteResponseId { get; set; }

        /// <summary>
        /// Red nedeni (opsiyonel)
        /// </summary>
        public string? Reason { get; set; }
    }
}
