using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Queries.AnalyzeEmployeePerformance
{
    public class AnalyzeEmployeePerformanceQuery : IRequest<AnalyzeEmployeePerformanceResponse>
    {
        public int? EmployeeId { get; set; }
        public int AnalysisPeriodDays { get; set; } = 90;
        public bool IncludeDetailedAnalysis { get; set; } = true;
        public bool IncludeRecommendations { get; set; } = true;
    }
}
