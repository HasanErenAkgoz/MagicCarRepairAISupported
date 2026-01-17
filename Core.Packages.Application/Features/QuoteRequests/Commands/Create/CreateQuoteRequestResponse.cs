using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.QuoteRequests.Commands.Create
{
    public class CreateQuoteRequestResponse
    {
        public int Id { get; set; }
        public string RequestNumber { get; set; } = string.Empty;
        public QuoteStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime QuoteDeadline { get; set; }
    }
}
