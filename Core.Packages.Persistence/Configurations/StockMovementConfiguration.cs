using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public sealed class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
    {
        public void Configure(EntityTypeBuilder<StockMovement> builder)
        {
            builder.ToTable("StockMovements");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.MovementType)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(m => m.Quantity)
                .IsRequired();

            builder.Property(m => m.MovementDate)
                .IsRequired();

            builder.Property(m => m.Description)
                .HasMaxLength(500);

            builder.Property(m => m.ReferenceNumber)
                .HasMaxLength(100);

            builder.Property(m => m.ReferenceType)
                .HasMaxLength(50);

            builder.Property(m => m.UnitPrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(m => m.TotalPrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(m => m.TargetLocation)
                .HasMaxLength(200);

            // Multi-tenant
            builder.HasIndex(m => m.PartId);
            builder.HasIndex(m => m.ClientId);
            builder.HasIndex(m => m.MovementType);
            builder.HasIndex(m => m.MovementDate);
            builder.HasIndex(m => m.EmployeeId);
            builder.HasIndex(m => m.ReferenceNumber);

            builder.HasOne(m => m.Client)
                .WithMany()
                .HasForeignKey(m => m.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Part)
                .WithMany(p => p.StockMovements)
                .HasForeignKey(m => m.PartId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Employee)
                .WithMany()
                .HasForeignKey(m => m.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

