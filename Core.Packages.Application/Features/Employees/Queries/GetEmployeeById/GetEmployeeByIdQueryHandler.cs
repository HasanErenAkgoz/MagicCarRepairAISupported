using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Messages;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using System.Text.Json;

namespace MagicCarRepairAISupported.Application.Features.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, GetEmployeeByIdResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<GetEmployeeByIdResponse> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.Id);
            if (employee == null)
            {
                throw new DomainException(Messages.NotFound, new { Entity = "Employee", Id = request.Id });
            }

            var response = _mapper.Map<GetEmployeeByIdResponse>(employee);

            // Enum isimleri
            response.PositionName = employee.Position.ToString();
            response.EmploymentStatusName = employee.EmploymentStatus.ToString();

            // Specializations JSON'dan parse et
            if (!string.IsNullOrEmpty(employee.Specializations))
            {
                response.Specializations = JsonSerializer.Deserialize<List<string>>(employee.Specializations);
            }

            return response;
        }
    }
}

