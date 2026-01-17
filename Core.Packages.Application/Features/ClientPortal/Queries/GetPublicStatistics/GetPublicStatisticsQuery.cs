using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicStatistics
{
    public class GetPublicStatisticsQuery : IRequest<IDataResult<GetPublicStatisticsResponse>>
    {
        public int ClientId { get; set; }
    }
}
