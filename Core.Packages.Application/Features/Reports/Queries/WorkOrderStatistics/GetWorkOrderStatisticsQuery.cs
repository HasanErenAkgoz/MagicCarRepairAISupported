using MagicCarRepairAISupported.Application.Common.Attributies;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Reports.Queries.WorkOrderStatistics
{
    [Cache("reports:workorder:stats:ClientId:{ClientId}:StartDate:{StartDate}:EndDate:{EndDate}:EmployeeId:{EmployeeId}:CompletedOnly:{IncludeCompletedOnly}", 10, true)]
    public class GetWorkOrderStatisticsQuery : IRequest<GetWorkOrderStatisticsResponse>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? EmployeeId { get; set; } // Belirli bir personel için
        public bool IncludeCompletedOnly { get; set; } = false; // Sadece tamamlanmış iş emirleri
    }
}

