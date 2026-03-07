using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Sync.Commands.SyncData
{
    public class SyncDataCommand : IRequest<IDataResult<SyncDataResponse>>
    {
        public List<SyncChange> Changes { get; set; } = new();
    }

    public class SyncChange
    {
        public string Type { get; set; } = string.Empty; // create, update, delete
        public string EntityType { get; set; } = string.Empty;
        public int? EntityId { get; set; }
        public object Data { get; set; } = new();
        public string Timestamp { get; set; } = string.Empty;
    }

    public class SyncDataResponse
    {
        public int SyncedCount { get; set; }
        public int FailedCount { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}
