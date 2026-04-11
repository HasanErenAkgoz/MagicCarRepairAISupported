using MagicCarRepairAISupported.Application.Common.Services.Cache;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.DeletePart
{
    public class DeletePartCommandHandler : IRequestHandler<DeletePartCommand, DeletePartResponse>
    {
        private readonly IPartRepository _partRepository;
        private readonly ICacheInvalidationService _cacheInvalidationService;

        public DeletePartCommandHandler(
            IPartRepository partRepository,
            ICacheInvalidationService cacheInvalidationService)
        {
            _partRepository = partRepository;
            _cacheInvalidationService = cacheInvalidationService;
        }

        public async Task<DeletePartResponse> Handle(DeletePartCommand request, CancellationToken cancellationToken)
        {
            // Part'ı bul
            var part = await _partRepository.GetWithStockAsync(request.Id, cancellationToken);
            if (part == null)
            {
                throw new DomainException("PART_NOT_FOUND", new { PartId = request.Id });
            }

            // Business Rule: Aktif iş emirlerinde kullanılıyorsa silmeyi engelle
            var isUsedInActiveWorkOrders = await _partRepository.IsPartUsedInActiveWorkOrdersAsync(request.Id, cancellationToken);
            if (isUsedInActiveWorkOrders)
            {
                throw new DomainException("PART_USED_IN_ACTIVE_WORKORDERS", new { PartId = request.Id, PartCode = part.PartCode });
            }

            // Soft delete (status'u Deleted yap)
            part.Status = Status.Deleted;
            _partRepository.Update(part);
            await _partRepository.SaveChangesAsync();

            // Cache invalidation (parallel)
            await Task.WhenAll(
                _cacheInvalidationService.InvalidatePartCacheAsync(part.Id),
                _cacheInvalidationService.InvalidateDashboardCacheAsync()
            );

            return new DeletePartResponse
            {
                Success = true,
                Message = "Part deleted successfully"
            };
        }
    }
}

