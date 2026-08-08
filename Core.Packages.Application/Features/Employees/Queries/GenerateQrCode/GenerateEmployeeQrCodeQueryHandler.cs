using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Invoice;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Employees.Queries.GenerateQrCode
{
    public class GenerateEmployeeQrCodeQueryHandler : IRequestHandler<GenerateEmployeeQrCodeQuery, byte[]>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITenantService _tenantService;
        private readonly IQrCodeService _qrCodeService;

        public GenerateEmployeeQrCodeQueryHandler(
            IEmployeeRepository employeeRepository,
            ITenantService tenantService,
            IQrCodeService qrCodeService)
        {
            _employeeRepository = employeeRepository;
            _tenantService = tenantService;
            _qrCodeService = qrCodeService;
        }

        public async Task<byte[]> Handle(GenerateEmployeeQrCodeQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
            if (employee == null || employee.ClientId != clientId)
            {
                throw new DomainException("EMPLOYEE_NOT_FOUND", new { EmployeeId = request.EmployeeId });
            }

            // QR Code içeriği: Employee bilgileri (kimlik kartı için)
            var qrContent = $"{{\"type\":\"Employee\",\"id\":{employee.Id},\"employeeNo\":\"{employee.EmployeeNo}\",\"name\":\"{employee.FullName}\",\"position\":\"{employee.Position}\"}}";

            return _qrCodeService.GenerateQrCode(qrContent, 300);
        }
    }
}
