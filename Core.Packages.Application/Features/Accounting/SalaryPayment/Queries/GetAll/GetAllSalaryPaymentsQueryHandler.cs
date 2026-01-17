using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Accounting.SalaryPayment.Queries.GetAll
{
    public class GetAllSalaryPaymentsQueryHandler : IRequestHandler<GetAllSalaryPaymentsQuery, GetAllSalaryPaymentsResponse>
    {
        private readonly ISalaryPaymentRepository _salaryPaymentRepository;
        private readonly ITenantService _tenantService;

        public GetAllSalaryPaymentsQueryHandler(
            ISalaryPaymentRepository salaryPaymentRepository,
            ITenantService tenantService)
        {
            _salaryPaymentRepository = salaryPaymentRepository;
            _tenantService = tenantService;
        }

        public async Task<GetAllSalaryPaymentsResponse> Handle(GetAllSalaryPaymentsQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            var query = _salaryPaymentRepository.Query()
                .Include(s => s.Employee)
                .Where(s => s.ClientId == clientId);

            // Filtreleme
            if (request.EmployeeId.HasValue)
            {
                query = query.Where(s => s.EmployeeId == request.EmployeeId.Value);
            }

            if (request.Year.HasValue)
            {
                query = query.Where(s => s.Year == request.Year.Value);
            }

            if (request.Month.HasValue)
            {
                query = query.Where(s => s.Month == request.Month.Value);
            }

            // Toplam sayı
            var totalCount = await query.CountAsync(cancellationToken);

            // Sayfalama
            var items = await query
                .OrderByDescending(s => s.Year)
                .ThenByDescending(s => s.Month)
                .ThenByDescending(s => s.PaymentDate)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(s => new SalaryPaymentItem
                {
                    Id = s.Id,
                    EmployeeId = s.EmployeeId,
                    EmployeeName = s.Employee.FullName,
                    Month = s.Month,
                    Year = s.Year,
                    GrossSalary = s.GrossSalary,
                    NetSalary = s.NetSalary,
                    PaymentDate = s.PaymentDate,
                    PaymentMethodName = s.PaymentMethod.ToString()
                })
                .ToListAsync(cancellationToken);

            return new GetAllSalaryPaymentsResponse
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
