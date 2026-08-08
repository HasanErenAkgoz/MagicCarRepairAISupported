using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class PartRepository : EfEntityRepository<Part, BaseDbContext>, IPartRepository
    {
        private readonly ITenantService _tenantService;

        public PartRepository(
            BaseDbContext context,
            IUnitOfWork unitOfWork,
            ITenantService tenantService) : base(context, unitOfWork)
        {
            _tenantService = tenantService;
        }

        private IQueryable<Part> TenantPartsQuery()
        {
            var query = Context.Set<Part>()
                .IgnoreQueryFilters()
                .Where(p => p.Status != Status.Deleted);

            var clientId = _tenantService.GetCurrentClientId();
            if (clientId.HasValue)
                query = query.Where(p => p.ClientId == clientId.Value);

            return query;
        }

        public async Task<Part?> GetByIdAsync(int id)
        {
            return await TenantPartsQuery()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Part?> GetByPartCodeAsync(string partCode, CancellationToken cancellationToken = default)
        {
            return await TenantPartsQuery()
                .Include(p => p.Stock)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.PartCode == partCode, cancellationToken);
        }

        public async Task<List<Part>> GetByCategoryAsync(PartCategory category, CancellationToken cancellationToken = default)
        {
            return await TenantPartsQuery()
                .Include(p => p.Stock)
                .Where(p => p.Category == category)
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Part>> GetLowStockPartsAsync(CancellationToken cancellationToken = default)
        {
            return await TenantPartsQuery()
                .Include(p => p.Stock)
                .Where(p => p.IsLowStockAlertEnabled &&
                           p.Stock != null &&
                           p.Stock.Quantity <= p.MinimumStockLevel)
                .OrderBy(p => p.Stock!.Quantity)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Part>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
        {
            var term = searchTerm.ToLower();
            return await TenantPartsQuery()
                .Include(p => p.Stock)
                .Include(p => p.Supplier)
                .Where(p => p.Name.ToLower().Contains(term) ||
                           p.PartCode.ToLower().Contains(term) ||
                           (p.Description != null && p.Description.ToLower().Contains(term)) ||
                           (p.Barcode != null && p.Barcode.ToLower().Contains(term)) ||
                           (p.OEMNumber != null && p.OEMNumber.ToLower().Contains(term)))
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsPartCodeExistsAsync(string partCode, CancellationToken cancellationToken = default)
        {
            return await TenantPartsQuery()
                .AnyAsync(p => p.PartCode == partCode, cancellationToken);
        }

        public async Task<Part?> GetWithStockAsync(int id, CancellationToken cancellationToken = default)
        {
            return await TenantPartsQuery()
                .Include(p => p.Stock)
                .Include(p => p.Supplier)
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<List<Part>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
        {
            var idList = ids?.Where(i => i > 0).Distinct().ToList() ?? new();
            if (idList.Count == 0) return new List<Part>();

            return await TenantPartsQuery()
                .Where(p => idList.Contains(p.Id))
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsPartUsedInActiveWorkOrdersAsync(int partId, CancellationToken cancellationToken = default)
        {
            var activeStatuses = GetActiveWorkOrderStatuses();

            return await Context.Set<WorkOrderItem>()
                .Include(woi => woi.WorkOrder)
                .AnyAsync(woi => woi.PartId == partId &&
                                woi.Status != Status.Deleted &&
                                activeStatuses.Contains(woi.WorkOrder.Status),
                         cancellationToken);
        }

        public async Task<List<int>> GetUsedInActiveWorkOrdersPartIdsAsync(IEnumerable<int> partIds, CancellationToken cancellationToken = default)
        {
            var idList = partIds?.Where(i => i > 0).Distinct().ToList() ?? new();
            if (idList.Count == 0) return new List<int>();

            var activeStatuses = GetActiveWorkOrderStatuses();

            return await Context.Set<WorkOrderItem>()
                .Include(woi => woi.WorkOrder)
                .Where(woi => woi.PartId != null &&
                              idList.Contains(woi.PartId.Value) &&
                              woi.Status != Status.Deleted &&
                              activeStatuses.Contains(woi.WorkOrder.Status))
                .Select(woi => woi.PartId!.Value)
                .Distinct()
                .ToListAsync(cancellationToken);
        }

        public async Task<List<CrossTenantPartMatch>> SearchCrossTenantAsync(
            string vehicleBrand,
            string vehicleModel,
            int vehicleYear,
            IEnumerable<string> partNames,
            CancellationToken cancellationToken = default)
        {
            var brandLower = vehicleBrand.ToLower();
            var modelLower = vehicleModel.ToLower();
            var nameList = partNames.Select(n => n.ToLower()).ToList();

            var allParts = await Context.Set<Part>()
                .IgnoreQueryFilters()
                .Include(p => p.Stock)
                .Include(p => p.Client)
                .Where(p => p.Status != Status.Deleted &&
                            p.Stock != null && p.Stock.Quantity > 0 &&
                            p.Client.IsActive && p.Client.IsPublicProfileEnabled &&
                            nameList.Any(n => p.Name.ToLower().Contains(n)))
                .ToListAsync(cancellationToken);

            var results = new List<CrossTenantPartMatch>();

            foreach (var part in allParts)
            {
                foreach (var searchedName in nameList)
                {
                    var score = ComputeMatchScore(part, brandLower, modelLower, vehicleYear, searchedName);
                    if (score == null) continue;

                    results.Add(new CrossTenantPartMatch(
                        PartId: part.Id,
                        PartName: part.Name,
                        PartCode: part.PartCode,
                        OemNumber: part.OEMNumber,
                        SalePrice: part.SalePrice,
                        StockQuantity: part.Stock!.Quantity,
                        ClientId: part.ClientId,
                        ShopName: part.Client.Name,
                        ShopLogoUrl: part.Client.LogoUrl,
                        ShopLatitude: part.Client.Latitude,
                        ShopLongitude: part.Client.Longitude,
                        MatchScore: score.Value,
                        SearchedPartName: searchedName
                    ));
                }
            }

            return results;
        }

        private static List<WorkOrderStatus> GetActiveWorkOrderStatuses() => new()
        {
            WorkOrderStatus.AppointmentScheduled,
            WorkOrderStatus.VehicleEntered,
            WorkOrderStatus.DiagnosisCompleted,
            WorkOrderStatus.WaitingForParts,
            WorkOrderStatus.InProgress,
            WorkOrderStatus.InRepair,
            WorkOrderStatus.QualityControl,
            WorkOrderStatus.Washing,
            WorkOrderStatus.ReadyForDelivery
        };

        private static PartMatchScore? ComputeMatchScore(
            Part part,
            string brandLower,
            string modelLower,
            int vehicleYear,
            string searchedNameLower)
        {
            var partNameLower = part.Name.ToLower();
            var hasNameMatch = partNameLower.Contains(searchedNameLower) ||
                                searchedNameLower.Contains(partNameLower);
            if (!hasNameMatch) return null;

            if (!string.IsNullOrWhiteSpace(part.OEMNumber) &&
                part.OEMNumber.Contains(searchedNameLower, StringComparison.OrdinalIgnoreCase))
                return PartMatchScore.Exact;

            if (!string.IsNullOrWhiteSpace(part.AdditionalOemCodes))
            {
                try
                {
                    var codes = JsonSerializer.Deserialize<List<string>>(part.AdditionalOemCodes) ?? new();
                    if (codes.Any(c => c.Contains(searchedNameLower, StringComparison.OrdinalIgnoreCase)))
                        return PartMatchScore.Exact;
                }
                catch { /* geçersiz JSON — atla */ }
            }

            var brandMatch = IsJsonListMatch(part.CompatibleVehicleBrands, brandLower);
            var modelMatch = IsJsonListMatch(part.CompatibleVehicleModels, modelLower);

            var yearOk = (!part.CompatibleYearFrom.HasValue || vehicleYear >= part.CompatibleYearFrom.Value) &&
                         (!part.CompatibleYearTo.HasValue || vehicleYear <= part.CompatibleYearTo.Value);

            if (brandMatch && modelMatch && yearOk) return PartMatchScore.Compatible;
            if (brandMatch && modelMatch) return PartMatchScore.Risky;

            if (string.IsNullOrWhiteSpace(part.CompatibleVehicleBrands) &&
                string.IsNullOrWhiteSpace(part.CompatibleVehicleModels))
                return PartMatchScore.Unknown;

            return null;
        }

        private static bool IsJsonListMatch(string? json, string valueLower)
        {
            if (string.IsNullOrWhiteSpace(json)) return false;
            try
            {
                var list = JsonSerializer.Deserialize<List<string>>(json) ?? new();
                return list.Any(v => v.ToLower().Contains(valueLower) || valueLower.Contains(v.ToLower()));
            }
            catch { return false; }
        }
    }
}
