using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GeneratePdf
{
    public class GenerateWorkOrderPdfQuery : IRequest<byte[]>
    {
        public int WorkOrderId { get; set; }
    }
}
