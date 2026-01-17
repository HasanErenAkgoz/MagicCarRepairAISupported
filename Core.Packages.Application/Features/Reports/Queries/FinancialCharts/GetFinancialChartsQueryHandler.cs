using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Reports.Queries.FinancialCharts
{
    public class GetFinancialChartsQueryHandler : IRequestHandler<GetFinancialChartsQuery, GetFinancialChartsResponse>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IEntityRepository<Payment, int> _paymentRepository;
        private readonly ITenantService _tenantService;

        public GetFinancialChartsQueryHandler(
            IIncomeRepository incomeRepository,
            IExpenseRepository expenseRepository,
            IInvoiceRepository invoiceRepository,
            IEntityRepository<Payment, int> paymentRepository,
            ITenantService tenantService)
        {
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
            _invoiceRepository = invoiceRepository;
            _paymentRepository = paymentRepository;
            _tenantService = tenantService;
        }

        public async Task<GetFinancialChartsResponse> Handle(GetFinancialChartsQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_NOT_FOUND");
            var startDate = request.StartDate ?? DateTime.UtcNow.AddMonths(-6);
            var endDate = request.EndDate ?? DateTime.UtcNow;

            // Gelir ve gider verilerini al
            var incomes = await _incomeRepository.Query()
                .Where(i => i.ClientId == clientId &&
                           i.TransactionDate >= startDate &&
                           i.TransactionDate <= endDate)
                .ToListAsync(cancellationToken);

            var expenses = await _expenseRepository.Query()
                .Where(e => e.ClientId == clientId &&
                           e.TransactionDate >= startDate &&
                           e.TransactionDate <= endDate)
                .ToListAsync(cancellationToken);

            // Fatura ve ödeme verilerini al
            var invoices = await _invoiceRepository.Query()
                .Where(inv => inv.ClientId == clientId &&
                            inv.InvoiceDate >= startDate &&
                            inv.InvoiceDate <= endDate)
                .ToListAsync(cancellationToken);

            var payments = await _paymentRepository.Query()
                .Where(p => p.ClientId == clientId &&
                           p.PaymentDate >= startDate &&
                           p.PaymentDate <= endDate)
                .ToListAsync(cancellationToken);

            // Gruplama fonksiyonu
            Func<DateTime, string> groupByFunc = request.GroupBy.ToLower() switch
            {
                "week" => (date) => $"{date.Year}-W{GetWeekOfYear(date)}",
                "day" => (date) => date.ToString("yyyy-MM-dd"),
                _ => (date) => $"{date.Year}-{date.Month:D2}" // month
            };

            // Gelir-gider karşılaştırması
            var incomeExpenseComparison = incomes
                .GroupBy(i => groupByFunc(i.TransactionDate))
                .Select(g => new IncomeExpenseItem
                {
                    Period = g.Key,
                    Income = g.Sum(i => i.Amount)
                })
                .Concat(expenses
                    .GroupBy(e => groupByFunc(e.TransactionDate))
                    .Select(g => new IncomeExpenseItem
                    {
                        Period = g.Key,
                        Expense = g.Sum(e => e.Amount)
                    }))
                .GroupBy(item => item.Period)
                .Select(g => new IncomeExpenseItem
                {
                    Period = g.Key,
                    Income = g.Sum(item => item.Income),
                    Expense = g.Sum(item => item.Expense),
                    Profit = g.Sum(item => item.Income) - g.Sum(item => item.Expense)
                })
                .OrderBy(item => item.Period)
                .ToList();

            // Aylık kar trendi
            var profitTrend = incomeExpenseComparison
                .Select(item => new ProfitTrendItem
                {
                    Period = item.Period,
                    Profit = item.Profit
                })
                .ToList();

            // Kategori bazlı harcama analizi
            var categoryExpenses = expenses
                .GroupBy(e => e.ExpenseType)
                .Select(g => new CategoryExpenseItem
                {
                    Category = g.Key,
                    CategoryName = g.Key.ToString(),
                    TotalAmount = g.Sum(e => e.Amount),
                    Count = g.Count()
                })
                .OrderByDescending(c => c.TotalAmount)
                .ToList();

            // Ödeme yöntemi dağılımı
            var paymentMethodDistribution = payments
                .GroupBy(p => p.PaymentMethod)
                .Select(g => new PaymentMethodDistributionItem
                {
                    PaymentMethod = g.Key,
                    PaymentMethodName = g.Key.ToString(),
                    Count = g.Count(),
                    TotalAmount = g.Sum(p => p.Amount),
                    Percentage = payments.Any() && payments.Sum(p => p.Amount) > 0 
                        ? (double)(g.Sum(p => p.Amount) / payments.Sum(p => p.Amount) * 100) 
                        : 0
                })
                .ToList();

            // Toplam istatistikler
            var totalIncome = incomes.Sum(i => i.Amount);
            var totalExpense = expenses.Sum(e => e.Amount);
            var totalProfit = totalIncome - totalExpense;
            var totalInvoiceAmount = invoices.Sum(inv => inv.TotalAmount);
            var totalPaidAmount = invoices.Sum(inv => inv.PaidAmount);
            var outstandingAmount = totalInvoiceAmount - totalPaidAmount;

            return new GetFinancialChartsResponse
            {
                StartDate = startDate,
                EndDate = endDate,
                IncomeExpenseComparison = incomeExpenseComparison,
                ProfitTrend = profitTrend,
                CategoryExpenses = categoryExpenses,
                PaymentMethodDistribution = paymentMethodDistribution,
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                TotalProfit = totalProfit,
                TotalInvoiceAmount = totalInvoiceAmount,
                TotalPaidAmount = totalPaidAmount,
                OutstandingAmount = outstandingAmount
            };
        }

        private int GetWeekOfYear(DateTime date)
        {
            var calendar = System.Globalization.CultureInfo.CurrentCulture.Calendar;
            return calendar.GetWeekOfYear(date, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
    }
}

