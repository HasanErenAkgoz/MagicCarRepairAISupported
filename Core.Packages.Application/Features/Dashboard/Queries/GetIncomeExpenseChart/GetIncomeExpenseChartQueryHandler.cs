using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetIncomeExpenseChart
{
    public class GetIncomeExpenseChartQueryHandler : IRequestHandler<GetIncomeExpenseChartQuery, List<GetIncomeExpenseChartResponse>>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly ITenantService _tenantService;

        public GetIncomeExpenseChartQueryHandler(
            IIncomeRepository incomeRepository,
            IExpenseRepository expenseRepository,
            ITenantService tenantService)
        {
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
            _tenantService = tenantService;
        }

        public async Task<List<GetIncomeExpenseChartResponse>> Handle(GetIncomeExpenseChartQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            var incomes = await _incomeRepository.Query()
                .Where(i => i.ClientId == clientId && 
                           i.TransactionDate >= request.StartDate && 
                           i.TransactionDate <= request.EndDate)
                .ToListAsync(cancellationToken);

            var expenses = await _expenseRepository.Query()
                .Where(e => e.ClientId == clientId && 
                           e.TransactionDate >= request.StartDate && 
                           e.TransactionDate <= request.EndDate)
                .ToListAsync(cancellationToken);

            var response = new List<GetIncomeExpenseChartResponse>();

            // Tarih aralığını gruplara böl
            var periods = GetPeriods(request.StartDate, request.EndDate, request.GroupBy);

            foreach (var period in periods)
            {
                var periodIncomes = incomes
                    .Where(i => i.TransactionDate >= period.Start && i.TransactionDate < period.End)
                    .Sum(i => i.Amount);

                var periodExpenses = expenses
                    .Where(e => e.TransactionDate >= period.Start && e.TransactionDate < period.End)
                    .Sum(e => e.Amount);

                response.Add(new GetIncomeExpenseChartResponse
                {
                    Period = period.Label,
                    PeriodStart = period.Start,
                    PeriodEnd = period.End,
                    Income = periodIncomes,
                    Expense = periodExpenses,
                    Profit = periodIncomes - periodExpenses
                });
            }

            return response;
        }

        private List<(string Label, DateTime Start, DateTime End)> GetPeriods(DateTime startDate, DateTime endDate, ChartGroupBy groupBy)
        {
            var periods = new List<(string Label, DateTime Start, DateTime End)>();
            var current = startDate;

            while (current <= endDate)
            {
                DateTime periodStart = current;
                DateTime periodEnd;
                string label;

                switch (groupBy)
                {
                    case ChartGroupBy.Day:
                        periodEnd = periodStart.AddDays(1);
                        label = periodStart.ToString("yyyy-MM-dd");
                        break;
                    case ChartGroupBy.Week:
                        periodEnd = periodStart.AddDays(7);
                        var weekNum = GetWeekNumber(periodStart);
                        label = $"{periodStart.Year}-W{weekNum:D2}";
                        break;
                    case ChartGroupBy.Month:
                        periodEnd = periodStart.AddMonths(1);
                        label = periodStart.ToString("yyyy-MM");
                        break;
                    case ChartGroupBy.Year:
                        periodEnd = periodStart.AddYears(1);
                        label = periodStart.ToString("yyyy");
                        break;
                    default:
                        periodEnd = periodStart.AddMonths(1);
                        label = periodStart.ToString("yyyy-MM");
                        break;
                }

                periods.Add((label, periodStart, periodEnd));
                current = periodEnd;
            }

            return periods;
        }

        private int GetWeekNumber(DateTime date)
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture;
            var calendar = culture.Calendar;
            return calendar.GetWeekOfYear(date, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek);
        }
    }
}

