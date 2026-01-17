using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetById
{
    public class GetWorkOrderByIdQuery : IRequest<GetWorkOrderByIdResponse>
    {
        public int Id { get; set; }
    }
}

