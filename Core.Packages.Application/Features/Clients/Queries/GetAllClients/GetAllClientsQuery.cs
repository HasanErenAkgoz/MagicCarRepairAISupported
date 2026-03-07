using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Clients.Queries.GetAllClients
{
    public class GetAllClientsQuery : IRequest<IDataResult<List<GetAllClientsResponse>>>
    {
        /// <summary>
        /// null = tümü, true = aktif, false = pasif (onay bekleyen)
        /// </summary>
        public bool? IsActive { get; set; }
    }

    public class GetAllClientsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
        public DateTime? SubscriptionStartDate { get; set; }
        public DateTime? SubscriptionEndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
