using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Customers.Queries.GetById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, GetCustomerByIdResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(
            ICustomerRepository customerRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<GetCustomerByIdResponse> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // Müşteriyi bul (Vehicle'ları da dahil et)
            var customer = await _customerRepository.Query()
                .Include(c => c.Vehicles)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (customer == null)
            {
                throw new DomainException("CUSTOMER_NOT_FOUND", new { Id = request.Id });
            }

            // ClientId kontrolü
            if (customer.ClientId != clientId)
            {
                throw new DomainException("CUSTOMER_NOT_BELONG_TO_CLIENT", new { CustomerId = request.Id });
            }

            // Response
            var response = _mapper.Map<GetCustomerByIdResponse>(customer);
            response.FullName = customer.FullName;
            response.Age = customer.GetAge();
            response.IsBirthdayToday = customer.IsBirthdayToday();
            response.VehicleCount = customer.Vehicles?.Count ?? 0;

            return response;
        }
    }
}

