using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetWeeklyRevenue
{
    public class GetWeeklyRevenueQuery : IRequest<IDataResult<GetWeeklyRevenueResponse>>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
