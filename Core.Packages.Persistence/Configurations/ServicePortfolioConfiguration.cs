using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class ServicePortfolioConfiguration : IEntityTypeConfiguration<ServicePortfolio>
    {
        public void Configure(EntityTypeBuilder<ServicePortfolio> builder)
        {
            builder.ToTable("ServicePortfolios");

            builder.HasKey(sp => sp.Id);

            builder.Property(sp => sp.WorkOrderId)
                .IsRequired();

            builder.Property(sp => sp.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(sp => sp.Description)
                .HasMaxLength(1000);

            builder.Property(sp => sp.Categories)
                .HasMaxLength(500);

            builder.Property(sp => sp.FeaturedPhotoIds)
                .HasMaxLength(1000);

            builder.Property(sp => sp.CustomerRejectionReason)
                .HasMaxLength(500);

            // Relationships
            builder.HasOne(sp => sp.WorkOrder)
                .WithMany()
                .HasForeignKey(sp => sp.WorkOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sp => sp.Client)
                .WithMany(c => c.Portfolios)
                .HasForeignKey(sp => sp.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(sp => sp.ClientId);
            builder.HasIndex(sp => sp.WorkOrderId);
            builder.HasIndex(sp => new { sp.ClientId, sp.IsPublished, sp.CustomerApprovalStatus });
        }
    }
}
