using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Clients.Commands.UpdateClient
{
    public class UpdateClientCommand : IRequest<IDataResult<UpdateClientResponse>>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }
        public string? LogoUrl { get; set; }
        public string? BannerUrl { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime? SubscriptionStartDate { get; set; }
        public DateTime? SubscriptionEndDate { get; set; }
    }
}
