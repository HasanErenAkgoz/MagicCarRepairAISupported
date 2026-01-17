using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Reports.Queries.CustomerAnalytics
{
    public class GetCustomerAnalyticsQuery : IRequest<GetCustomerAnalyticsResponse>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CustomerId { get; set; } // Belirli bir müşteri için
    }
}

