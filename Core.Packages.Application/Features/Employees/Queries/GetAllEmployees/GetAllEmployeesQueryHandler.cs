using AutoMapper;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Employees.Queries.GetAllEmployees
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, List<GetAllEmployeesResponse>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllEmployeesResponse>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            // Query oluştur
            var query = _employeeRepository.Query();

            // Filtreler
            if (request.EmploymentStatus.HasValue)
            {
                query = query.Where(e => e.EmploymentStatus == request.EmploymentStatus.Value);
            }

            if (request.Position.HasValue)
            {
                query = query.Where(e => e.Position == request.Position.Value);
            }

            // Pagination
            if (request.PageNumber.HasValue && request.PageSize.HasValue)
            {
                query = query.Skip((request.PageNumber.Value - 1) * request.PageSize.Value)
                             .Take(request.PageSize.Value);
            }

            // Çalıştır ve map et
            var employees = await query
                .OrderBy(e => e.Position)
                .ThenBy(e => e.FirstName)
                .ToListAsync(cancellationToken);

            var response = _mapper.Map<List<GetAllEmployeesResponse>>(employees);

            // Enum isimleri ekle
            foreach (var item in response)
            {
                item.PositionName = item.Position.ToString();
                item.EmploymentStatusName = item.EmploymentStatus.ToString();
            }

            return response;
        }
    }
}

