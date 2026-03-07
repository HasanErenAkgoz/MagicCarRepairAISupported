using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class QuoteRequestConfiguration : IEntityTypeConfiguration<QuoteRequest>
    {
        public void Configure(EntityTypeBuilder<QuoteRequest> builder)
        {
            builder.ToTable("QuoteRequests");

            builder.HasKey(qr => qr.Id);

            builder.Property(qr => qr.RequestNumber)
                .HasMaxLength(50);

            builder.Property(qr => qr.Description)
                .HasMaxLength(2000);

            builder.Property(qr => qr.VehicleBrand)
                .HasMaxLength(100);

            builder.Property(qr => qr.VehicleModel)
                .HasMaxLength(100);

            builder.Property(qr => qr.VehicleLicensePlate)
                .HasMaxLength(20);

            builder.Property(qr => qr.CustomerEmail)
                .HasMaxLength(255);

            builder.Property(qr => qr.CustomerPhone)
                .HasMaxLength(20);

            builder.Property(qr => qr.CustomerName)
                .HasMaxLength(200);

            builder.Property(qr => qr.RequestType)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(qr => qr.UrgencyLevel)
                .HasConversion<int>()
                .IsRequired()
                .HasDefaultValue(UrgencyLevel.Normal);

            builder.Property(qr => qr.Status)
                .HasConversion<int>()
                .IsRequired()
                .HasDefaultValue(QuoteStatus.Open);

            builder.Property(qr => qr.PhotoPaths)
                .HasDefaultValue("[]");

            // Ignore computed / alias properties
            builder.Ignore(qr => qr.ProblemDescription);

            // Relationships
            builder.HasOne(qr => qr.Customer)
                .WithMany()
                .HasForeignKey(qr => qr.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(qr => qr.Vehicle)
                .WithMany()
                .HasForeignKey(qr => qr.VehicleId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(qr => qr.Client)
                .WithMany()
                .HasForeignKey(qr => qr.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(qr => qr.QuoteResponses)
                .WithOne(qres => qres.QuoteRequest)
                .HasForeignKey(qres => qres.QuoteRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(qr => qr.RequestNumber);
            builder.HasIndex(qr => qr.CustomerId);
            builder.HasIndex(qr => qr.VehicleId);
            builder.HasIndex(qr => qr.ClientId);
            builder.HasIndex(qr => qr.Status);
            builder.HasIndex(qr => qr.QuoteDeadline);
        }
    }
}
