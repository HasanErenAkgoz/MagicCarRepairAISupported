using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Customers.Queries.GetById
{
    public class GetCustomerByIdQuery : IRequest<GetCustomerByIdResponse>
    {
        public int Id { get; set; }
    }
}

