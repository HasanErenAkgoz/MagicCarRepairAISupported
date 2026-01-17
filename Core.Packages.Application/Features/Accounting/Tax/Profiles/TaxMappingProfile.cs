using AutoMapper;
using MagicCarRepairAISupported.Application.Features.Accounting.Tax.Commands.Create;
using MagicCarRepairAISupported.Application.Features.Accounting.Tax.Commands.PayTax;
using MagicCarRepairAISupported.Application.Features.Accounting.Tax.Commands.Update;
using MagicCarRepairAISupported.Application.Features.Accounting.Tax.Queries.GetAll;
using MagicCarRepairAISupported.Application.Features.Accounting.Tax.Queries.GetById;
using TaxEntity = MagicCarRepairAISupported.Domain.Entities.Tax;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Tax.Profiles
{
    public class TaxMappingProfile : Profile
    {
        public TaxMappingProfile()
        {
            CreateMap<TaxEntity, CreateTaxResponse>();
            CreateMap<TaxEntity, UpdateTaxResponse>();
            CreateMap<TaxEntity, PayTaxResponse>();
            CreateMap<TaxEntity, GetTaxByIdResponse>();
        }
    }
}
