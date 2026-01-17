using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQuery : IRequest<GetEmployeeByIdResponse>
    {
        public int Id { get; set; }
    }
}

