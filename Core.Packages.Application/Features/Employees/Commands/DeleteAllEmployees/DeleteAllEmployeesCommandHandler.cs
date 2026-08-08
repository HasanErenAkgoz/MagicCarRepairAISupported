using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Employees.Commands.DeleteAllEmployees
{
    public class DeleteAllEmployeesCommandHandler : IRequestHandler<DeleteAllEmployeesCommand, IResult>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteAllEmployeesCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IResult> Handle(DeleteAllEmployeesCommand request, CancellationToken cancellationToken)
        {
            await _employeeRepository.DeleteAllByClientIdAsync(request.ClientId, cancellationToken);
            return new SuccessResult("Tüm çalışanlar silindi.");
        }
    }
}
