using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.PartSuppliers.Commands.Delete
{
    public class DeletePartSupplierCommandHandler : IRequestHandler<DeletePartSupplierCommand, DeletePartSupplierResponse>
    {
        private readonly IPartSupplierRepository _partSupplierRepository;

        public DeletePartSupplierCommandHandler(IPartSupplierRepository partSupplierRepository)
        {
            _partSupplierRepository = partSupplierRepository;
        }

        public async Task<DeletePartSupplierResponse> Handle(DeletePartSupplierCommand request, CancellationToken cancellationToken)
        {
            // PartSupplier'ı bul
            var supplier = await _partSupplierRepository.GetByIdAsync(request.Id);
            if (supplier == null)
            {
                throw new DomainException("SUPPLIER_NOT_FOUND", new { SupplierId = request.Id });
            }

            // Business Rule: İlişkili parçalar varsa silmeyi engelle (soft delete yapılacak)
            // Şimdilik sadece soft delete yapıyoruz
            // İleride Parts ile ilişki kontrolü eklenebilir

            // Soft delete (status'u Deleted yap)
            supplier.Status = Status.Deleted;
            supplier.IsActive = false;
            _partSupplierRepository.Update(supplier);
            await _partSupplierRepository.SaveChangesAsync();

            return new DeletePartSupplierResponse
            {
                Success = true,
                Message = "Supplier deleted successfully"
            };
        }
    }
}

