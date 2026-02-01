using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.IdentityNo)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.Address)
                .HasMaxLength(500);

            builder.Property(c => c.Language)
                .IsRequired()
                .HasMaxLength(10)
                .HasDefaultValue("tr");

            builder.Property(c => c.IsVip)
                .IsRequired()
                .HasDefaultValue(false);

            // User relationship (nullable)
            builder.HasIndex(c => c.UserId);

            builder.HasIndex(c => c.ClientId);
            builder.HasIndex(c => new { c.IdentityNo, c.ClientId });

            builder.Ignore(c => c.FullName);
        }
    }
}

