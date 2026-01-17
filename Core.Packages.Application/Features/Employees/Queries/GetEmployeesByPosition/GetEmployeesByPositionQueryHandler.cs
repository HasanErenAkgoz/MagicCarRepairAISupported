using AutoMapper;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using System.Text.Json;

namespace MagicCarRepairAISupported.Application.Features.Employees.Queries.GetEmployeesByPosition
{
    public class GetEmployeesByPositionQueryHandler : IRequestHandler<GetEmployeesByPositionQuery, List<GetEmployeesByPositionResponse>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeesByPositionQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<List<GetEmployeesByPositionResponse>> Handle(GetEmployeesByPositionQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetByPositionAsync(request.Position, cancellationToken);

            var response = _mapper.Map<List<GetEmployeesByPositionResponse>>(employees);

            // Specializations parse et
            foreach (var item in response)
            {
                var employee = employees.FirstOrDefault(e => e.Id == item.Id);
                if (employee != null && !string.IsNullOrEmpty(employee.Specializations))
                {
                    item.Specializations = JsonSerializer.Deserialize<List<string>>(employee.Specializations);
                }
            }

            return response;
        }
    }
}

