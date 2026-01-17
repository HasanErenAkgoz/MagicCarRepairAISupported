namespace MagicCarRepairAISupported.Application.Features.QuoteRequests.Commands.Reject
{
    public class RejectQuoteResponseResponse
    {
        public int QuoteRequestId { get; set; }
        public int QuoteResponseId { get; set; }
        public string Message { get; set; } = "Quote response rejected successfully";
    }
}

