using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Employees.Commands.DeleteAllEmployees
{
    public class DeleteAllEmployeesCommand : IRequest<IResult>
    {
        public int ClientId { get; set; }
    }
}
