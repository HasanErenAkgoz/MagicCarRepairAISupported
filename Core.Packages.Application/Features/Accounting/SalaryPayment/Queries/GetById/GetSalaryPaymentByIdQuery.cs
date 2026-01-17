using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.SalaryPayment.Queries.GetById
{
    public class GetSalaryPaymentByIdQuery : IRequest<GetSalaryPaymentByIdResponse>
    {
        public int Id { get; set; }
    }
}
