using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Accounting.SalaryPayment.Commands.Create
{
    public class CreateSalaryPaymentResponse
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal SocialSecurityDeduction { get; set; }
        public decimal UnemploymentInsuranceDeduction { get; set; }
        public decimal IncomeTaxDeduction { get; set; }
        public decimal StampTax { get; set; }
        public decimal OtherDeductions { get; set; }
        public decimal NetSalary { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string PaymentMethodName { get; set; }
        public string? Description { get; set; }
        public string? PaymentReferenceNumber { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
