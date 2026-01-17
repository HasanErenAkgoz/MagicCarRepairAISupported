using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Queries.AnalyzeCustomers
{
    public class AnalyzeCustomersQuery : IRequest<AnalyzeCustomersResponse>
    {
        public int? CustomerId { get; set; }
        public int AnalysisPeriodDays { get; set; } = 365;
        public bool IncludeDetailedAnalysis { get; set; } = true;
        public bool IncludeRecommendations { get; set; } = true;
    }
}
