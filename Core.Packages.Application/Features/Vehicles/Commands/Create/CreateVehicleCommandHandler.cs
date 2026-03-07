using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Commands.Create
{
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, CreateVehicleResponse>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IEntityRepository<Customer, int> _customerRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public CreateVehicleCommandHandler(
            IVehicleRepository vehicleRepository,
            IEntityRepository<Customer, int> customerRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _customerRepository = customerRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<CreateVehicleResponse> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // Müşteri kontrolü
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
            if (customer == null)
            {
                throw new DomainException("CUSTOMER_NOT_FOUND", new { CustomerId = request.CustomerId });
            }

            if (customer.ClientId != clientId)
            {
                throw new DomainException("CUSTOMER_NOT_BELONG_TO_CLIENT", new { CustomerId = request.CustomerId });
            }

            // Business Rule: Plaka benzersiz olmalı (ClientId ile birlikte)
            var existingVehicle = await _vehicleRepository.GetByLicensePlateAsync(request.LicensePlate, cancellationToken);
            if (existingVehicle != null && existingVehicle.ClientId == clientId)
            {
                throw new DomainException("VEHICLE_LICENSE_PLATE_EXISTS", new { LicensePlate = request.LicensePlate });
            }

            // Entity oluştur
            var vehicle = new Vehicle
            {
                CustomerId = request.CustomerId,
                LicensePlate = request.LicensePlate,
                Brand = request.Brand,
                Model = request.Model,
                Year = request.Year,
                Color = request.Color,
                Status = request.Status,
                VehicleType = request.VehicleType,
                ClientId = clientId
            };

            // Km güncelle (eğer 0'dan büyükse)
            if (request.Kilometers > 0)
            {
                vehicle.UpdateKilometers(request.Kilometers);
            }

            // Kaydet
            await _vehicleRepository.AddAsync(vehicle, cancellationToken);
            await _vehicleRepository.SaveChangesAsync();

            // Response
            var response = _mapper.Map<CreateVehicleResponse>(vehicle);
            response.CustomerName = customer.FullName;
            response.StatusName = vehicle.Status.ToString();
            response.VehicleTypeName = vehicle.VehicleType.ToString();
            return response;
        }
    }
}

