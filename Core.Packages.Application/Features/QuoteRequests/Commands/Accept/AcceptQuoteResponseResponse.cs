using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.QuoteRequests.Commands.Accept
{
    public class AcceptQuoteResponseResponse
    {
        public int QuoteRequestId { get; set; }
        public int QuoteResponseId { get; set; }
        public QuoteStatus Status { get; set; }
        public DateTime AcceptedDate { get; set; }
        public int? WorkOrderId { get; set; } // Teklif kabul edildiğinde oluşturulan WorkOrder ID
    }
}
