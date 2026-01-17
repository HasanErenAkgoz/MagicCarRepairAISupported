namespace MagicCarRepairAISupported.Application.Common.Services.WorkOrder
{
    public interface IPdfWorkOrderService
    {
        Task<byte[]> GenerateWorkOrderPdfAsync(int workOrderId, CancellationToken cancellationToken = default);
    }
}
