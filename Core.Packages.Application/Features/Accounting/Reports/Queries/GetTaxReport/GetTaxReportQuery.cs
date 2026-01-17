using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetTaxReport
{
    public class GetTaxReportQuery : IRequest<GetTaxReportResponse>
    {
        public int? Year { get; set; }
        public int? Month { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
