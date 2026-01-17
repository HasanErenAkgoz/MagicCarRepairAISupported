using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Messages;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using System.Text.Json;

namespace MagicCarRepairAISupported.Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, CreateEmployeeResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<CreateEmployeeResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            // Business Rule: EmployeeNo benzersiz olmalı
            var existingEmployee = await _employeeRepository.GetByEmployeeNoAsync(request.EmployeeNo, cancellationToken);
            if (existingEmployee != null)
            {
                throw new DomainException("EMPLOYEE_NO_EXISTS", new { EmployeeNo = request.EmployeeNo });
            }

            // Entity oluştur
            var employee = new Employee
            {
                EmployeeNo = request.EmployeeNo,
                FirstName = request.FirstName,
                LastName = request.LastName,
                NationalId = request.NationalId,
                Phone = request.Phone,
                Email = request.Email,
                Position = request.Position,
                Salary = request.Salary,
                HireDate = request.HireDate,
                EmploymentStatus = request.EmploymentStatus,
                Address = request.Address,
                BloodType = request.BloodType,
                EmergencyContact = request.EmergencyContact,
                EmergencyPhone = request.EmergencyPhone,
                Notes = request.Notes,
                UserId = request.UserId
            };

            // Specializations JSON'a çevir
            if (request.Specializations != null && request.Specializations.Any())
            {
                employee.Specializations = JsonSerializer.Serialize(request.Specializations);
            }

            // Kaydet
            await _employeeRepository.AddAsync(employee, cancellationToken);
            await _employeeRepository.SaveChangesAsync();

            // Response
            var response = _mapper.Map<CreateEmployeeResponse>(employee);
            return response;
        }
    }
}

