using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public sealed class InsuranceCompanyConfiguration : IEntityTypeConfiguration<InsuranceCompany>
    {
        public void Configure(EntityTypeBuilder<InsuranceCompany> builder)
        {
            builder.ToTable("InsuranceCompanies");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.CompanyName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(i => i.CompanyCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(i => i.ContactPerson)
                .HasMaxLength(200);

            builder.Property(i => i.Phone)
                .HasMaxLength(50);

            builder.Property(i => i.Email)
                .HasMaxLength(200);

            builder.Property(i => i.Address)
                .HasMaxLength(500);

            builder.Property(i => i.ApiEndpoint)
                .HasMaxLength(500);

            builder.Property(i => i.ApiKey)
                .HasMaxLength(500); // Encrypted in production

            builder.Property(i => i.SupportedInsuranceTypes)
                .HasColumnType("nvarchar(max)"); // JSON

            builder.Property(i => i.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            // Indexes
            builder.HasIndex(i => i.CompanyCode)
                .IsUnique();

            builder.HasIndex(i => new { i.ClientId, i.CompanyCode })
                .IsUnique();

            // Relationships
            builder.HasOne(i => i.Client)
                .WithMany()
                .HasForeignKey(i => i.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(i => i.Policies)
                .WithOne(p => p.InsuranceCompany)
                .HasForeignKey(p => p.InsuranceCompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

