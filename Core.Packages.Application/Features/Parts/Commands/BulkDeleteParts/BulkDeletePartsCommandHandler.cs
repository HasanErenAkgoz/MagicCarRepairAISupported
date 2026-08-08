using MagicCarRepairAISupported.Application.Common.Services.Cache;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.BulkDeleteParts
{
    public class BulkDeletePartsCommandHandler : IRequestHandler<BulkDeletePartsCommand, BulkDeletePartsResponse>
    {
        private readonly IPartRepository _partRepository;
        private readonly ICacheInvalidationService _cacheInvalidationService;

        public BulkDeletePartsCommandHandler(
            IPartRepository partRepository,
            ICacheInvalidationService cacheInvalidationService)
        {
            _partRepository = partRepository;
            _cacheInvalidationService = cacheInvalidationService;
        }

        public async Task<BulkDeletePartsResponse> Handle(BulkDeletePartsCommand request, CancellationToken cancellationToken)
        {
            var ids = (request.Ids ?? new List<int>())
                .Where(i => i > 0)
                .Distinct()
                .ToList();

            if (ids.Count == 0)
            {
                return new BulkDeletePartsResponse
                {
                    Success = false,
                    Message = "No ids provided",
                    RequestedCount = 0,
                };
            }

            var parts = await _partRepository.GetByIdsAsync(ids, cancellationToken);

            var foundIds = parts.Select(p => p.Id).ToHashSet();
            var missingIds = ids.Where(i => !foundIds.Contains(i)).ToList();

            var usedSet = (await _partRepository.GetUsedInActiveWorkOrdersPartIdsAsync(ids, cancellationToken)).ToHashSet();

            var deletedIds = new List<int>();
            var skipped = new List<BulkDeletePartSkippedItem>();

            foreach (var missing in missingIds)
            {
                skipped.Add(new BulkDeletePartSkippedItem { Id = missing, Reason = "PART_NOT_FOUND" });
            }

            foreach (var part in parts)
            {
                if (usedSet.Contains(part.Id))
                {
                    skipped.Add(new BulkDeletePartSkippedItem
                    {
                        Id = part.Id,
                        Reason = "PART_USED_IN_ACTIVE_WORKORDERS"
                    });
                    continue;
                }

                part.Status = Status.Deleted;
                deletedIds.Add(part.Id);
            }

            if (deletedIds.Count > 0)
            {
                foreach (var p in parts.Where(p => deletedIds.Contains(p.Id)))
                {
                    _partRepository.Update(p);
                }
                await _partRepository.SaveChangesAsync();

                // Cache invalidation: invalidate each part + dashboard once
                var invalidateTasks = deletedIds
                    .Select(id => _cacheInvalidationService.InvalidatePartCacheAsync(id))
                    .Append(_cacheInvalidationService.InvalidateDashboardCacheAsync());

                await Task.WhenAll(invalidateTasks);
            }

            return new BulkDeletePartsResponse
            {
                Success = true,
                Message = deletedIds.Count == ids.Count ? "Deleted" : "Partially deleted",
                RequestedCount = ids.Count,
                DeletedCount = deletedIds.Count,
                SkippedCount = skipped.Count,
                DeletedIds = deletedIds,
                Skipped = skipped
            };
        }
    }
}

