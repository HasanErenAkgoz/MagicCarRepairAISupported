using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Queries.GetMyVehicles
{
    public class GetMyVehiclesQueryHandler : IRequestHandler<GetMyVehiclesQuery, List<GetMyVehiclesResponse>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenantService _tenantService;

        public GetMyVehiclesQueryHandler(
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

        public async Task<List<GetMyVehiclesResponse>> Handle(GetMyVehiclesQuery request, CancellationToken cancellationToken)
        {
            // Get current user ID from HttpContext
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("User not authenticated");
            }

            var clientId = _tenantService.GetCurrentClientId();
            if (!clientId.HasValue)
            {
                throw new UnauthorizedAccessException("Client ID not found");
            }

            // Find customer by UserId
            var customers = await _customerRepository.GetListAsync(cancellationToken, c => c.UserId == userId && c.ClientId == clientId.Value);
            var currentCustomer = customers.FirstOrDefault();
            if (currentCustomer == null)
            {
                return new List<GetMyVehiclesResponse>();
            }

            // Get vehicles for this customer
            var vehicles = await _vehicleRepository.GetByCustomerIdAsync(currentCustomer.Id, cancellationToken);

            return vehicles.Select(v => new GetMyVehiclesResponse
            {
                Id = v.Id,
                Plate = v.LicensePlate,
                Brand = v.Brand,
                Model = v.Model,
                Year = v.Year,
                Color = v.Color,
                Vin = v.Vin,
                FuelType = v.FuelType,
                Kilometers = v.Kilometers,
                Status = v.Status.ToString()
            }).ToList();
        }
    }
}
