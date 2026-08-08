using MagicCarRepairAISupported.Application.Common.Models;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Customers.Queries.GetById
{
    public class GetCustomerByIdQuery : IRequest<QueryResult<GetCustomerByIdResponse>>
    {
        public int Id { get; set; }
    }
}

