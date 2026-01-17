using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Stock;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.StockMovements.Commands.RecordStockMovement
{
    public class RecordStockMovementCommandHandler : IRequestHandler<RecordStockMovementCommand, RecordStockMovementResponse>
    {
        private readonly IPartRepository _partRepository;
        private readonly IPartStockRepository _partStockRepository;
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly IStockAlertService _stockAlertService;
        private readonly ITenantService _tenantService;

        public RecordStockMovementCommandHandler(
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

        public async Task<RecordStockMovementResponse> Handle(RecordStockMovementCommand request, CancellationToken cancellationToken)
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

            if (stock == null)
            {
                // İlk kez stok kaydı oluştur
                stock = new PartStock
                {
                    PartId = request.PartId,
                    Quantity = 0,
                    ClientId = clientId,
                    Status = Status.Active
                };
                await _partStockRepository.AddAsync(stock, cancellationToken);
                await _partStockRepository.SaveChangesAsync(); // Id almak için kaydet
            }

            // Stok hareketine göre miktarı güncelle
            if (request.MovementType == StockMovementType.In)
            {
                stock.AddQuantity(request.Quantity, request.EmployeeId);
            }
            else if (request.MovementType == StockMovementType.Out)
            {
                // Çıkış yapılırken stok kontrolü
                if (stock.Quantity < request.Quantity)
                {
                    throw new DomainException("INSUFFICIENT_STOCK", new
                    {
                        PartId = request.PartId,
                        PartCode = part.PartCode,
                        AvailableStock = stock.Quantity,
                        RequestedQuantity = request.Quantity
                    });
                }
                stock.SubtractQuantity(request.Quantity, request.EmployeeId);
            }
            else if (request.MovementType == StockMovementType.Adjustment)
            {
                // Sayım düzeltmesi - direkt miktar set edilir
                stock.UpdateQuantity(request.Quantity, request.EmployeeId);
            }
            // Transfer için şimdilik basit giriş/çıkış mantığı uygulanabilir

            if (!string.IsNullOrEmpty(request.Description) && request.Description.Contains("Location:"))
            {
                // Description'dan location çıkarılabilir, şimdilik basit bırakıyoruz
            }

            _partStockRepository.Update(stock);
            await _partStockRepository.SaveChangesAsync();

            // StockMovement kaydı oluştur
            var stockMovement = StockMovement.Create(
                partId: request.PartId,
                movementType: request.MovementType,
                quantity: request.Quantity,
                employeeId: request.EmployeeId,
                description: request.Description,
                referenceNumber: request.ReferenceNumber,
                referenceType: request.ReferenceType,
                unitPrice: request.UnitPrice,
                clientId: clientId
            );

            await _stockMovementRepository.AddAsync(stockMovement, cancellationToken);
            await _stockMovementRepository.SaveChangesAsync();

            // Minimum stock kontrolü ve alarm
            if (part.IsLowStockAlertEnabled)
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
            return new RecordStockMovementResponse
            {
                MovementId = stockMovement.Id,
                PartId = request.PartId,
                PartCode = part.PartCode,
                PartName = part.Name,
                MovementType = request.MovementType,
                Quantity = request.Quantity,
                PreviousStock = previousQuantity,
                CurrentStock = stock.Quantity,
                IsLowStock = part.IsLowStock()
            };
        }
    }
}

