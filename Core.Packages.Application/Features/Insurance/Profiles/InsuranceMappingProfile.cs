using AutoMapper;
using MagicCarRepairAISupported.Application.Features.Insurance.Commands.CreateInsuranceCompany;
using MagicCarRepairAISupported.Application.Features.Insurance.Commands.CreateInsurancePolicy;
using MagicCarRepairAISupported.Application.Features.Insurance.Commands.CreateInsuranceClaim;
using MagicCarRepairAISupported.Application.Features.Insurance.Commands.UpdateInsuranceClaimStatus;
using MagicCarRepairAISupported.Application.Features.Insurance.Commands.UpdateInsuranceCompany;
using MagicCarRepairAISupported.Application.Features.Insurance.Commands.RenewInsurancePolicy;
using MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetAllInsuranceCompanies;
using MagicCarRepairAISupported.Domain.Entities;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Profiles
{
    public class InsuranceMappingProfile : Profile
    {
        public InsuranceMappingProfile()
        {
            CreateMap<InsuranceCompany, CreateInsuranceCompanyResponse>();
            CreateMap<InsuranceCompany, UpdateInsuranceCompanyResponse>();
            CreateMap<InsuranceCompany, InsuranceCompanyDto>();
            CreateMap<InsurancePolicy, CreateInsurancePolicyResponse>();
            CreateMap<InsurancePolicy, RenewInsurancePolicyResponse>();
            CreateMap<InsuranceClaim, CreateInsuranceClaimResponse>();
            CreateMap<InsuranceClaim, UpdateInsuranceClaimStatusResponse>();
        }
    }
}

