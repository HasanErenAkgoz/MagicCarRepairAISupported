using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Income.Queries.GetAll
{
    public class GetAllIncomesQuery : IRequest<List<GetAllIncomesResponse>>
    {
        public IncomeType? IncomeType { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public int? WorkOrderId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}

