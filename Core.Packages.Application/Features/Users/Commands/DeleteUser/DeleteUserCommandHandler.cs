using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, IResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly ILogger<DeleteUserCommandHandler> _logger;

        public DeleteUserCommandHandler(
            UserManager<User> userManager,
            ICustomerRepository customerRepository,
            IEmployeeRepository employeeRepository,
            IChatMessageRepository chatMessageRepository,
            ILogger<DeleteUserCommandHandler> logger)
        {
            _userManager = userManager;
            _customerRepository = customerRepository;
            _employeeRepository = employeeRepository;
            _chatMessageRepository = chatMessageRepository;
            _logger = logger;
        }

        public async Task<IResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return new ErrorResult("Kullanıcı bulunamadı.");

            // Customers.UserId FK kısıtlamasını kaldır
            var customers = await _customerRepository.GetListAsync(
                cancellationToken,
                c => c.UserId == request.UserId);

            foreach (var customer in customers)
            {
                customer.UserId = null;
                _customerRepository.Update(customer);
            }

            if (customers.Any())
                await _customerRepository.SaveChangesAsync();

            // Employees.UserId FK kısıtlamasını kaldır
            var employees = await _employeeRepository.GetListAsync(
                cancellationToken,
                e => e.UserId == request.UserId);

            foreach (var employee in employees)
            {
                employee.UserId = null;
                _employeeRepository.Update(employee);
            }

            if (employees.Any())
                await _employeeRepository.SaveChangesAsync();

            // ChatMessages: SenderId / ReceiverId FK — kullanıcı silinmeden önce ilgili mesajlar kaldırılmalı
            await _chatMessageRepository.DeleteAllInvolvingUserAsync(request.UserId, cancellationToken);

            // Kullanıcıyı sil
            var deleteResult = await _userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
            {
                var errors = string.Join(", ", deleteResult.Errors.Select(e => e.Description));
                return new ErrorResult($"Silme işlemi başarısız: {errors}");
            }

            _logger.LogInformation(
                "User deleted: UserId={UserId}, Email={Email}",
                request.UserId, user.Email);

            return new SuccessResult("Kullanıcı başarıyla silindi.");
        }
    }
}
