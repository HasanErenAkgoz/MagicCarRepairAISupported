using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Users.Commands.CreateUserByAdmin
{
    public class CreateUserByAdminCommand : IRequest<IResult>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 2 = Manager, 3 = Employee
        /// </summary>
        public int UserType { get; set; }

        public int ClientId { get; set; }
    }
}
