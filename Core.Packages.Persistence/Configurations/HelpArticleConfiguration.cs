using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class HelpArticleConfiguration : IEntityTypeConfiguration<HelpArticle>
    {
        public void Configure(EntityTypeBuilder<HelpArticle> builder)
        {
            builder.ToTable("HelpArticles");

            builder.HasKey(h => h.Id);

            builder.Property(h => h.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(h => h.Content)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(h => h.Category)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(h => h.Order)
                .HasDefaultValue(0);

            builder.Property(h => h.IsPublished)
                .HasDefaultValue(true);

            builder.Property(h => h.ViewCount)
                .HasDefaultValue(0);

            // Indexes
            builder.HasIndex(h => h.Category);
            builder.HasIndex(h => h.IsPublished);
            builder.HasIndex(h => h.ClientId);
            builder.HasIndex(h => new { h.Category, h.Order });

            // Relationships
            builder.HasOne(h => h.Client)
                .WithMany()
                .HasForeignKey(h => h.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
