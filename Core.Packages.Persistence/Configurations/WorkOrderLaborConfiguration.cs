using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public sealed class WorkOrderLaborConfiguration : IEntityTypeConfiguration<WorkOrderLabor>
    {
        public void Configure(EntityTypeBuilder<WorkOrderLabor> builder)
        {
            builder.ToTable("WorkOrderLabors");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.OperationName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(l => l.DurationHours)
                .HasColumnType("decimal(10,2)");

            builder.Property(l => l.HourlyRate)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(l => l.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(l => l.Description)
                .HasMaxLength(1000);

            // Indexes
            builder.HasIndex(l => l.WorkOrderId);
            builder.HasIndex(l => l.EmployeeId);
            builder.HasIndex(l => l.ClientId);

            // Relationships
            builder.HasOne(l => l.Client)
                .WithMany()
                .HasForeignKey(l => l.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.WorkOrder)
                .WithMany(w => w.Labors)
                .HasForeignKey(l => l.WorkOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(l => l.Employee)
                .WithMany()
                .HasForeignKey(l => l.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

