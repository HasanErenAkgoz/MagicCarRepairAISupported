using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Import;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Customers.Commands.Import
{
    public class ImportCustomersCommandHandler : IRequestHandler<ImportCustomersCommand, ImportCustomersResponse>
    {
        private readonly IImportService _importService;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITenantService _tenantService;

        public ImportCustomersCommandHandler(
            IImportService importService,
            ICustomerRepository customerRepository,
            ITenantService tenantService)
        {
            _importService = importService;
            _customerRepository = customerRepository;
            _tenantService = tenantService;
        }

        public async Task<ImportCustomersResponse> Handle(ImportCustomersCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            // Import data
            ImportResult<CustomerImportDto> importResult;
            if (request.Format.ToLower() == "excel")
            {
                importResult = await _importService.ImportFromExcelAsync<CustomerImportDto>(request.FileData, cancellationToken);
            }
            else if (request.Format.ToLower() == "csv")
            {
                importResult = await _importService.ImportFromCsvAsync<CustomerImportDto>(request.FileData, cancellationToken);
            }
            else
            {
                throw new NotSupportedException($"Format {request.Format} is not supported");
            }

            // Create customers from imported data
            foreach (var dto in importResult.SuccessItems)
            {
                // Check if customer already exists (by IdentityNo)
                var existingCustomer = await _customerRepository.GetByIdentityNoAsync(dto.IdentityNo, cancellationToken);
                if (existingCustomer != null && existingCustomer.ClientId == clientId)
                {
                    // Update existing customer
                    existingCustomer.FirstName = dto.FirstName;
                    existingCustomer.LastName = dto.LastName;
                    existingCustomer.Email = dto.Email;
                    existingCustomer.PhoneNumber = dto.PhoneNumber;
                    existingCustomer.Address = dto.Address;
                    existingCustomer.DateTimeOfBirth = dto.DateTimeOfBirth;
                    existingCustomer.Language = dto.Language ?? "tr";

                    _customerRepository.Update(existingCustomer);
                }
                else
                {
                    // Create new customer
                    var customer = new Customer
                    {
                        IdentityNo = dto.IdentityNo,
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Email = dto.Email,
                        PhoneNumber = dto.PhoneNumber,
                        Address = dto.Address,
                        DateTimeOfBirth = dto.DateTimeOfBirth,
                        Language = dto.Language ?? "tr",
                        ClientId = clientId
                    };

                    await _customerRepository.AddAsync(customer, cancellationToken);
                }
            }

            await _customerRepository.SaveChangesAsync();

            return new ImportCustomersResponse
            {
                TotalRows = importResult.TotalRows,
                SuccessCount = importResult.SuccessCount,
                ErrorCount = importResult.ErrorCount,
                Errors = importResult.Errors
            };
        }

        // DTO for import
        public class CustomerImportDto
        {
            public string IdentityNo { get; set; } = string.Empty;
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
            public DateTime DateTimeOfBirth { get; set; }
            public string? Language { get; set; }
        }
    }
}
