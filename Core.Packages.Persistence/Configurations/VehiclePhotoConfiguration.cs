using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class VehiclePhotoConfiguration : IEntityTypeConfiguration<VehiclePhoto>
    {
        public void Configure(EntityTypeBuilder<VehiclePhoto> builder)
        {
            builder.ToTable("VehiclePhotos");
            builder.HasKey(vp => vp.Id);

            builder.Property(vp => vp.FilePath)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(vp => vp.Description)
                .HasMaxLength(500);

            builder.Property(vp => vp.PhotoType)
                .HasMaxLength(50);

            builder.Property(vp => vp.UploadDate)
                .IsRequired();

            builder.Property(vp => vp.DisplayOrder)
                .IsRequired()
                .HasDefaultValue(0);

            builder.HasIndex(vp => vp.VehicleId);
            builder.HasIndex(vp => vp.ClientId);

            // Relationships
            builder.HasOne(vp => vp.Vehicle)
                .WithMany(v => v.Photos)
                .HasForeignKey(vp => vp.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(vp => vp.UploadedFile)
                .WithMany()
                .HasForeignKey(vp => vp.UploadedFileId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(vp => vp.UploadedByEmployee)
                .WithMany()
                .HasForeignKey(vp => vp.UploadedByEmployeeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(vp => vp.Client)
                .WithMany()
                .HasForeignKey(vp => vp.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
