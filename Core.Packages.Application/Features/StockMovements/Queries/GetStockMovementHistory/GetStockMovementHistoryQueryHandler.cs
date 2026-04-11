using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.StockMovements.Queries.GetStockMovementHistory
{
    public class GetStockMovementHistoryQueryHandler : IRequestHandler<GetStockMovementHistoryQuery, GetStockMovementHistoryResponse>
    {
        private readonly IStockMovementRepository _stockMovementRepository;

        public GetStockMovementHistoryQueryHandler(IStockMovementRepository stockMovementRepository)
        {
            _stockMovementRepository = stockMovementRepository;
        }

        public async Task<GetStockMovementHistoryResponse> Handle(GetStockMovementHistoryQuery request, CancellationToken cancellationToken)
        {
            List<StockMovement> movements;

            // Filtreleme
            if (request.PartId.HasValue)
            {
                movements = await _stockMovementRepository.GetByPartIdAsync(request.PartId.Value, cancellationToken);
            }
            else if (request.MovementType.HasValue)
            {
                movements = await _stockMovementRepository.GetByMovementTypeAsync(request.MovementType.Value, cancellationToken);
            }
            else if (request.EmployeeId.HasValue)
            {
                movements = await _stockMovementRepository.GetByEmployeeIdAsync(request.EmployeeId.Value, cancellationToken);
            }
            else if (request.StartDate.HasValue && request.EndDate.HasValue)
            {
                movements = await _stockMovementRepository.GetByDateRangeAsync(
                    request.StartDate.Value,
                    request.EndDate.Value,
                    cancellationToken);
            }
            else
            {
                // Tüm hareketler — Part / Employee Include zorunlu (GetListAsync navigation yüklemez)
                movements = await _stockMovementRepository.GetAllWithDetailsAsync(cancellationToken);
            }

            // Tarih aralığı filtreleme (ek filtre)
            if (request.StartDate.HasValue && !request.EndDate.HasValue)
            {
                movements = movements.Where(m => m.MovementDate >= request.StartDate.Value).ToList();
            }

            if (request.EndDate.HasValue && !request.StartDate.HasValue)
            {
                movements = movements.Where(m => m.MovementDate <= request.EndDate.Value.AddDays(1)).ToList(); // EndDate'i dahil et
            }
            else if (request.StartDate.HasValue && request.EndDate.HasValue && !request.PartId.HasValue)
            {
                // Zaten GetByDateRangeAsync ile filtrelenmiş, tekrar filtrelemeye gerek yok
            }

            // MovementType filtreleme (ek filtre - PartId filtrelemesi yoksa)
            if (request.MovementType.HasValue && !request.PartId.HasValue && !request.EmployeeId.HasValue)
            {
                movements = movements.Where(m => m.MovementType == request.MovementType.Value).ToList();
            }

            // EmployeeId filtreleme (ek filtre - PartId filtrelemesi yoksa)
            if (request.EmployeeId.HasValue && !request.PartId.HasValue)
            {
                movements = movements.Where(m => m.EmployeeId == request.EmployeeId.Value).ToList();
            }

            // Tarihe göre sırala
            movements = movements.OrderByDescending(m => m.MovementDate).ToList();

            // Toplam sayı (pagination öncesi)
            var totalCount = movements.Count;

            // Pagination
            var paginatedMovements = movements
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            // Response items
            var items = paginatedMovements.Select(m => new StockMovementHistoryItem
            {
                Id = m.Id,
                PartId = m.PartId,
                PartCode = m.Part?.PartCode,
                PartName = m.Part?.Name,
                MovementType = m.MovementType,
                Quantity = m.Quantity,
                MovementDate = m.MovementDate,
                EmployeeId = m.EmployeeId,
                EmployeeName = m.Employee != null ? $"{m.Employee.FirstName} {m.Employee.LastName}" : null,
                Description = m.Description,
                ReferenceNumber = m.ReferenceNumber,
                ReferenceType = m.ReferenceType,
                UnitPrice = m.UnitPrice,
                TotalPrice = m.TotalPrice
            }).ToList();

            return new GetStockMovementHistoryResponse
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
            };
        }
    }
}

