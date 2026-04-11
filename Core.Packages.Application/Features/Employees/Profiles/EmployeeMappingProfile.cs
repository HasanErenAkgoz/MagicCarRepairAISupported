using AutoMapper;
using MagicCarRepairAISupported.Application.Features.Employees.Commands.CreateEmployee;
using MagicCarRepairAISupported.Application.Features.Employees.Commands.UpdateEmployee;
using MagicCarRepairAISupported.Application.Features.Employees.Queries.GetAllEmployees;
using MagicCarRepairAISupported.Application.Features.Employees.Queries.GetEmployeeById;
using MagicCarRepairAISupported.Application.Features.Employees.Queries.GetEmployeesByPosition;
using MagicCarRepairAISupported.Domain.Entities;

namespace MagicCarRepairAISupported.Application.Features.Employees.Profiles
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<Employee, CreateEmployeeResponse>();
            CreateMap<Employee, UpdateEmployeeResponse>();
            CreateMap<Employee, GetAllEmployeesResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));
            CreateMap<Employee, GetEmployeeByIdResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                // Handler sets these manually after mapping (type mismatch or derived values)
                .ForMember(dest => dest.Specializations, opt => opt.Ignore())
                .ForMember(dest => dest.PositionName, opt => opt.Ignore())
                .ForMember(dest => dest.EmploymentStatusName, opt => opt.Ignore());
            CreateMap<Employee, GetEmployeesByPositionResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));
        }
    }
}

