using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetMonthlySummary
{
    public class GetMonthlySummaryQueryHandler : IRequestHandler<GetMonthlySummaryQuery, GetMonthlySummaryResponse>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly ISalaryPaymentRepository _salaryPaymentRepository;
        private readonly ITaxRepository _taxRepository;
        private readonly ITenantService _tenantService;

        public GetMonthlySummaryQueryHandler(
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

        public async Task<GetMonthlySummaryResponse> Handle(GetMonthlySummaryQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // Tarih aralığı
            var startDate = new DateTime(request.Year, request.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

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
            var salaryPayments = await _salaryPaymentRepository.GetByPeriodAsync(request.Year, request.Month, cancellationToken);
            var totalSalaryPayments = salaryPayments.Sum(s => s.NetSalary);

            // Vergi ödemeleri (bu ay ödenenler)
            var allTaxes = await _taxRepository.Query()
                .Where(t => t.ClientId == clientId && 
                           t.PaymentDate.HasValue &&
                           t.PaymentDate.Value.Year == request.Year &&
                           t.PaymentDate.Value.Month == request.Month)
                .ToListAsync(cancellationToken);
            var totalTaxPayments = allTaxes.Sum(t => t.Amount);

            return new GetMonthlySummaryResponse
            {
                Year = request.Year,
                Month = request.Month,
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                NetProfit = totalIncome - totalExpense,
                IncomeCount = incomes.Count,
                ExpenseCount = expenses.Count,
                TotalSalaryPayments = totalSalaryPayments,
                SalaryPaymentCount = salaryPayments.Count,
                TotalTaxPayments = totalTaxPayments,
                TaxPaymentCount = allTaxes.Count,
                Breakdown = new MonthlyBreakdown
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
