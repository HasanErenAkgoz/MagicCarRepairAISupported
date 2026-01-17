using AutoMapper;
using MagicCarRepairAISupported.Application.Features.PartSuppliers.Commands.Create;
using MagicCarRepairAISupported.Application.Features.PartSuppliers.Commands.Update;
using MagicCarRepairAISupported.Application.Features.PartSuppliers.Queries.GetAll;
using MagicCarRepairAISupported.Application.Features.PartSuppliers.Queries.GetById;
using MagicCarRepairAISupported.Domain.Entities;

namespace MagicCarRepairAISupported.Application.Features.PartSuppliers.Profiles
{
    public class PartSupplierMappingProfile : Profile
    {
        public PartSupplierMappingProfile()
        {
            CreateMap<PartSupplier, CreatePartSupplierResponse>();
            CreateMap<PartSupplier, UpdatePartSupplierResponse>();
            CreateMap<PartSupplier, GetPartSupplierByIdResponse>();
            CreateMap<PartSupplier, PartSupplierItem>();
        }
    }
}

