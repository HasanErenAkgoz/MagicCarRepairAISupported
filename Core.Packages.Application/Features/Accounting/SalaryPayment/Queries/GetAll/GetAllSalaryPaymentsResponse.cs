namespace MagicCarRepairAISupported.Application.Features.Accounting.SalaryPayment.Queries.GetAll
{
    public class GetAllSalaryPaymentsResponse
    {
        public List<SalaryPaymentItem> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    }

    public class SalaryPaymentItem
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal NetSalary { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethodName { get; set; }
    }
}
