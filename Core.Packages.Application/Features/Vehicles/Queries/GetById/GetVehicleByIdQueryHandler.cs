using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Queries.GetById
{
    public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, GetVehicleByIdResponse>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetVehicleByIdQueryHandler(
            IVehicleRepository vehicleRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<GetVehicleByIdResponse> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // Aracı bul (Customer'ı da dahil et)
            var vehicle = await _vehicleRepository.Query()
                .Include(v => v.Customer)
                .FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken);

            if (vehicle == null)
            {
                throw new DomainException("VEHICLE_NOT_FOUND", new { Id = request.Id });
            }

            // ClientId kontrolü
            if (vehicle.ClientId != clientId)
            {
                throw new DomainException("VEHICLE_NOT_BELONG_TO_CLIENT", new { VehicleId = request.Id });
            }

            // Response
            var response = _mapper.Map<GetVehicleByIdResponse>(vehicle);
            response.StatusName = vehicle.Status.ToString();
            response.CustomerName = vehicle.Customer?.FullName ?? "Unknown";

            return response;
        }
    }
}

