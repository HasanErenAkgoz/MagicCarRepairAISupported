using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> builder)
        {
            builder.ToTable("Certificates");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.CertificateName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.IssuingOrganization)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.CertificateNumber)
                .HasMaxLength(100);

            builder.Property(c => c.CertificateFileUrl)
                .HasMaxLength(500);

            builder.Property(c => c.Description)
                .HasMaxLength(1000);

            // Relationships
            builder.HasOne(c => c.Client)
                .WithMany(cl => cl.Certificates)
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(c => c.ClientId);
            builder.HasIndex(c => new { c.ClientId, c.IsPublic });
            builder.HasIndex(c => c.ExpiryDate);
        }
    }
}
