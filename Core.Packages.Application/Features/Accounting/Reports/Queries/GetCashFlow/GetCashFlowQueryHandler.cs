using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetCashFlow
{
    public class GetCashFlowQueryHandler : IRequestHandler<GetCashFlowQuery, GetCashFlowResponse>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly ITenantService _tenantService;

        public GetCashFlowQueryHandler(
            IIncomeRepository incomeRepository,
            IExpenseRepository expenseRepository,
            ITenantService tenantService)
        {
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
            _tenantService = tenantService;
        }

        public async Task<GetCashFlowResponse> Handle(GetCashFlowQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            // Başlangıç bakiyesi (başlangıç tarihinden önceki tüm gelir - gider)
            var openingIncomes = await _incomeRepository.Query()
                .Where(i => i.ClientId == clientId && i.TransactionDate < request.StartDate)
                .SumAsync(i => (decimal?)i.Amount, cancellationToken) ?? 0;

            var openingExpenses = await _expenseRepository.Query()
                .Where(e => e.ClientId == clientId && e.TransactionDate < request.StartDate)
                .SumAsync(e => (decimal?)e.Amount, cancellationToken) ?? 0;

            var openingBalance = openingIncomes - openingExpenses;

            // Dönem içi gelirler
            var incomes = await _incomeRepository.GetByDateRangeAsync(request.StartDate, request.EndDate, cancellationToken);
            var totalInflow = incomes.Sum(i => i.Amount);

            // Dönem içi giderler
            var expenses = await _expenseRepository.GetByDateRangeAsync(request.StartDate, request.EndDate, cancellationToken);
            var totalOutflow = expenses.Sum(e => e.Amount);

            var netCashFlow = totalInflow - totalOutflow;
            var closingBalance = openingBalance + netCashFlow;

            // Periyodik veriler
            var periods = new List<CashFlowPeriod>();
            var groupByDays = request.GroupByDays ?? 1; // Varsayılan günlük

            var currentDate = request.StartDate.Date;
            var runningBalance = openingBalance;

            while (currentDate <= request.EndDate.Date)
            {
                var periodEnd = currentDate.AddDays(groupByDays - 1);
                if (periodEnd > request.EndDate.Date)
                {
                    periodEnd = request.EndDate.Date;
                }

                var periodIncomes = incomes
                    .Where(i => i.TransactionDate.Date >= currentDate && i.TransactionDate.Date <= periodEnd)
                    .Sum(i => i.Amount);

                var periodExpenses = expenses
                    .Where(e => e.TransactionDate.Date >= currentDate && e.TransactionDate.Date <= periodEnd)
                    .Sum(e => e.Amount);

                var periodNetFlow = periodIncomes - periodExpenses;
                runningBalance += periodNetFlow;

                string periodLabel;
                if (groupByDays == 1)
                {
                    periodLabel = currentDate.ToString("dd.MM.yyyy");
                }
                else if (groupByDays == 7)
                {
                    periodLabel = $"{currentDate:dd.MM} - {periodEnd:dd.MM.yyyy}";
                }
                else
                {
                    periodLabel = currentDate.ToString("MMMM yyyy");
                }

                periods.Add(new CashFlowPeriod
                {
                    PeriodStart = currentDate,
                    PeriodEnd = periodEnd,
                    PeriodLabel = periodLabel,
                    Inflow = periodIncomes,
                    Outflow = periodExpenses,
                    NetFlow = periodNetFlow,
                    RunningBalance = runningBalance
                });

                currentDate = periodEnd.AddDays(1);
            }

            return new GetCashFlowResponse
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                OpeningBalance = openingBalance,
                ClosingBalance = closingBalance,
                TotalInflow = totalInflow,
                TotalOutflow = totalOutflow,
                NetCashFlow = netCashFlow,
                Periods = periods
            };
        }
    }
}
