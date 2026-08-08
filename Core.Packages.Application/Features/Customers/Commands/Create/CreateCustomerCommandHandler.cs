using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Utils;
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
            var clientId = _tenantService.GetRequiredClientId();

            var identityNo = RequiredStringDefaults.ResolveIdentityNo(request.IdentityNo);
            var firstName = RequiredStringDefaults.Coalesce(request.FirstName);
            var lastName = RequiredStringDefaults.Coalesce(request.LastName);
            var email = RequiredStringDefaults.Coalesce(request.Email);
            var phoneNumber = RequiredStringDefaults.Coalesce(request.PhoneNumber);
            var address = RequiredStringDefaults.Coalesce(request.Address);
            var language = RequiredStringDefaults.Coalesce(request.Language, "tr");
            var dateOfBirth = request.DateTimeOfBirth == default
                ? new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                : request.DateTimeOfBirth;

            // Business Rule: IdentityNo benzersiz olmalı (ClientId ile birlikte)
            var existingCustomer = await _customerRepository.GetByIdentityNoAsync(identityNo, cancellationToken);
            if (existingCustomer != null && existingCustomer.ClientId == clientId)
            {
                throw new DomainException("CUSTOMER_IDENTITY_NO_EXISTS", new { IdentityNo = identityNo });
            }

            // Business Rule: Email benzersiz olmalı (opsiyonel kontrol)
            if (!string.IsNullOrEmpty(email))
            {
                var existingEmail = await _customerRepository.GetByEmailAsync(email, cancellationToken);
                if (existingEmail != null && existingEmail.ClientId == clientId)
                {
                    throw new DomainException("CUSTOMER_EMAIL_EXISTS", new { Email = email });
                }
            }

            // Entity oluştur
            var customer = new Customer
            {
                IdentityNo = identityNo,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                Address = address,
                DateTimeOfBirth = dateOfBirth,
                Language = language,
                IsVip = request.IsVip,
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

