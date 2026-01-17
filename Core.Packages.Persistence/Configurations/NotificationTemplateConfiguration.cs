using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class NotificationTemplateConfiguration : IEntityTypeConfiguration<NotificationTemplate>
    {
        public void Configure(EntityTypeBuilder<NotificationTemplate> builder)
        {
            builder.ToTable("NotificationTemplates");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Description)
                .HasMaxLength(500);

            builder.Property(t => t.TitleTemplate)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(t => t.ContentTemplate)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(t => t.RelatedEntityType)
                .HasMaxLength(100);

            builder.Property(t => t.Type)
                .HasConversion<int>()
                .IsRequired();

            // Relationships
            builder.HasOne(t => t.Client)
                .WithMany()
                .HasForeignKey(t => t.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(t => t.Name);
            builder.HasIndex(t => t.ClientId);
            builder.HasIndex(t => new { t.Name, t.ClientId }).IsUnique();
            builder.HasIndex(t => t.RelatedEntityType);
        }
    }
}

