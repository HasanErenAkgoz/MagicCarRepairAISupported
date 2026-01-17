using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public sealed class InsurancePolicyConfiguration : IEntityTypeConfiguration<InsurancePolicy>
    {
        public void Configure(EntityTypeBuilder<InsurancePolicy> builder)
        {
            builder.ToTable("InsurancePolicies");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.PolicyNumber)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.InsuranceType)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(p => p.StartDate)
                .IsRequired();

            builder.Property(p => p.EndDate)
                .IsRequired();

            builder.Property(p => p.PremiumAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.CoverageAmount)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.DeductiblePercentage)
                .IsRequired()
                .HasColumnType("decimal(5,2)")
                .HasDefaultValue(0);

            builder.Property(p => p.DeductibleAmount)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Status)
                .IsRequired()
                .HasConversion<int>()
                .HasDefaultValue(InsuranceStatus.Active);

            builder.Property(p => p.Notes)
                .HasMaxLength(2000);

            // Indexes
            builder.HasIndex(p => p.PolicyNumber)
                .IsUnique();

            builder.HasIndex(p => new { p.ClientId, p.PolicyNumber })
                .IsUnique();

            builder.HasIndex(p => p.VehicleId);
            builder.HasIndex(p => p.CustomerId);
            builder.HasIndex(p => p.InsuranceCompanyId);
            builder.HasIndex(p => p.EndDate);
            builder.HasIndex(p => new { p.Status, p.EndDate });

            // Relationships
            builder.HasOne(p => p.Client)
                .WithMany()
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Vehicle)
                .WithMany()
                .HasForeignKey(p => p.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Customer)
                .WithMany()
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.InsuranceCompany)
                .WithMany(i => i.Policies)
                .HasForeignKey(p => p.InsuranceCompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.PolicyFile)
                .WithMany()
                .HasForeignKey(p => p.PolicyFileId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(p => p.Claims)
                .WithOne(c => c.InsurancePolicy)
                .HasForeignKey(c => c.InsurancePolicyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

