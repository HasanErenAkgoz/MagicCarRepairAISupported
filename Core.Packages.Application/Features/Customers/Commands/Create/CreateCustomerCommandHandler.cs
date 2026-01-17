using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Customers.Commands.Create
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(
            ICustomerRepository customerRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<CreateCustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // Business Rule: IdentityNo benzersiz olmalı (ClientId ile birlikte)
            var existingCustomer = await _customerRepository.GetByIdentityNoAsync(request.IdentityNo, cancellationToken);
            if (existingCustomer != null && existingCustomer.ClientId == clientId)
            {
                throw new DomainException("CUSTOMER_IDENTITY_NO_EXISTS", new { IdentityNo = request.IdentityNo });
            }

            // Business Rule: Email benzersiz olmalı (opsiyonel kontrol)
            if (!string.IsNullOrEmpty(request.Email))
            {
                var existingEmail = await _customerRepository.GetByEmailAsync(request.Email, cancellationToken);
                if (existingEmail != null && existingEmail.ClientId == clientId)
                {
                    throw new DomainException("CUSTOMER_EMAIL_EXISTS", new { Email = request.Email });
                }
            }

            // Entity oluştur
            var customer = new Customer
            {
                IdentityNo = request.IdentityNo,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                DateTimeOfBirth = request.DateTimeOfBirth,
                Language = request.Language,
                UserId = request.UserId,
                ClientId = clientId
            };

            // Kaydet
            await _customerRepository.AddAsync(customer, cancellationToken);
            await _customerRepository.SaveChangesAsync();

            // Response
            var response = _mapper.Map<CreateCustomerResponse>(customer);
            response.FullName = customer.FullName;
            return response;
        }
    }
}

