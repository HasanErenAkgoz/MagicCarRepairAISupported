using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public sealed class ServiceRatingConfiguration : IEntityTypeConfiguration<ServiceRating>
    {
        public void Configure(EntityTypeBuilder<ServiceRating> builder)
        {
            builder.ToTable("ServiceRatings");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Rating)
                .IsRequired()
                .HasMaxLength(1); // 1-5

            builder.Property(r => r.ServiceQuality)
                .IsRequired()
                .HasMaxLength(1); // 1-5

            builder.Property(r => r.PriceValue)
                .IsRequired()
                .HasMaxLength(1); // 1-5

            builder.Property(r => r.OnTimeDelivery)
                .IsRequired()
                .HasMaxLength(1); // 1-5

            builder.Property(r => r.StaffBehavior)
                .IsRequired()
                .HasMaxLength(1); // 1-5

            builder.Property(r => r.Comment)
                .HasMaxLength(2000);

            builder.Property(r => r.Photos)
                .HasColumnType("text"); // JSON

            builder.Property(r => r.Status)
                .IsRequired()
                .HasConversion<int>()
                .HasDefaultValue(RatingStatus.Pending);

            builder.Property(r => r.ServiceReply)
                .HasMaxLength(2000);

            // Indexes
            builder.HasIndex(r => r.WorkOrderId)
                .IsUnique(); // Her iş emri için bir rating

            builder.HasIndex(r => new { r.ClientId, r.Status });
            builder.HasIndex(r => r.CustomerId);
            builder.HasIndex(r => r.CreatedDate);

            // Relationships
            builder.HasOne(r => r.Client)
                .WithMany()
                .HasForeignKey(r => r.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.WorkOrder)
                .WithMany()
                .HasForeignKey(r => r.WorkOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Customer)
                .WithMany()
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
