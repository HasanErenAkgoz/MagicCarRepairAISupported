namespace MagicCarRepairAISupported.Application.Features.QuoteResponses.Commands.Submit
{
    public class SubmitQuoteResponseResponse
    {
        public int Id { get; set; }
        public string QuoteNumber { get; set; } = string.Empty;
        public int QuoteRequestId { get; set; }
        public decimal NetAmount { get; set; }
        public DateTime QuoteDate { get; set; }
        public DateTime ValidUntilDate { get; set; }
    }
}
