using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Customers.Commands.Delete
{
    public class DeleteCustomerCommand : IRequest<DeleteCustomerResponse>
    {
        public int Id { get; set; }
    }
}

