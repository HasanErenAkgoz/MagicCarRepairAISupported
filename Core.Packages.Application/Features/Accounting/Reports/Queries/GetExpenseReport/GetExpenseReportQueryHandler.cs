using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetExpenseReport
{
    public class GetExpenseReportQueryHandler : IRequestHandler<GetExpenseReportQuery, GetExpenseReportResponse>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ITenantService _tenantService;

        public GetExpenseReportQueryHandler(
            IExpenseRepository expenseRepository,
            ITenantService tenantService)
        {
            _expenseRepository = expenseRepository;
            _tenantService = tenantService;
        }

        public async Task<GetExpenseReportResponse> Handle(GetExpenseReportQuery request, CancellationToken cancellationToken)
        {
            var query = _expenseRepository.Query()
                .Where(e => e.TransactionDate >= request.StartDate && e.TransactionDate <= request.EndDate);

            // Filtreler
            if (request.ExpenseType.HasValue)
            {
                query = query.Where(e => e.ExpenseType == request.ExpenseType.Value);
            }

            if (request.PaymentMethod.HasValue)
            {
                query = query.Where(e => e.PaymentMethod == request.PaymentMethod.Value);
            }

            var expenses = await query.ToListAsync(cancellationToken);

            var response = new GetExpenseReportResponse
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                TotalAmount = expenses.Sum(e => e.Amount),
                TotalCount = expenses.Count
            };

            // Gider türüne göre grupla
            response.AmountByType = expenses
                .GroupBy(e => e.ExpenseType)
                .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));

            // Ödeme yöntemine göre grupla
            response.AmountByPaymentMethod = expenses
                .GroupBy(e => e.PaymentMethod)
                .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));

            // Günlük özet
            response.DailySummaries = expenses
                .GroupBy(e => e.TransactionDate.Date)
                .Select(g => new DailyExpenseSummary
                {
                    Date = g.Key,
                    Amount = g.Sum(e => e.Amount),
                    Count = g.Count()
                })
                .OrderBy(d => d.Date)
                .ToList();

            return response;
        }
    }
}

