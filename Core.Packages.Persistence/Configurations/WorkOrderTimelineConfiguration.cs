using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public sealed class WorkOrderTimelineConfiguration : IEntityTypeConfiguration<WorkOrderTimeline>
    {
        public void Configure(EntityTypeBuilder<WorkOrderTimeline> builder)
        {
            builder.ToTable("WorkOrderTimelines");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.EventDate)
                .IsRequired();

            builder.Property(t => t.StatusChange)
                .HasConversion<int>();

            builder.Property(t => t.OldStatus)
                .HasConversion<int>();

            builder.Property(t => t.NewStatus)
                .HasConversion<int>();

            builder.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(t => t.PhotoIds)
                .HasColumnType("nvarchar(max)"); // JSON array

            builder.Property(t => t.EventType)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue("StatusChange");

            // Indexes
            builder.HasIndex(t => t.WorkOrderId);
            builder.HasIndex(t => t.EmployeeId);
            builder.HasIndex(t => t.EventDate);
            builder.HasIndex(t => t.ClientId);

            // Relationships
            builder.HasOne(t => t.Client)
                .WithMany()
                .HasForeignKey(t => t.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.WorkOrder)
                .WithMany(w => w.Timeline)
                .HasForeignKey(t => t.WorkOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Employee)
                .WithMany()
                .HasForeignKey(t => t.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

