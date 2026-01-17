using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
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
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(qres => qres.Description)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(qres => qres.QuoteAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(qres => qres.DiscountRate)
                .HasColumnType("decimal(5,2)")
                .HasDefaultValue(0);

            builder.Property(qres => qres.DiscountAmount)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(qres => qres.TaxRate)
                .HasColumnType("decimal(5,2)")
                .HasDefaultValue(20);

            builder.Property(qres => qres.TaxAmount)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(qres => qres.NetAmount)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(qres => qres.Notes)
                .HasMaxLength(1000);

            builder.Property(qres => qres.RejectionReason)
                .HasMaxLength(500);

            builder.Property(qres => qres.Status)
                .HasConversion<int>()
                .IsRequired()
                .HasDefaultValue(QuoteResponseStatus.Pending);

            // Relationships
            builder.HasOne(qres => qres.QuoteRequest)
                .WithMany(qr => qr.QuoteResponses)
                .HasForeignKey(qres => qres.QuoteRequestId)
                .OnDelete(DeleteBehavior.Restrict); // QuoteRequest silinirse QuoteResponse'lar da silinmemeli

            builder.HasOne(qres => qres.Client)
                .WithMany()
                .HasForeignKey(qres => qres.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(qres => qres.QuoteNumber)
                .IsUnique();

            builder.HasIndex(qres => new { qres.QuoteNumber, qres.ClientId })
                .IsUnique();

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

