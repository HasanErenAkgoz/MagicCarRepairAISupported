using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.SalaryPayment.Queries.GetAll
{
    public class GetAllSalaryPaymentsQuery : IRequest<GetAllSalaryPaymentsResponse>
    {
        public int? EmployeeId { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
