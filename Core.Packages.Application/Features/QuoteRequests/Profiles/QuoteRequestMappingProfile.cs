using AutoMapper;
using MagicCarRepairAISupported.Application.Features.QuoteRequests.Commands.Create;
using MagicCarRepairAISupported.Application.Features.QuoteRequests.Queries.GetAll;
using MagicCarRepairAISupported.Application.Features.QuoteRequests.Queries.GetById;
using MagicCarRepairAISupported.Domain.Entities;

namespace MagicCarRepairAISupported.Application.Features.QuoteRequests.Profiles
{
    public class QuoteRequestMappingProfile : Profile
    {
        public QuoteRequestMappingProfile()
        {
            // Create
            CreateMap<QuoteRequest, CreateQuoteRequestResponse>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate ?? DateTime.UtcNow));

            // GetById
            CreateMap<QuoteRequest, GetQuoteRequestByIdResponse>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => 
                    src.Customer != null ? src.Customer.FullName : src.CustomerName))
                .ForMember(dest => dest.VehicleInfo, opt => opt.MapFrom(src => 
                    src.Vehicle != null 
                        ? $"{src.Vehicle.Brand} {src.Vehicle.Model} ({src.Vehicle.LicensePlate})"
                        : $"{src.VehicleBrand} {src.VehicleModel} ({src.VehicleLicensePlate})"))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.RequestTypeName, opt => opt.MapFrom(src => src.RequestType.ToString()))
                .ForMember(dest => dest.UrgencyLevelName, opt => opt.MapFrom(src => src.UrgencyLevel.ToString()));

            // GetAll
            CreateMap<QuoteRequest, GetAllQuoteRequestsResponse>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => 
                    src.Customer != null ? src.Customer.FullName : src.CustomerName))
                .ForMember(dest => dest.VehicleInfo, opt => opt.MapFrom(src => 
                    src.Vehicle != null 
                        ? $"{src.Vehicle.Brand} {src.Vehicle.Model} ({src.Vehicle.LicensePlate})"
                        : $"{src.VehicleBrand} {src.VehicleModel} ({src.VehicleLicensePlate})"))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.QuoteCount, opt => opt.MapFrom(src => src.QuoteResponses.Count));
        }
    }
}

