using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Export;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Customers.Queries.Export
{
    public class ExportCustomersQueryHandler : IRequestHandler<ExportCustomersQuery, byte[]>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITenantService _tenantService;
        private readonly IExportService _exportService;

        public ExportCustomersQueryHandler(
            ICustomerRepository customerRepository,
            ITenantService tenantService,
            IExportService exportService)
        {
            _customerRepository = customerRepository;
            _tenantService = tenantService;
            _exportService = exportService;
        }

        public async Task<byte[]> Handle(ExportCustomersQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            var customers = await _customerRepository.Query()
                .Where(c => c.ClientId == clientId)
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName)
                .ToListAsync(cancellationToken);

            // DTO'ya dönüştür
            var exportData = customers.Select(c => new
            {
                IdentityNo = c.IdentityNo,
                FirstName = c.FirstName,
                LastName = c.LastName,
                FullName = c.FullName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Address = c.Address,
                DateOfBirth = c.DateTimeOfBirth.ToString("dd.MM.yyyy"),
                Age = c.GetAge().ToString(),
                Language = c.Language
            }).ToList();

            // Format'a göre export et
            return request.Format.ToLower() switch
            {
                "excel" => await _exportService.ExportToExcelAsync(exportData, "Customers", cancellationToken),
                "csv" => await _exportService.ExportToCsvAsync(exportData, cancellationToken),
                _ => throw new NotSupportedException($"Format {request.Format} is not supported")
            };
        }
    }
}
