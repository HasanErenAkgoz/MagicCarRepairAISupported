using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public sealed class WorkOrderPhotoConfiguration : IEntityTypeConfiguration<WorkOrderPhoto>
    {
        public void Configure(EntityTypeBuilder<WorkOrderPhoto> builder)
        {
            builder.ToTable("WorkOrderPhotos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.FilePath)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.Property(p => p.PhotoType)
                .IsRequired()
                .HasConversion<int>()
                .HasDefaultValue(WorkOrderPhotoType.Process);

            builder.Property(p => p.UploadDate)
                .IsRequired();

            // Indexes
            builder.HasIndex(p => p.WorkOrderId);
            builder.HasIndex(p => p.TimelineId);
            builder.HasIndex(p => p.UploadedFileId);
            builder.HasIndex(p => p.UploadedByEmployeeId);
            builder.HasIndex(p => p.ClientId);
            builder.HasIndex(p => p.PhotoType);

            // Relationships
            builder.HasOne(p => p.Client)
                .WithMany()
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.WorkOrder)
                .WithMany(w => w.Photos)
                .HasForeignKey(p => p.WorkOrderId)
                .OnDelete(DeleteBehavior.Restrict); // NO ACTION to avoid cascade path conflict

            builder.HasOne(p => p.Timeline)
                .WithMany()
                .HasForeignKey(p => p.TimelineId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.UploadedFile)
                .WithMany()
                .HasForeignKey(p => p.UploadedFileId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.UploadedByEmployee)
                .WithMany()
                .HasForeignKey(p => p.UploadedByEmployeeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

