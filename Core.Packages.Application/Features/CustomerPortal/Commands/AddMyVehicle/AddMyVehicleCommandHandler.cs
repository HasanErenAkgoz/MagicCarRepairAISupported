using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Commands.AddMyVehicle
{
    public class AddMyVehicleCommandHandler : IRequestHandler<AddMyVehicleCommand, AddMyVehicleResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenantService _tenantService;

        public AddMyVehicleCommandHandler(
            ICustomerRepository customerRepository,
            IVehicleRepository vehicleRepository,
            IHttpContextAccessor httpContextAccessor,
            ITenantService tenantService)
        {
            _customerRepository = customerRepository;
            _vehicleRepository = vehicleRepository;
            _httpContextAccessor = httpContextAccessor;
            _tenantService = tenantService;
        }

        public async Task<AddMyVehicleResponse> Handle(AddMyVehicleCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                throw new UnauthorizedAccessException("User not authenticated");

            var clientId = _tenantService.GetCurrentClientId();
            if (!clientId.HasValue)
                throw new UnauthorizedAccessException("Client ID not found");

            var currentCustomer = await _customerRepository.GetByUserIdForTenantAsync(
                userId, clientId.Value, cancellationToken);
            if (currentCustomer == null)
                throw new UnauthorizedAccessException("Customer not found");

            var plateExists = await _vehicleRepository.IsLicensePlateExistsAsync(request.Plate, cancellationToken);
            if (plateExists)
                throw new InvalidOperationException("A vehicle with this license plate already exists");

            var vehicle = new Vehicle
            {
                CustomerId = currentCustomer.Id,
                ClientId = clientId.Value,
                LicensePlate = request.Plate.ToUpper().Trim(),
                Brand = request.Brand,
                Model = request.Model,
                Year = request.Year,
                Color = request.Color,
                Vin = request.Vin,
                FuelType = request.FuelType
            };

            await _vehicleRepository.AddAsync(vehicle, cancellationToken);
            await _vehicleRepository.SaveChangesAsync();

            return new AddMyVehicleResponse
            {
                Id = vehicle.Id,
                Plate = vehicle.LicensePlate,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                Year = vehicle.Year,
                Color = vehicle.Color,
                Vin = vehicle.Vin,
                FuelType = vehicle.FuelType
            };
        }
    }
}
