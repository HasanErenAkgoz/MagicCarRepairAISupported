using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Employees.Queries.GetAllEmployees
{
    public class GetAllEmployeesQuery : IRequest<List<GetAllEmployeesResponse>>
    {
        public EmploymentStatus? EmploymentStatus { get; set; } // Filtre: Aktif/Pasif
        public EmployeePosition? Position { get; set; } // Filtre: Pozisyona göre
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}

