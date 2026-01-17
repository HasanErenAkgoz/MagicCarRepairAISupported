using MagicCarRepairAISupported.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MagicCarRepairAISupported.Persistence.Configurations
{
    public sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(x => x.Id);
            
            // Multi-tenant: Each client should have unique role-permission mappings
            builder.HasIndex(x => new { x.RoleId, x.PermissionId, x.ClientId }).IsUnique();
            builder.HasIndex(x => x.ClientId);
            
            // Relationships
            builder.HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasOne(rp => rp.Client)
                .WithMany()
                .HasForeignKey(rp => rp.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
