using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest<DeleteEmployeeResponse>
    {
        public int Id { get; set; }
    }
}

