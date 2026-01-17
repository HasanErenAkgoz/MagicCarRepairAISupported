using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Audit.Queries.GetDataChangeHistory
{
    public class GetDataChangeHistoryQuery : IRequest<GetDataChangeHistoryResponse>
    {
        public string EntityName { get; set; }
        public int EntityId { get; set; }
    }
}
