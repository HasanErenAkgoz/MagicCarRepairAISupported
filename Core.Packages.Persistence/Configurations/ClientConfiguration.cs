using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(c => c.Code)
                .IsUnique();

            builder.Property(c => c.Description)
                .HasMaxLength(1000);

            builder.Property(c => c.ContactEmail)
                .HasMaxLength(100);

            builder.Property(c => c.ContactPhone)
                .HasMaxLength(20);

            builder.Property(c => c.Address)
                .HasMaxLength(500);

            builder.Property(c => c.TaxOfficeNo)
                .HasMaxLength(50);

            // Relationships
            builder.HasMany(c => c.Users)
                .WithOne(u => u.Client)
                .HasForeignKey(u => u.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Customers)
                .WithOne(cu => cu.Client)
                .HasForeignKey(cu => cu.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Vehicles)
                .WithOne(v => v.Client)
                .HasForeignKey(v => v.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Profile/Showcase relationships
            builder.HasMany(c => c.Portfolios)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Certificates)
                .WithOne(cert => cert.Client)
                .HasForeignKey(cert => cert.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.FacilityPhotos)
                .WithOne(fp => fp.Client)
                .HasForeignKey(fp => fp.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Subscriptions)
                .WithOne(s => s.Client)
                .HasForeignKey(s => s.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Profile properties
            builder.Property(c => c.LogoUrl)
                .HasMaxLength(500);

            builder.Property(c => c.WebsiteUrl)
                .HasMaxLength(500);

            builder.Property(c => c.AboutUs)
                .HasColumnType("nvarchar(max)");

            builder.Property(c => c.WorkingHours)
                .HasMaxLength(1000);

            builder.Property(c => c.Services)
                .HasMaxLength(1000);

            builder.Property(c => c.SocialMediaLinks)
                .HasMaxLength(1000);
        }
    }
}

