using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MagicCarRepairAISupported.Persistence.Context;
using MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Repositories
{
    public class PartRepository : EfEntityRepository<Part, BaseDbContext>, IPartRepository
    {
        public PartRepository(BaseDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<Part?> GetByIdAsync(int id)
        {
            return await Context.Set<Part>().FindAsync(id);
        }

        public async Task<Part?> GetByPartCodeAsync(string partCode, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Part>()
                .Include(p => p.Stock)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.PartCode == partCode, cancellationToken);
        }

        public async Task<List<Part>> GetByCategoryAsync(PartCategory category, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Part>()
                .Include(p => p.Stock)
                .Where(p => p.Category == category)
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Part>> GetLowStockPartsAsync(CancellationToken cancellationToken = default)
        {
            return await Context.Set<Part>()
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
            return await Context.Set<Part>()
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
            return await Context.Set<Part>()
                .AnyAsync(p => p.PartCode == partCode, cancellationToken);
        }

        public async Task<Part?> GetWithStockAsync(int id, CancellationToken cancellationToken = default)
        {
            return await Context.Set<Part>()
                .Include(p => p.Stock)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<bool> IsPartUsedInActiveWorkOrdersAsync(int partId, CancellationToken cancellationToken = default)
        {
            var activeStatuses = new[]
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

            return await Context.Set<WorkOrderItem>()
                .Include(woi => woi.WorkOrder)
                .AnyAsync(woi => woi.PartId == partId &&
                                woi.Status != Status.Deleted &&
                                activeStatuses.Contains(woi.WorkOrder.Status),
                         cancellationToken);
        }
    }
}

