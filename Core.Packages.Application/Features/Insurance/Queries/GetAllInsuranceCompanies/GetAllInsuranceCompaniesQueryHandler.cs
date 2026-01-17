using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetAllInsuranceCompanies
{
    public class GetAllInsuranceCompaniesQueryHandler : IRequestHandler<GetAllInsuranceCompaniesQuery, GetAllInsuranceCompaniesResponse>
    {
        private readonly IInsuranceCompanyRepository _insuranceCompanyRepository;
        private readonly ITenantService _tenantService;

        public GetAllInsuranceCompaniesQueryHandler(
            IInsuranceCompanyRepository insuranceCompanyRepository,
            ITenantService tenantService)
        {
            _insuranceCompanyRepository = insuranceCompanyRepository;
            _tenantService = tenantService;
        }

        public async Task<GetAllInsuranceCompaniesResponse> Handle(GetAllInsuranceCompaniesQuery request, CancellationToken cancellationToken)
        {
            var query = _insuranceCompanyRepository.Query();

            if (request.IsActive.HasValue)
            {
                query = query.Where(c => c.IsActive == request.IsActive.Value);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var companies = await query
                .OrderBy(c => c.CompanyName)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(c => new InsuranceCompanyDto
                {
                    Id = c.Id,
                    CompanyName = c.CompanyName,
                    CompanyCode = c.CompanyCode,
                    ContactPerson = c.ContactPerson,
                    Phone = c.Phone,
                    Email = c.Email,
                    IsActive = c.IsActive
                })
                .ToListAsync(cancellationToken);

            return new GetAllInsuranceCompaniesResponse
            {
                Companies = companies,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}

