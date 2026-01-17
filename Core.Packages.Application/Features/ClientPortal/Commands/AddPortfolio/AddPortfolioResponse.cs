namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.AddPortfolio
{
    public class AddPortfolioResponse
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public string Title { get; set; }
        public bool RequiresCustomerApproval { get; set; }
    }
}
