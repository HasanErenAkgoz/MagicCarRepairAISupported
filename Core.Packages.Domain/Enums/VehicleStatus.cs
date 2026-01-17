using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCarRepairAISupported.Domain.Enums
{
    public enum VehicleStatus
    {
        Registered = 1,
        AwaitingInspection = 2,
        InRepair = 3,
        ReadyForDelivery = 4,
        Delivered = 5,
        Cancelled = 6
    }

}
