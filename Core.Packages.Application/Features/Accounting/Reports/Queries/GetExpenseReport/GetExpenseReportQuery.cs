using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetExpenseReport
{
    public class GetExpenseReportQuery : IRequest<GetExpenseReportResponse>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ExpenseType? ExpenseType { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
    }
}

