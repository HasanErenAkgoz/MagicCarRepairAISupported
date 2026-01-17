using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetAllInsuranceCompanies
{
    public class GetAllInsuranceCompaniesQuery : IRequest<GetAllInsuranceCompaniesResponse>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public bool? IsActive { get; set; }
    }
}

