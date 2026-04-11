using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Messages;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using System.Text.Json;

namespace MagicCarRepairAISupported.Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, UpdateEmployeeResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<UpdateEmployeeResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            // Employee bul
            var employee = await _employeeRepository.GetByIdAsync(request.Id);
            if (employee == null)
            {
                throw new DomainException(Messages.NotFound, new { Entity = "Employee", Id = request.Id });
            }

            // Güncelle (sadece gönderilen alanları)
            if (request.FirstName != null) employee.FirstName = request.FirstName;
            if (request.LastName != null) employee.LastName = request.LastName;
            if (request.NationalId != null) employee.NationalId = request.NationalId;
            if (request.Phone != null) employee.Phone = request.Phone;
            if (request.Email != null) employee.Email = request.Email;
            if (request.Position.HasValue) employee.Position = request.Position.Value;
            if (request.Salary.HasValue) employee.Salary = request.Salary.Value;
            if (request.EmploymentStatus.HasValue) employee.EmploymentStatus = request.EmploymentStatus.Value;
            if (request.Address != null) employee.Address = request.Address;
            if (request.BloodType != null) employee.BloodType = request.BloodType;
            if (request.EmergencyContact != null) employee.EmergencyContact = request.EmergencyContact;
            if (request.EmergencyPhone != null) employee.EmergencyPhone = request.EmergencyPhone;
            if (request.Notes != null) employee.Notes = request.Notes;
            if (request.Biography != null) employee.Biography = request.Biography;
            if (request.ProfilePhotoUrl != null) employee.ProfilePhotoUrl = request.ProfilePhotoUrl;
            if (request.IsPublic.HasValue) employee.IsPublic = request.IsPublic.Value;
            if (request.DisplayOrder.HasValue) employee.DisplayOrder = request.DisplayOrder.Value;

            if (request.Specializations != null)
            {
                employee.Specializations = JsonSerializer.Serialize(request.Specializations);
            }

            // Kaydet
            _employeeRepository.Update(employee);
            await _employeeRepository.SaveChangesAsync();

            // Response
            var response = _mapper.Map<UpdateEmployeeResponse>(employee);
            return response;
        }
    }
}

