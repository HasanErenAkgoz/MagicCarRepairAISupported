using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.UpdatePortfolio
{
    public class UpdatePortfolioCommand : IRequest<UpdatePortfolioResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Categories { get; set; }
        public List<int>? FeaturedPhotoIds { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsPublished { get; set; }
    }
}
