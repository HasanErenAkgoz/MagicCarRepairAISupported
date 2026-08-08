using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Clients.Commands.DeleteClient
{
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, IDataResult<DeleteClientResponse>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly UserManager<UserEntity> _userManager;
        private readonly ILogger<DeleteClientCommandHandler> _logger;

        public DeleteClientCommandHandler(
            IClientRepository clientRepository,
            UserManager<UserEntity> userManager,
            ILogger<DeleteClientCommandHandler> logger)
        {
            _clientRepository = clientRepository;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IDataResult<DeleteClientResponse>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(request.ClientId, cancellationToken);
            if (client == null)
                return new ErrorDataResult<DeleteClientResponse>("Servis yeri bulunamadı.");

            var clientName = client.Name;

            // Collect all users belonging to this client
            var users = await _userManager.Users
                .Where(u => u.ClientId == request.ClientId)
                .ToListAsync(cancellationToken);

            var userIds = users.Select(u => u.Id).ToList();

            // Clear Restrict FKs to AspNetUsers (chat, reminders, customer/employee portal links) before Identity delete
            await _clientRepository.PrepareForClientUserDeletionAsync(request.ClientId, userIds, cancellationToken);

            // Delete Identity users (removes UserRoles, UserClaims, cascaded UserSessions, etc.)
            foreach (var user in users)
            {
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                    _logger.LogWarning("Kullanıcı silinemedi. UserId: {UserId}, Hatalar: {Errors}",
                        user.Id, string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            // Cascade-delete all client data (runs in a single DB round-trip per table)
            await _clientRepository.DeleteCascadeAsync(request.ClientId, userIds, cancellationToken);

            return new SuccessDataResult<DeleteClientResponse>(
                new DeleteClientResponse
                {
                    ClientId = request.ClientId,
                    ClientName = clientName,
                    Message = $"'{clientName}' ve tüm alt verileri kalıcı olarak silindi."
                },
                $"'{clientName}' başarıyla silindi.");
        }
    }
}
