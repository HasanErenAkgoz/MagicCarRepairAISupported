using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Customers.Commands.Delete
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, DeleteCustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITenantService _tenantService;

        public DeleteCustomerCommandHandler(
            ICustomerRepository customerRepository,
            ITenantService tenantService)
        {
            _customerRepository = customerRepository;
            _tenantService = tenantService;
        }

        public async Task<DeleteCustomerResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
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

            // Soft delete
            customer.Status = Status.Deleted;
            _customerRepository.Update(customer);
            await _customerRepository.SaveChangesAsync();

            return new DeleteCustomerResponse
            {
                Id = request.Id,
                Success = true,
                Message = "Customer deleted successfully"
            };
        }
    }
}

