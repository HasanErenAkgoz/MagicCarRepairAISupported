using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Domain.Common
{
    public abstract class BaseEntity<TId> where TId : struct
    {
        public TId Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public TId CreatedBy { get; set; }
        public TId? ModifiedBy { get; set; }
        public Status Status { get; set; }
    }
}
