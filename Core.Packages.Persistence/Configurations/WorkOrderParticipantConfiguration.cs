using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations;

public sealed class WorkOrderParticipantConfiguration : IEntityTypeConfiguration<WorkOrderParticipant>
{
    public void Configure(EntityTypeBuilder<WorkOrderParticipant> builder)
    {
        builder.ToTable("WorkOrderParticipants");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new { x.WorkOrderId, x.UserId }).IsUnique();
        builder.HasIndex(x => new { x.ClientId, x.WorkOrderId, x.RemovedAt });
        builder.HasOne(x => x.WorkOrder).WithMany().HasForeignKey(x => x.WorkOrderId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Client).WithMany().HasForeignKey(x => x.ClientId).OnDelete(DeleteBehavior.Restrict);
    }
}
