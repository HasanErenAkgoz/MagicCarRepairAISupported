using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetYearlySummary
{
    public class GetYearlySummaryQueryHandler : IRequestHandler<GetYearlySummaryQuery, GetYearlySummaryResponse>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly ISalaryPaymentRepository _salaryPaymentRepository;
        private readonly ITaxRepository _taxRepository;
        private readonly ITenantService _tenantService;

        public GetYearlySummaryQueryHandler(
            IIncomeRepository incomeRepository,
            IExpenseRepository expenseRepository,
            ISalaryPaymentRepository salaryPaymentRepository,
            ITaxRepository taxRepository,
            ITenantService tenantService)
        {
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
            _salaryPaymentRepository = salaryPaymentRepository;
            _taxRepository = taxRepository;
            _tenantService = tenantService;
        }

        public async Task<GetYearlySummaryResponse> Handle(GetYearlySummaryQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // Tarih aralığı
            var startDate = new DateTime(request.Year, 1, 1);
            var endDate = new DateTime(request.Year, 12, 31, 23, 59, 59);

            // Gelirler
            var incomes = await _incomeRepository.GetByDateRangeAsync(startDate, endDate, cancellationToken);
            var totalIncome = incomes.Sum(i => i.Amount);
            var workOrderIncome = incomes.Where(i => i.IncomeType == IncomeType.WorkOrder).Sum(i => i.Amount);
            var otherIncome = incomes.Where(i => i.IncomeType != IncomeType.WorkOrder).Sum(i => i.Amount);

            // Giderler
            var expenses = await _expenseRepository.GetByDateRangeAsync(startDate, endDate, cancellationToken);
            var totalExpense = expenses.Sum(e => e.Amount);
            var salaryExpenses = expenses.Where(e => e.ExpenseType == ExpenseType.Salary).Sum(e => e.Amount);
            var taxExpenses = expenses.Where(e => e.ExpenseType == ExpenseType.Tax).Sum(e => e.Amount);
            var partExpenses = expenses.Where(e => e.ExpenseType == ExpenseType.PartPurchase).Sum(e => e.Amount);
            var otherExpenses = expenses.Where(e => e.ExpenseType != ExpenseType.Salary && 
                                                      e.ExpenseType != ExpenseType.Tax && 
                                                      e.ExpenseType != ExpenseType.PartPurchase).Sum(e => e.Amount);

            // Maaş ödemeleri
            var salaryPayments = await _salaryPaymentRepository.GetByPeriodAsync(request.Year, null, cancellationToken);
            var totalSalaryPayments = salaryPayments.Sum(s => s.NetSalary);

            // Vergi ödemeleri (bu yıl ödenenler)
            var allTaxes = await _taxRepository.Query()
                .Where(t => t.ClientId == clientId && 
                           t.PaymentDate.HasValue &&
                           t.PaymentDate.Value.Year == request.Year)
                .ToListAsync(cancellationToken);
            var totalTaxPayments = allTaxes.Sum(t => t.Amount);

            // Aylık veriler
            var monthlyData = new List<MonthlyData>();
            var monthNames = new[] { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", 
                                     "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };

            for (int month = 1; month <= 12; month++)
            {
                var monthStart = new DateTime(request.Year, month, 1);
                var monthEnd = monthStart.AddMonths(1).AddDays(-1);

                var monthIncomes = incomes.Where(i => i.TransactionDate >= monthStart && i.TransactionDate <= monthEnd).Sum(i => i.Amount);
                var monthExpenses = expenses.Where(e => e.TransactionDate >= monthStart && e.TransactionDate <= monthEnd).Sum(e => e.Amount);

                monthlyData.Add(new MonthlyData
                {
                    Month = month,
                    MonthName = monthNames[month - 1],
                    Income = monthIncomes,
                    Expense = monthExpenses,
                    Profit = monthIncomes - monthExpenses
                });
            }

            return new GetYearlySummaryResponse
            {
                Year = request.Year,
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                NetProfit = totalIncome - totalExpense,
                IncomeCount = incomes.Count,
                ExpenseCount = expenses.Count,
                TotalSalaryPayments = totalSalaryPayments,
                SalaryPaymentCount = salaryPayments.Count,
                TotalTaxPayments = totalTaxPayments,
                TaxPaymentCount = allTaxes.Count,
                MonthlyData = monthlyData,
                Breakdown = new YearlyBreakdown
                {
                    WorkOrderIncome = workOrderIncome,
                    OtherIncome = otherIncome,
                    SalaryExpenses = salaryExpenses,
                    TaxExpenses = taxExpenses,
                    PartExpenses = partExpenses,
                    OtherExpenses = otherExpenses
                }
            };
        }
    }
}
