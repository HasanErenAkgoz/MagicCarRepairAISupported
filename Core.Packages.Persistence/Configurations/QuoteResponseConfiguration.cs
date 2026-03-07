using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class QuoteResponseConfiguration : IEntityTypeConfiguration<QuoteResponse>
    {
        public void Configure(EntityTypeBuilder<QuoteResponse> builder)
        {
            builder.ToTable("QuoteResponses");

            builder.HasKey(qres => qres.Id);

            builder.Property(qres => qres.QuoteNumber)
                .HasMaxLength(50);

            builder.Property(qres => qres.Description)
                .HasMaxLength(2000);

            builder.Property(qres => qres.QuoteAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(qres => qres.Amount)
                .HasColumnType("decimal(18,2)");

            builder.Property(qres => qres.DiscountRate)
                .HasColumnType("decimal(5,2)");

            builder.Property(qres => qres.TaxRate)
                .HasColumnType("decimal(5,2)");

            builder.Property(qres => qres.Notes)
                .HasMaxLength(1000);

            builder.Property(qres => qres.Status)
                .HasMaxLength(50)
                .IsRequired()
                .HasDefaultValue("Pending");

            // Computed properties - ignore them to avoid mapping issues
            builder.Ignore(qres => qres.DiscountAmount);
            builder.Ignore(qres => qres.NetAmount);

            // Relationships
            builder.HasOne(qres => qres.QuoteRequest)
                .WithMany(qr => qr.QuoteResponses)
                .HasForeignKey(qres => qres.QuoteRequestId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(qres => qres.Client)
                .WithMany()
                .HasForeignKey(qres => qres.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(qres => qres.QuoteRequestId);
            builder.HasIndex(qres => qres.ClientId);
            builder.HasIndex(qres => qres.Status);
            builder.HasIndex(qres => qres.ValidUntilDate);

            // Unique constraint: Bir QuoteRequest'e bir Client sadece bir kez teklif verebilir
            builder.HasIndex(qres => new { qres.QuoteRequestId, qres.ClientId })
                .IsUnique();
        }
    }
}
