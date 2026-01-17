using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            
            // Multi-tenant: Role names should be unique per client
            builder.HasIndex(x => new { x.Name, x.ClientId }).IsUnique();
            builder.HasIndex(x => x.ClientId);
            
            // Relationship to Client
            builder.HasOne(r => r.Client)
                .WithMany()
                .HasForeignKey(r => r.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
