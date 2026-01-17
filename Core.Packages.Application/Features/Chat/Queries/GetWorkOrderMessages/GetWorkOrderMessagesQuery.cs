using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Chat.Queries.GetWorkOrderMessages
{
    public class GetWorkOrderMessagesQuery : IRequest<GetWorkOrderMessagesResponse>
    {
        public int WorkOrderId { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; } = 50;
    }
}
