using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Customers.Commands.Create
{
    public class CreateCustomerCommand : IRequest<CreateCustomerResponse>
    {
        public string IdentityNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public DateTime DateTimeOfBirth { get; set; }
        public string Language { get; set; } = "tr";
        public int? UserId { get; set; } // Opsiyonel: Eğer bu müşterinin bir User hesabı varsa
    }
}

