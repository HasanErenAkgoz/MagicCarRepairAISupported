using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Interfaces;

namespace MagicCarRepairAISupported.Domain.Entities;

/// <summary>
/// An explicit staff participant. Customer and assigned technician remain derived,
/// so assignment changes and account deactivation take effect immediately.
/// </summary>
public sealed class WorkOrderParticipant : BaseEntity<int>, IClientEntity
{
    public int WorkOrderId { get; set; }
    public WorkOrder WorkOrder { get; set; } = null!;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public WorkOrderParticipantRole Role { get; set; } = WorkOrderParticipantRole.ServiceAdvisor;
    public int AddedByUserId { get; set; }
    public DateTime? RemovedAt { get; set; }
    public int? RemovedByUserId { get; set; }
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;
}
