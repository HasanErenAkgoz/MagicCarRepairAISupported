using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.SalaryPayment.Commands.Delete
{
    public class DeleteSalaryPaymentCommand : IRequest<DeleteSalaryPaymentResponse>
    {
        public int Id { get; set; }
    }
}
