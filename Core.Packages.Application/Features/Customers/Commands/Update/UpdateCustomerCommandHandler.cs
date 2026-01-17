using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Customers.Commands.Update
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, UpdateCustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(
            ICustomerRepository customerRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<UpdateCustomerResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // Müşteriyi bul
            var customer = await _customerRepository.GetByIdAsync(request.Id);
            if (customer == null)
            {
                throw new DomainException("CUSTOMER_NOT_FOUND", new { Id = request.Id });
            }

            // ClientId kontrolü
            if (customer.ClientId != clientId)
            {
                throw new DomainException("CUSTOMER_NOT_BELONG_TO_CLIENT", new { CustomerId = request.Id });
            }

            // Email benzersizlik kontrolü (eğer değiştiriliyorsa)
            if (!string.IsNullOrEmpty(request.Email) && request.Email != customer.Email)
            {
                var existingEmail = await _customerRepository.GetByEmailAsync(request.Email, cancellationToken);
                if (existingEmail != null && existingEmail.Id != request.Id && existingEmail.ClientId == clientId)
                {
                    throw new DomainException("CUSTOMER_EMAIL_EXISTS", new { Email = request.Email });
                }
            }

            // Güncelleme
            if (!string.IsNullOrEmpty(request.FirstName))
                customer.FirstName = request.FirstName;
            if (!string.IsNullOrEmpty(request.LastName))
                customer.LastName = request.LastName;
            if (!string.IsNullOrEmpty(request.Email))
                customer.Email = request.Email;
            if (!string.IsNullOrEmpty(request.PhoneNumber))
                customer.PhoneNumber = request.PhoneNumber;
            if (request.Address != null)
                customer.Address = request.Address;
            if (request.DateTimeOfBirth.HasValue)
                customer.DateTimeOfBirth = request.DateTimeOfBirth.Value;
            if (!string.IsNullOrEmpty(request.Language))
                customer.Language = request.Language;
            if (request.UserId.HasValue)
                customer.UserId = request.UserId;

            _customerRepository.Update(customer);
            await _customerRepository.SaveChangesAsync();

            // Response
            var response = _mapper.Map<UpdateCustomerResponse>(customer);
            response.FullName = customer.FullName;
            return response;
        }
    }
}

