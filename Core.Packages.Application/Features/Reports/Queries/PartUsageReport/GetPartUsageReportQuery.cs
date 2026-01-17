using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Reports.Queries.PartUsageReport
{
    public class GetPartUsageReportQuery : IRequest<GetPartUsageReportResponse>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? PartId { get; set; } // Belirli bir parça için
        public int? CategoryId { get; set; } // PartCategory (enum değeri)
        public int TopN { get; set; } = 10; // En çok kullanılan N parça
    }
}

