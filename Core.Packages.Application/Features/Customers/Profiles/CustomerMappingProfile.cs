using AutoMapper;
using MagicCarRepairAISupported.Application.Features.Customers.Commands.Create;
using MagicCarRepairAISupported.Application.Features.Customers.Commands.Update;
using MagicCarRepairAISupported.Application.Features.Customers.Queries.GetAll;
using MagicCarRepairAISupported.Application.Features.Customers.Queries.GetById;
using MagicCarRepairAISupported.Domain.Entities;

namespace MagicCarRepairAISupported.Application.Features.Customers.Profiles
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Customer, CreateCustomerResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));
            
            CreateMap<Customer, UpdateCustomerResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));
            
            CreateMap<Customer, GetAllCustomersResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));
            
            CreateMap<Customer, GetCustomerByIdResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));
        }
    }
}

