using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Clients.Queries.GetAllClients
{
    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, IDataResult<List<GetAllClientsResponse>>>
    {
        private readonly IClientRepository _clientRepository;

        public GetAllClientsQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IDataResult<List<GetAllClientsResponse>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _clientRepository.GetListAsync(
                cancellationToken,
                expression: request.IsActive.HasValue
                    ? c => c.IsActive == request.IsActive.Value
                    : null
            );

            var response = clients.Select(c => new GetAllClientsResponse
            {
                Id = c.Id,
                Name = c.Name,
                Code = c.Code ?? string.Empty,
                Description = c.Description,
                Email = c.ContactEmail,
                Phone = c.ContactPhone,
                Address = c.Address,
                IsActive = c.IsActive,
                SubscriptionStartDate = c.SubscriptionStartDate,
                SubscriptionEndDate = c.SubscriptionEndDate,
                CreatedDate = c.CreatedDate,
            }).ToList();

            return new SuccessDataResult<List<GetAllClientsResponse>>(response);
        }
    }
}
