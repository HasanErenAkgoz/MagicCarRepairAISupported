namespace MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetAllInsuranceCompanies
{
    public class GetAllInsuranceCompaniesResponse
    {
        public List<InsuranceCompanyDto> Companies { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class InsuranceCompanyDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyCode { get; set; } = string.Empty;
        public string? ContactPerson { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
    }
}

