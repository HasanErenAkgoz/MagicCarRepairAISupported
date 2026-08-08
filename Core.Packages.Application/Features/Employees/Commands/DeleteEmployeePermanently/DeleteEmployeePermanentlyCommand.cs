using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Employees.Commands.DeleteEmployeePermanently
{
    public class DeleteEmployeePermanentlyCommand : IRequest<DeleteEmployeePermanentlyResponse>
    {
        public int Id { get; set; }
    }
}

