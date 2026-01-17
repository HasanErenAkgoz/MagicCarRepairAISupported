using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public sealed class InsuranceClaimConfiguration : IEntityTypeConfiguration<InsuranceClaim>
    {
        public void Configure(EntityTypeBuilder<InsuranceClaim> builder)
        {
            builder.ToTable("InsuranceClaims");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.ClaimNumber)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.DamageDate)
                .IsRequired();

            builder.Property(c => c.DamageDescription)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(c => c.DamageAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.ApprovedAmount)
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.DeductibleAmount)
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.PayableAmount)
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.Status)
                .IsRequired()
                .HasConversion<int>()
                .HasDefaultValue(ClaimStatus.Applied);

            builder.Property(c => c.RejectionReason)
                .HasMaxLength(1000);

            builder.Property(c => c.Photos)
                .HasColumnType("nvarchar(max)"); // JSON

            builder.Property(c => c.Notes)
                .HasMaxLength(2000);

            // Indexes
            builder.HasIndex(c => c.ClaimNumber)
                .IsUnique();

            builder.HasIndex(c => new { c.ClientId, c.ClaimNumber })
                .IsUnique();

            builder.HasIndex(c => c.WorkOrderId);
            builder.HasIndex(c => c.InsurancePolicyId);
            builder.HasIndex(c => c.Status);
            builder.HasIndex(c => c.DamageDate);

            // Relationships
            builder.HasOne(c => c.Client)
                .WithMany()
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.WorkOrder)
                .WithMany()
                .HasForeignKey(c => c.WorkOrderId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(c => c.InsurancePolicy)
                .WithMany(p => p.Claims)
                .HasForeignKey(c => c.InsurancePolicyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

