using MagicCarRepairAISupported.Application.Common.Messages;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, DeleteEmployeeResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<DeleteEmployeeResponse> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            // Employee bul
            var employee = await _employeeRepository.GetByIdAsync(request.Id);
            if (employee == null)
            {
                throw new DomainException(Messages.NotFound, new { Entity = "Employee", Id = request.Id });
            }

            // Soft delete (status'u Terminated yap)
            employee.EmploymentStatus = EmploymentStatus.Terminated;
            employee.Status = Status.Deleted;
            _employeeRepository.Update(employee);
            await _employeeRepository.SaveChangesAsync();

            return new DeleteEmployeeResponse
            {
                Success = true,
                Message = Messages.Deleted
            };
        }
    }
}

