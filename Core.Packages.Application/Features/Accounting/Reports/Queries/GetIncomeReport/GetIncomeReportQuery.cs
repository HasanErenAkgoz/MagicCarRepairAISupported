using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetIncomeReport
{
    public class GetIncomeReportQuery : IRequest<GetIncomeReportResponse>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IncomeType? IncomeType { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
    }
}

