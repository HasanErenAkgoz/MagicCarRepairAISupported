using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Clients.Commands.CreateClient
{
    public class CreateClientCommand : IRequest<IDataResult<CreateClientResponse>>
    {
        // Shop (Client) bilgileri
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }
        public DateTime? SubscriptionStartDate { get; set; }
        public DateTime? SubscriptionEndDate { get; set; }
        public string? LogoUrl { get; set; }
        public string? BannerUrl { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        // Yönetici (Manager) bilgileri — otomatik kullanıcı oluşturulur
        public string OwnerFirstName { get; set; } = string.Empty;
        public string OwnerLastName { get; set; } = string.Empty;
        public string OwnerEmail { get; set; } = string.Empty;
        public string OwnerPassword { get; set; } = string.Empty;
        public string? OwnerPhoneNumber { get; set; }
    }
}

