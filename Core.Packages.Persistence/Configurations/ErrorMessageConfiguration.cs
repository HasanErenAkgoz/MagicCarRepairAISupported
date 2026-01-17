using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class ErrorMessageConfiguration : IEntityTypeConfiguration<ErrorMessage>
    {
        public void Configure(EntityTypeBuilder<ErrorMessage> builder)
        {
            builder.ToTable("ErrorMessages");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.ErrorCode)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Language)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.Message)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(e => e.Description)
                .HasMaxLength(2000);

            builder.HasIndex(e => new { e.ErrorCode, e.Language })
                .IsUnique();
        }
    }
}

