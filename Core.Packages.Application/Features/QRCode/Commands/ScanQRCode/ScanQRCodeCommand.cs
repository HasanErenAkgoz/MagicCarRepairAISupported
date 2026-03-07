using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.QRCode.Commands.ScanQRCode
{
    public class ScanQRCodeCommand : IRequest<IDataResult<ScanQRCodeResponse>>
    {
        public string QRCodeData { get; set; } = string.Empty;
    }

    public class ScanQRCodeResponse
    {
        public QRCodeType Type { get; set; }
        public int? EntityId { get; set; }
        public string Data { get; set; } = string.Empty;
        public bool IsValid { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public enum QRCodeType
    {
        WorkOrder = 1,
        Invoice = 2,
        Vehicle = 3,
        Customer = 4,
        QuickService = 5
    }
}
