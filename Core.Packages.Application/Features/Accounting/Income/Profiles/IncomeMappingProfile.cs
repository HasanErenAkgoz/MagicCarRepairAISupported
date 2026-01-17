using AutoMapper;
using MagicCarRepairAISupported.Application.Features.Accounting.Income.Commands.Create;
using MagicCarRepairAISupported.Application.Features.Accounting.Income.Commands.Update;
using MagicCarRepairAISupported.Application.Features.Accounting.Income.Queries.GetAll;
using MagicCarRepairAISupported.Application.Features.Accounting.Income.Queries.GetById;
using IncomeEntity = MagicCarRepairAISupported.Domain.Entities.Income;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Income.Profiles
{
    public class IncomeMappingProfile : Profile
    {
        public IncomeMappingProfile()
        {
            CreateMap<IncomeEntity, CreateIncomeResponse>();
            CreateMap<IncomeEntity, UpdateIncomeResponse>();
            CreateMap<IncomeEntity, GetAllIncomesResponse>();
            CreateMap<IncomeEntity, GetIncomeByIdResponse>();
        }
    }
}

