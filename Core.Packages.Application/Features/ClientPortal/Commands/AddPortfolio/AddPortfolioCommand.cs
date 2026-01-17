using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.AddPortfolio
{
    public class AddPortfolioCommand : IRequest<AddPortfolioResponse>
    {
        public int WorkOrderId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Categories { get; set; } // JSON formatında
        public List<int>? FeaturedPhotoIds { get; set; }
        public int DisplayOrder { get; set; } = 0;
        public bool RequestCustomerApproval { get; set; } = true;
    }
}
