using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace MagicCarRepairAISupported.Domain.Entities
{
    public class Role : IdentityRole<int>, IClientEntity
    {
        // Multi-tenant support
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }

}
