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
            CreateMap<WorkOrder, GetWorkOrderByIdResponse>();
        }
    }
}

