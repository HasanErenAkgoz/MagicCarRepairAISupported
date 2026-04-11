using MagicCarRepairAISupported.Application.Common.TextEncoding;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.RepairUtf8Mojibake
{
    public sealed class RepairUtf8MojibakeCommandHandler : IRequestHandler<RepairUtf8MojibakeCommand, RepairUtf8MojibakeResponse>
    {
        private readonly IPartRepository _partRepository;
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly IPartStockRepository _partStockRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantService _tenantService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RepairUtf8MojibakeCommandHandler(
            IPartRepository partRepository,
            IStockMovementRepository stockMovementRepository,
            IPartStockRepository partStockRepository,
            IUnitOfWork unitOfWork,
            ITenantService tenantService,
            IHttpContextAccessor httpContextAccessor)
        {
            _partRepository = partRepository;
            _stockMovementRepository = stockMovementRepository;
            _partStockRepository = partStockRepository;
            _unitOfWork = unitOfWork;
            _tenantService = tenantService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<RepairUtf8MojibakeResponse> Handle(RepairUtf8MojibakeCommand request, CancellationToken cancellationToken)
        {
            if (request.ForClientId is > 0 && IsSystemAdmin())
                _tenantService.SetCurrentClientId(request.ForClientId.Value);

            //var clientId = _tenantService.GetCurrentClientId();
            //if (!clientId.HasValue || clientId.Value <= 0)
            //    throw new DomainException(errorCode: "CLIENT_ID_REQUIRED");

            var partsUpdated = 0;
            var movementsUpdated = 0;
            var stocksUpdated = 0;

            var parts = await _partRepository.Query().ToListAsync(cancellationToken);
            foreach (var p in parts)
            {
                if (RepairPartStrings(p))
                    partsUpdated++;
            }

            var movements = await _stockMovementRepository.Query().ToListAsync(cancellationToken);
            foreach (var m in movements)
            {
                if (RepairMovementStrings(m))
                    movementsUpdated++;
            }

            var stocks = await _partStockRepository.Query().ToListAsync(cancellationToken);
            foreach (var s in stocks)
            {
                if (RepairStockStrings(s))
                    stocksUpdated++;
            }

            if (partsUpdated > 0 || movementsUpdated > 0 || stocksUpdated > 0)
                await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new RepairUtf8MojibakeResponse
            {
                PartsUpdated = partsUpdated,
                StockMovementsUpdated = movementsUpdated,
                PartStocksUpdated = stocksUpdated
            };
        }

        private static bool RepairPartStrings(Part p)
        {
            var changed = false;
            changed |= ReplaceIfRepaired(v => p.PartCode = v, p.PartCode);
            changed |= ReplaceIfRepaired(v => p.Name = v, p.Name);
            changed |= ReplaceIfRepairedNullable(v => p.Description = v, p.Description);
            changed |= ReplaceIfRepairedNullable(v => p.Brand = v, p.Brand);
            changed |= ReplaceIfRepairedNullable(v => p.OEMNumber = v, p.OEMNumber);
            changed |= ReplaceIfRepairedNullable(v => p.Barcode = v, p.Barcode);
            changed |= ReplaceIfRepaired(v => p.Unit = v, p.Unit);
            changed |= ReplaceIfRepairedNullable(v => p.Notes = v, p.Notes);
            return changed;
        }

        private static bool RepairMovementStrings(StockMovement m)
        {
            var changed = false;
            changed |= ReplaceIfRepairedNullable(v => m.Description = v, m.Description);
            changed |= ReplaceIfRepairedNullable(v => m.ReferenceNumber = v, m.ReferenceNumber);
            changed |= ReplaceIfRepairedNullable(v => m.ReferenceType = v, m.ReferenceType);
            changed |= ReplaceIfRepairedNullable(v => m.TargetLocation = v, m.TargetLocation);
            return changed;
        }

        private static bool RepairStockStrings(PartStock s) =>
            ReplaceIfRepairedNullable(v => s.Location = v, s.Location);

        private static bool ReplaceIfRepaired(Action<string> set, string current)
        {
            var repaired = Utf8MojibakeHelper.Repair(current);
            if (repaired == null || string.Equals(current, repaired, StringComparison.Ordinal))
                return false;
            set(repaired);
            return true;
        }

        private static bool ReplaceIfRepairedNullable(Action<string?> set, string? current)
        {
            var repaired = Utf8MojibakeHelper.Repair(current);
            if (string.Equals(current, repaired, StringComparison.Ordinal))
                return false;
            set(repaired);
            return true;
        }

        private bool IsSystemAdmin()
        {
            var userType = _httpContextAccessor.HttpContext?.User?.FindFirst("UserType")?.Value;
            return userType == "1";
        }
    }
}
