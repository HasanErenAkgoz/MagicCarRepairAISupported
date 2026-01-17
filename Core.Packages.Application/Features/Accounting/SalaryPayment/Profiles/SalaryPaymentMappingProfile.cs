using AutoMapper;
using MagicCarRepairAISupported.Application.Features.Accounting.SalaryPayment.Commands.Create;
using MagicCarRepairAISupported.Application.Features.Accounting.SalaryPayment.Commands.Update;
using MagicCarRepairAISupported.Application.Features.Accounting.SalaryPayment.Queries.GetAll;
using MagicCarRepairAISupported.Application.Features.Accounting.SalaryPayment.Queries.GetById;
using SalaryPaymentEntity = MagicCarRepairAISupported.Domain.Entities.SalaryPayment;

namespace MagicCarRepairAISupported.Application.Features.Accounting.SalaryPayment.Profiles
{
    public class SalaryPaymentMappingProfile : Profile
    {
        public SalaryPaymentMappingProfile()
        {
            CreateMap<SalaryPaymentEntity, CreateSalaryPaymentResponse>();
            CreateMap<SalaryPaymentEntity, UpdateSalaryPaymentResponse>();
            CreateMap<SalaryPaymentEntity, GetAllSalaryPaymentsResponse>();
            CreateMap<SalaryPaymentEntity, GetSalaryPaymentByIdResponse>();
        }
    }
}
