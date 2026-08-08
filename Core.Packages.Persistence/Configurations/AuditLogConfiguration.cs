using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public sealed class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.EntityName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Action)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.OldValues)
                .HasColumnType("text");

            builder.Property(a => a.NewValues)
                .HasColumnType("text");

            builder.Property(a => a.ChangedProperties)
                .HasColumnType("text");

            builder.Property(a => a.IpAddress)
                .HasMaxLength(50);

            builder.Property(a => a.UserAgent)
                .HasMaxLength(500);

            builder.Property(a => a.Description)
                .HasMaxLength(1000);

            builder.Property(a => a.ErrorMessage)
                .HasMaxLength(2000);

            builder.Property(a => a.RequestPath)
                .HasMaxLength(500);

            builder.Property(a => a.RequestMethod)
                .HasMaxLength(10);

            // Indexes
            builder.HasIndex(a => a.UserId);
            builder.HasIndex(a => a.EntityName);
            builder.HasIndex(a => a.EntityId);
            builder.HasIndex(a => a.Action);
            builder.HasIndex(a => a.CreatedDate);
            builder.HasIndex(a => a.ClientId);
            builder.HasIndex(a => new { a.EntityName, a.EntityId });

            // Relationships
            builder.HasOne(a => a.Client)
                .WithMany()
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
