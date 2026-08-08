using MagicCarRepairAISupported.Application.Common.Messages;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Employees.Commands.DeleteEmployeePermanently
{
    public class DeleteEmployeePermanentlyCommandHandler : IRequestHandler<DeleteEmployeePermanentlyCommand, DeleteEmployeePermanentlyResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteEmployeePermanentlyCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<DeleteEmployeePermanentlyResponse> Handle(DeleteEmployeePermanentlyCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.Id);
            if (employee == null)
            {
                throw new DomainException(Messages.NotFound, new { Entity = "Employee", Id = request.Id });
            }

            await _employeeRepository.DeleteByIdAsync(request.Id, cancellationToken);

            return new DeleteEmployeePermanentlyResponse
            {
                Success = true,
                Message = "Çalışan kalıcı olarak silindi."
            };
        }
    }
}

