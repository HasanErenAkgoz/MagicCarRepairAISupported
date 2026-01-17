using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.SalaryPayment.Commands.Create
{
    public class CreateSalaryPaymentCommand : IRequest<CreateSalaryPaymentResponse>
    {
        public int EmployeeId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal SocialSecurityDeduction { get; set; }
        public decimal UnemploymentInsuranceDeduction { get; set; }
        public decimal IncomeTaxDeduction { get; set; }
        public decimal StampTax { get; set; }
        public decimal OtherDeductions { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public PaymentMethod PaymentMethod { get; set; }
        public string? Description { get; set; }
        public string? PaymentReferenceNumber { get; set; }
    }
}
