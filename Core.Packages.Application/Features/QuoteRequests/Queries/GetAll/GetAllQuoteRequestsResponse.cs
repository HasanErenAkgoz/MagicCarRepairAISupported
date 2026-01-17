using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.QuoteRequests.Queries.GetAll
{
    public class GetAllQuoteRequestsResponse
    {
        public int Id { get; set; }
        public string RequestNumber { get; set; } = string.Empty;
        public string? CustomerName { get; set; }
        public string? VehicleInfo { get; set; }
        public string ProblemDescription { get; set; } = string.Empty;
        public QuoteRequestType RequestType { get; set; }
        public UrgencyLevel UrgencyLevel { get; set; }
        public QuoteStatus Status { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public DateTime QuoteDeadline { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int QuoteCount { get; set; }
    }
}

