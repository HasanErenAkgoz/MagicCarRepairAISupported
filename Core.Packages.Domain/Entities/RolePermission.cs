using MagicCarRepairAISupported.Domain.Comman;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities
{
    public class RolePermission : BaseEntity<int>, IClientEntity
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
        
        // Multi-tenant support
        public int ClientId { get; set; }
        public virtual Client? Client { get; set; }
    }

}
