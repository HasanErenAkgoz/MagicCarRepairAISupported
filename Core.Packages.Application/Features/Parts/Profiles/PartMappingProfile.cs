using AutoMapper;
using MagicCarRepairAISupported.Application.Features.Parts.Commands.CreatePart;
using MagicCarRepairAISupported.Application.Features.Parts.Commands.UpdatePart;
using MagicCarRepairAISupported.Application.Features.Parts.Queries.GetPartById;
using MagicCarRepairAISupported.Domain.Entities;

namespace MagicCarRepairAISupported.Application.Features.Parts.Profiles
{
    public class PartMappingProfile : Profile
    {
        public PartMappingProfile()
        {
            CreateMap<Part, CreatePartResponse>()
                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.Stock != null ? src.Stock.Quantity : (int?)null));

            CreateMap<Part, UpdatePartResponse>()
                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.Stock != null ? src.Stock.Quantity : (int?)null))
                .ForMember(dest => dest.StockLocation, opt => opt.MapFrom(src => src.Stock != null ? src.Stock.Location : null))
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier != null ? src.Supplier.CompanyName : null))
                .ForMember(dest => dest.IsLowStock, opt => opt.MapFrom(src => src.IsLowStock()));

            CreateMap<Part, GetPartByIdResponse>()
                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.Stock != null ? src.Stock.Quantity : (int?)null))
                .ForMember(dest => dest.StockLocation, opt => opt.MapFrom(src => src.Stock != null ? src.Stock.Location : null))
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier != null ? src.Supplier.CompanyName : null))
                .ForMember(dest => dest.IsLowStock, opt => opt.MapFrom(src => src.IsLowStock()));
        }
    }
}

