using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class FacilityPhotoConfiguration : IEntityTypeConfiguration<FacilityPhoto>
    {
        public void Configure(EntityTypeBuilder<FacilityPhoto> builder)
        {
            builder.ToTable("FacilityPhotos");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.PhotoPath)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(f => f.Title)
                .HasMaxLength(200);

            builder.Property(f => f.Description)
                .HasMaxLength(1000);

            builder.Property(f => f.Category)
                .HasMaxLength(100);

            // Relationships
            builder.HasOne(f => f.Client)
                .WithMany(c => c.FacilityPhotos)
                .HasForeignKey(f => f.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(f => f.ClientId);
            builder.HasIndex(f => new { f.ClientId, f.IsPublic, f.Category });
        }
    }
}
