using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetProfitLossReport
{
    public class GetProfitLossReportQueryHandler : IRequestHandler<GetProfitLossReportQuery, GetProfitLossReportResponse>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly ITenantService _tenantService;

        public GetProfitLossReportQueryHandler(
            IIncomeRepository incomeRepository,
            IExpenseRepository expenseRepository,
            ITenantService tenantService)
        {
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
            _tenantService = tenantService;
        }

        public async Task<GetProfitLossReportResponse> Handle(GetProfitLossReportQuery request, CancellationToken cancellationToken)
        {
            // Gelirleri getir
            var incomes = await _incomeRepository.Query()
                .Where(i => i.TransactionDate >= request.StartDate && i.TransactionDate <= request.EndDate)
                .ToListAsync(cancellationToken);

            // Giderleri getir
            var expenses = await _expenseRepository.Query()
                .Where(e => e.TransactionDate >= request.StartDate && e.TransactionDate <= request.EndDate)
                .ToListAsync(cancellationToken);

            var totalIncome = incomes.Sum(i => i.Amount);
            var totalExpense = expenses.Sum(e => e.Amount);
            var netProfit = totalIncome - totalExpense;
            var profitMargin = totalIncome > 0 ? (netProfit / totalIncome) * 100 : 0;

            var response = new GetProfitLossReportResponse
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                NetProfit = netProfit,
                ProfitMargin = profitMargin
            };

            // Aylık özet
            var allDates = incomes.Select(i => i.TransactionDate.Date)
                .Union(expenses.Select(e => e.TransactionDate.Date))
                .Distinct()
                .ToList();

            response.MonthlySummaries = allDates
                .GroupBy(d => new { d.Year, d.Month })
                .Select(g => new MonthlySummary
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    MonthName = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM yyyy"),
                    Income = incomes
                        .Where(i => i.TransactionDate.Year == g.Key.Year && i.TransactionDate.Month == g.Key.Month)
                        .Sum(i => i.Amount),
                    Expense = expenses
                        .Where(e => e.TransactionDate.Year == g.Key.Year && e.TransactionDate.Month == g.Key.Month)
                        .Sum(e => e.Amount)
                })
                .Select(m => new MonthlySummary
                {
                    Year = m.Year,
                    Month = m.Month,
                    MonthName = m.MonthName,
                    Income = m.Income,
                    Expense = m.Expense,
                    Profit = m.Income - m.Expense
                })
                .OrderBy(m => m.Year)
                .ThenBy(m => m.Month)
                .ToList();

            return response;
        }
    }
}

