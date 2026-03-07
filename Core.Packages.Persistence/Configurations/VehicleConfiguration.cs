using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("Vehicles");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.LicensePlate)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(v => v.Brand)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(v => v.Model)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(v => v.Color)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(v => v.Vin)
                .HasMaxLength(50);

            builder.Property(v => v.ModelVariant)
                .HasMaxLength(100);

            builder.Property(v => v.Trim)
                .HasMaxLength(100);

            builder.Property(v => v.VehicleType)
                .IsRequired();

            builder.HasIndex(v => v.ClientId);
            builder.HasIndex(v => new { v.LicensePlate, v.ClientId });

            builder.HasOne(v => v.Customer)
                .WithMany(c => c.Vehicles)
                .HasForeignKey(v => v.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

