using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Commands.Update
{
    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, UpdateVehicleResponse>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public UpdateVehicleCommandHandler(
            IVehicleRepository vehicleRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<UpdateVehicleResponse> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // Aracı bul
            var vehicle = await _vehicleRepository.GetByIdAsync(request.Id);
            if (vehicle == null)
            {
                throw new DomainException("VEHICLE_NOT_FOUND", new { Id = request.Id });
            }

            // ClientId kontrolü
            if (vehicle.ClientId != clientId)
            {
                throw new DomainException("VEHICLE_NOT_BELONG_TO_CLIENT", new { VehicleId = request.Id });
            }

            // Güncelleme
            if (!string.IsNullOrEmpty(request.Brand))
                vehicle.Brand = request.Brand;
            if (!string.IsNullOrEmpty(request.Model))
                vehicle.Model = request.Model;
            if (request.Year.HasValue)
                vehicle.Year = request.Year.Value;
            if (!string.IsNullOrEmpty(request.Color))
                vehicle.Color = request.Color;
            if (!string.IsNullOrEmpty(request.LicensePlate))
                vehicle.LicensePlate = request.LicensePlate;
            if (request.Status.HasValue)
                vehicle.Status = request.Status.Value;
            if (request.VehicleType.HasValue)
                vehicle.VehicleType = request.VehicleType.Value;
            if (request.Kilometers.HasValue)
            {
                vehicle.UpdateKilometers(request.Kilometers.Value);
            }

            _vehicleRepository.Update(vehicle);
            await _vehicleRepository.SaveChangesAsync();

            // Response
            var response = _mapper.Map<UpdateVehicleResponse>(vehicle);
            response.StatusName = vehicle.Status.ToString();
            response.VehicleTypeName = vehicle.VehicleType.ToString();
            return response;
        }
    }
}

