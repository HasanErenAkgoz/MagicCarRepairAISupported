using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Domain.Repositories
{
    public interface IPartRepository : IEntityRepository<Part, int>
    {
        Task<Part?> GetByPartCodeAsync(string partCode, CancellationToken cancellationToken = default);
        Task<List<Part>> GetByCategoryAsync(PartCategory category, CancellationToken cancellationToken = default);
        Task<List<Part>> GetLowStockPartsAsync(CancellationToken cancellationToken = default);
        Task<List<Part>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
        Task<bool> IsPartCodeExistsAsync(string partCode, CancellationToken cancellationToken = default);
        Task<Part?> GetWithStockAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> IsPartUsedInActiveWorkOrdersAsync(int partId, CancellationToken cancellationToken = default);
        Task<List<Part>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);
        Task<List<int>> GetUsedInActiveWorkOrdersPartIdsAsync(IEnumerable<int> partIds, CancellationToken cancellationToken = default);

        /// <summary>
        /// Tüm tenant envanterlerinde araç uyumlu parça arar (tenant filtresi bypass).
        /// AI teşhis sonrası çapraz servis fiyat çözümleme için kullanılır.
        /// </summary>
        Task<List<CrossTenantPartMatch>> SearchCrossTenantAsync(
            string vehicleBrand,
            string vehicleModel,
            int vehicleYear,
            IEnumerable<string> partNames,
            CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Cross-tenant envanter araması sonucu — servis bazlı parça eşleşmesi.
    /// </summary>
    public record CrossTenantPartMatch(
        int PartId,
        string PartName,
        string? PartCode,
        string? OemNumber,
        decimal SalePrice,
        int StockQuantity,
        int ClientId,
        string ShopName,
        string? ShopLogoUrl,
        double? ShopLatitude,
        double? ShopLongitude,
        PartMatchScore MatchScore,
        string SearchedPartName
    );
}

