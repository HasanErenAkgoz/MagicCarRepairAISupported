using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GenerateQrCode
{
    public class GenerateWorkOrderQrCodeQuery : IRequest<byte[]>
    {
        public int WorkOrderId { get; set; }
    }
}
