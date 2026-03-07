using AutoMapper;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.Create;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.Update;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetAll;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetById;
using MagicCarRepairAISupported.Domain.Entities;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Profiles
{
    public class WorkOrderMappingProfile : Profile
    {
        public WorkOrderMappingProfile()
        {
            CreateMap<WorkOrder, CreateWorkOrderResponse>();
            CreateMap<WorkOrder, UpdateWorkOrderResponse>();
            CreateMap<WorkOrder, GetAllWorkOrdersResponse>();
            
            // GetWorkOrderByIdResponse mapping - nested properties are set manually in handler
            CreateMap<WorkOrder, GetWorkOrderByIdResponse>()
                .ForMember(dest => dest.Vehicle, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.Items, opt => opt.Ignore())
                .ForMember(dest => dest.Labors, opt => opt.Ignore())
                .ForMember(dest => dest.Timeline, opt => opt.Ignore())
                .ForMember(dest => dest.Photos, opt => opt.Ignore())
                .ForMember(dest => dest.StatusName, opt => opt.Ignore())
                .ForMember(dest => dest.PriorityName, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentStatusName, opt => opt.Ignore())
                .ForMember(dest => dest.AssignedEmployeeName, opt => opt.Ignore());
        }
    }
}

