using MagicCarRepairAISupported.Application.Common.Attributies;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetDashboardStats
{
    [Cache("dashboard:stats:ClientId:{ClientId}:StartDate:{StartDate}:EndDate:{EndDate}", 5, true)]
    public class GetDashboardStatsQuery : IRequest<GetDashboardStatsResponse>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

