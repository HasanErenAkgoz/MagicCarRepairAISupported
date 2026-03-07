using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicShopList
{
    public class GetPublicShopListQuery : IRequest<IDataResult<List<PublicShopDto>>>
    {
        public string? SearchTerm { get; set; }
        public string? City { get; set; }
    }

    public class PublicShopDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }
        public string? LogoUrl { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? WorkingHours { get; set; }
        public string? Services { get; set; }
        public string? AboutUs { get; set; }
    }
}
