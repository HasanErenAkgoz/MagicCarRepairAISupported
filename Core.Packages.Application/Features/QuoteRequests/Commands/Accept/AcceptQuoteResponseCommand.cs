using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.QuoteRequests.Commands.Accept
{
    public class AcceptQuoteResponseCommand : IRequest<IDataResult<AcceptQuoteResponseResponse>>
    {
        /// <summary>
        /// Teklif talebi ID
        /// </summary>
        public int QuoteRequestId { get; set; }

        /// <summary>
        /// Kabul edilecek teklif ID
        /// </summary>
        public int QuoteResponseId { get; set; }
    }
}
