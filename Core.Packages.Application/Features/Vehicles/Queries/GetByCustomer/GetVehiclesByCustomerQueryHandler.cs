using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Queries.GetByCustomer
{
    public class GetVehiclesByCustomerQueryHandler : IRequestHandler<GetVehiclesByCustomerQuery, List<GetVehiclesByCustomerResponse>>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IEntityRepository<Customer, int> _customerRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetVehiclesByCustomerQueryHandler(
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

        public async Task<List<GetVehiclesByCustomerResponse>> Handle(GetVehiclesByCustomerQuery request, CancellationToken cancellationToken)
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

            // Müşterinin araçlarını getir
            var vehicles = await _vehicleRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);

            var response = _mapper.Map<List<GetVehiclesByCustomerResponse>>(vehicles);

            // Enum isimleri ekle
            foreach (var item in response)
            {
                var vehicle = vehicles.First(v => v.Id == item.Id);
                item.StatusName = vehicle.Status.ToString();
            }

            return response;
        }
    }
}

