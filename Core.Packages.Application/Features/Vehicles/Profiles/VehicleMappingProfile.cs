using AutoMapper;
using MagicCarRepairAISupported.Application.Features.Vehicles.Commands.Create;
using MagicCarRepairAISupported.Application.Features.Vehicles.Commands.Update;
using MagicCarRepairAISupported.Application.Features.Vehicles.Queries.GetAll;
using MagicCarRepairAISupported.Application.Features.Vehicles.Queries.GetById;
using MagicCarRepairAISupported.Application.Features.Vehicles.Queries.GetByCustomer;
using MagicCarRepairAISupported.Domain.Entities;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Profiles
{
    public class VehicleMappingProfile : Profile
    {
        public VehicleMappingProfile()
        {
            CreateMap<Vehicle, CreateVehicleResponse>();
            CreateMap<Vehicle, UpdateVehicleResponse>();
            CreateMap<Vehicle, GetAllVehiclesResponse>();
            CreateMap<Vehicle, GetVehiclesByCustomerResponse>();

            // VehiclePhoto → VehiclePhotoDto
            CreateMap<VehiclePhoto, VehiclePhotoDto>();

            // Vehicle → GetVehicleByIdResponse
            // Photos handler'da manuel map ediliyor, AutoMapper'ın ignore etmesi yeterli
            CreateMap<Vehicle, GetVehicleByIdResponse>()
                .ForMember(dest => dest.Photos, opt => opt.Ignore());
        }
    }
}

