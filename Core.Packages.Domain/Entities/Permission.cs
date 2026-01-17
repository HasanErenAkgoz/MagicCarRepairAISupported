using MagicCarRepairAISupported.Domain.Comman;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Permission entity - Global permissions available to all clients
    /// Client-specific permission assignment is handled via RolePermission table
    /// </summary>
    public class Permission : BaseEntity<int>
    {
        public string Name { get; set; }
        public string? Description { get; set; } = string.Empty;
        
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }

}
