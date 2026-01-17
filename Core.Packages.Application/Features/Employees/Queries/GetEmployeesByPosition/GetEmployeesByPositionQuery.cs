using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Employees.Queries.GetEmployeesByPosition
{
    public class GetEmployeesByPositionQuery : IRequest<List<GetEmployeesByPositionResponse>>
    {
        public EmployeePosition Position { get; set; }
    }
}

