using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class PartPhotoConfiguration : IEntityTypeConfiguration<PartPhoto>
    {
        public void Configure(EntityTypeBuilder<PartPhoto> builder)
        {
            builder.ToTable("PartPhotos");
            builder.HasKey(pp => pp.Id);

            builder.Property(pp => pp.FilePath)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(pp => pp.Description)
                .HasMaxLength(500);

            builder.Property(pp => pp.UploadDate)
                .IsRequired();

            builder.Property(pp => pp.DisplayOrder)
                .IsRequired()
                .HasDefaultValue(0);

            builder.HasIndex(pp => pp.PartId);
            builder.HasIndex(pp => pp.ClientId);

            builder.HasOne(pp => pp.Part)
                .WithMany(p => p.Photos)
                .HasForeignKey(pp => pp.PartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pp => pp.UploadedFile)
                .WithMany()
                .HasForeignKey(pp => pp.UploadedFileId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(pp => pp.Client)
                .WithMany()
                .HasForeignKey(pp => pp.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
