using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetIncomeReport
{
    public class GetIncomeReportQueryHandler : IRequestHandler<GetIncomeReportQuery, GetIncomeReportResponse>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly ITenantService _tenantService;

        public GetIncomeReportQueryHandler(
            IIncomeRepository incomeRepository,
            ITenantService tenantService)
        {
            _incomeRepository = incomeRepository;
            _tenantService = tenantService;
        }

        public async Task<GetIncomeReportResponse> Handle(GetIncomeReportQuery request, CancellationToken cancellationToken)
        {
            var query = _incomeRepository.Query()
                .Where(i => i.TransactionDate >= request.StartDate && i.TransactionDate <= request.EndDate);

            // Filtreler
            if (request.IncomeType.HasValue)
            {
                query = query.Where(i => i.IncomeType == request.IncomeType.Value);
            }

            if (request.PaymentMethod.HasValue)
            {
                query = query.Where(i => i.PaymentMethod == request.PaymentMethod.Value);
            }

            var incomes = await query.ToListAsync(cancellationToken);

            var response = new GetIncomeReportResponse
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                TotalAmount = incomes.Sum(i => i.Amount),
                TotalCount = incomes.Count
            };

            // Gelir türüne göre grupla
            response.AmountByType = incomes
                .GroupBy(i => i.IncomeType)
                .ToDictionary(g => g.Key, g => g.Sum(i => i.Amount));

            // Ödeme yöntemine göre grupla
            response.AmountByPaymentMethod = incomes
                .GroupBy(i => i.PaymentMethod)
                .ToDictionary(g => g.Key, g => g.Sum(i => i.Amount));

            // Günlük özet
            response.DailySummaries = incomes
                .GroupBy(i => i.TransactionDate.Date)
                .Select(g => new DailyIncomeSummary
                {
                    Date = g.Key,
                    Amount = g.Sum(i => i.Amount),
                    Count = g.Count()
                })
                .OrderBy(d => d.Date)
                .ToList();

            return response;
        }
    }
}

