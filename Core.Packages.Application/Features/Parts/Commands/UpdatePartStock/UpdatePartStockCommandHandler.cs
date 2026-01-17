using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Stock;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.UpdatePartStock
{
    public class UpdatePartStockCommandHandler : IRequestHandler<UpdatePartStockCommand, UpdatePartStockResponse>
    {
        private readonly IPartRepository _partRepository;
        private readonly IPartStockRepository _partStockRepository;
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly IStockAlertService _stockAlertService;
        private readonly ITenantService _tenantService;

        public UpdatePartStockCommandHandler(
            IPartRepository partRepository,
            IPartStockRepository partStockRepository,
            IStockMovementRepository stockMovementRepository,
            IStockAlertService stockAlertService,
            ITenantService tenantService)
        {
            _partRepository = partRepository;
            _partStockRepository = partStockRepository;
            _stockMovementRepository = stockMovementRepository;
            _stockAlertService = stockAlertService;
            _tenantService = tenantService;
        }

        public async Task<UpdatePartStockResponse> Handle(UpdatePartStockCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_NOT_FOUND");

            // Part'ı bul
            var part = await _partRepository.GetWithStockAsync(request.PartId, cancellationToken);
            if (part == null)
            {
                throw new DomainException("PART_NOT_FOUND", new { PartId = request.PartId });
            }

            // PartStock'u bul veya oluştur
            var stock = await _partStockRepository.GetByPartIdAsync(request.PartId, cancellationToken);
            var previousQuantity = stock?.Quantity ?? 0;
            var isNewStock = stock == null;

            if (stock == null)
            {
                // İlk kez stok kaydı oluştur
                stock = new PartStock
                {
                    PartId = request.PartId,
                    Quantity = request.Quantity ?? 0,
                    Location = request.Location,
                    LastUpdatedByEmployeeId = request.EmployeeId,
                    LastUpdatedDate = DateTime.UtcNow,
                    ClientId = clientId,
                    Status = Status.Active
                };
                await _partStockRepository.AddAsync(stock, cancellationToken);
                await _partStockRepository.SaveChangesAsync(); // Id almak için kaydet
            }
            else
            {
                // Mevcut stok miktarını kaydet (StockMovement için)
                previousQuantity = stock.Quantity;
                var newQuantity = request.Quantity ?? previousQuantity;

                // Stok güncelle
                if (request.Quantity.HasValue && request.Quantity.Value != previousQuantity)
                {
                    stock.UpdateQuantity(request.Quantity.Value, request.EmployeeId);
                }
                else if (!request.Quantity.HasValue && request.EmployeeId.HasValue)
                {
                    // Sadece location güncelleniyorsa
                    stock.LastUpdatedDate = DateTime.UtcNow;
                    stock.LastUpdatedByEmployeeId = request.EmployeeId;
                }

                if (!string.IsNullOrEmpty(request.Location))
                {
                    stock.Location = request.Location;
                }

                _partStockRepository.Update(stock);
                await _partStockRepository.SaveChangesAsync();
            }

            // StockMovement kaydı oluştur (miktar değiştiyse)
            StockMovement? stockMovement = null;
            if (request.Quantity.HasValue)
            {
                var quantityDifference = request.Quantity.Value - previousQuantity;

                if (quantityDifference != 0 || isNewStock)
                {
                    var movementType = quantityDifference >= 0 ? StockMovementType.In : StockMovementType.Out;
                    var movementQuantity = isNewStock ? request.Quantity.Value : Math.Abs(quantityDifference);
                    
                    stockMovement = StockMovement.Create(
                        partId: request.PartId,
                        movementType: movementType,
                        quantity: movementQuantity,
                        employeeId: request.EmployeeId,
                        description: request.Description ?? (isNewStock ? "İlk stok girişi" : (quantityDifference > 0 ? "Manuel stok girişi" : "Manuel stok çıkışı")),
                        referenceType: "Manual",
                        clientId: clientId
                    );
                    await _stockMovementRepository.AddAsync(stockMovement, cancellationToken);
                    await _stockMovementRepository.SaveChangesAsync();
                }
            }

            // Stok güncellendikten sonra alarm kontrolü yap
            if (stock.Id > 0 && part.IsLowStockAlertEnabled)
            {
                try
                {
                    await _stockAlertService.CheckAndCreateAlertAsync(request.PartId, stock.Id, cancellationToken);
                }
                catch
                {
                    // Alarm kontrolü başarısız olsa bile devam et
                }
            }

            // Response
            return new UpdatePartStockResponse
            {
                PartId = request.PartId,
                StockId = stock.Id,
                Quantity = stock.Quantity,
                Location = stock.Location,
                IsLowStock = part.IsLowStock()
            };
        }
    }
}

