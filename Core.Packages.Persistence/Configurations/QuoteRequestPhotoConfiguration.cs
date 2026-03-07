using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class QuoteRequestPhotoConfiguration : IEntityTypeConfiguration<QuoteRequestPhoto>
    {
        public void Configure(EntityTypeBuilder<QuoteRequestPhoto> builder)
        {
            builder.ToTable("QuoteRequestPhotos");

            builder.HasKey(qp => qp.Id);

            builder.Property(qp => qp.FilePath)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(qp => qp.Description)
                .HasMaxLength(500);

            builder.Property(qp => qp.PhotoType)
                .HasConversion<int>()
                .IsRequired()
                .HasDefaultValue(QuoteRequestPhotoType.General);

            // Relationships
            builder.HasOne(qp => qp.QuoteRequest)
                .WithMany()
                .HasForeignKey(qp => qp.QuoteRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(qp => qp.UploadedFile)
                .WithMany()
                .HasForeignKey(qp => qp.UploadedFileId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(qp => qp.UploadedByEmployee)
                .WithMany()
                .HasForeignKey(qp => qp.UploadedByEmployeeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(qp => qp.Client)
                .WithMany()
                .HasForeignKey(qp => qp.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(qp => qp.QuoteRequestId);
            builder.HasIndex(qp => qp.ClientId);
        }
    }
}

