using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.Title)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(n => n.Content)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(n => n.RecipientEmail)
                .HasMaxLength(255);

            builder.Property(n => n.RecipientPhone)
                .HasMaxLength(20);

            builder.Property(n => n.ErrorMessage)
                .HasMaxLength(1000);

            builder.Property(n => n.RelatedEntityType)
                .HasMaxLength(100);

            builder.Property(n => n.ExtraData)
                .HasColumnType("nvarchar(max)");

            builder.Property(n => n.Type)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(n => n.Status)
                .HasConversion<int>()
                .IsRequired()
                .HasDefaultValue(NotificationStatus.Pending);

            // Relationships
            builder.HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(n => n.Client)
                .WithMany()
                .HasForeignKey(n => n.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(n => n.UserId);
            builder.HasIndex(n => n.ClientId);
            builder.HasIndex(n => n.Status);
            builder.HasIndex(n => n.Type);
            builder.HasIndex(n => new { n.RelatedEntityType, n.RelatedEntityId });
            builder.HasIndex(n => n.SentDate);
        }
    }
}

