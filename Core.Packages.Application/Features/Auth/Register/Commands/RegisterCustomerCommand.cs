using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Auth.Register.Commands
{
    public class RegisterCustomerCommand : IRequest<IDataResult<RegisterCustomerResponse>>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? IdentityNo { get; set; }
        public DateTime? DateTimeOfBirth { get; set; }
        public string Language { get; set; } = "tr";
        
        // Vehicle data (optional - from VehicleInfoScreen)
        public string? VehicleLicensePlate { get; set; }
        public string? VehicleBrand { get; set; }
        public string? VehicleModel { get; set; }
        public int? VehicleYear { get; set; }
        public string? VehicleColor { get; set; }
        public long? VehicleKilometers { get; set; }
        public string? VehicleFuelType { get; set; }
    }

    public class RegisterCustomerResponse
    {
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
