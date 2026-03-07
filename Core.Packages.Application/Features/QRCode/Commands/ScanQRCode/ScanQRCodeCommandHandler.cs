using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using System.Text.Json;

namespace MagicCarRepairAISupported.Application.Features.QRCode.Commands.ScanQRCode
{
    public class ScanQRCodeCommandHandler : IRequestHandler<ScanQRCodeCommand, IDataResult<ScanQRCodeResponse>>
    {
        private readonly ITenantService _tenantService;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ICustomerRepository _customerRepository;

        public ScanQRCodeCommandHandler(
            ITenantService tenantService,
            IWorkOrderRepository workOrderRepository,
            IVehicleRepository vehicleRepository,
            ICustomerRepository customerRepository)
        {
            _tenantService = tenantService;
            _workOrderRepository = workOrderRepository;
            _vehicleRepository = vehicleRepository;
            _vehicleRepository = vehicleRepository;
            _customerRepository = customerRepository;
        }

        public async Task<IDataResult<ScanQRCodeResponse>> Handle(ScanQRCodeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId() ?? 1;

                // QR kod verisini parse et
                QRCodeData? qrData;
                try
                {
                    qrData = JsonSerializer.Deserialize<QRCodeData>(request.QRCodeData);
                }
                catch
                {
                    return new ErrorDataResult<ScanQRCodeResponse>(
                        new ScanQRCodeResponse
                        {
                            IsValid = false,
                            ErrorMessage = "Geçersiz QR kod formatı."
                        },
                        "Geçersiz QR kod formatı."
                    );
                }

                if (qrData == null)
                {
                    return new ErrorDataResult<ScanQRCodeResponse>(
                        new ScanQRCodeResponse
                        {
                            IsValid = false,
                            ErrorMessage = "QR kod verisi boş."
                        },
                        "QR kod verisi boş."
                    );
                }

                // Client ID kontrolü
                if (qrData.ClientId != clientId)
                {
                    return new ErrorDataResult<ScanQRCodeResponse>(
                        new ScanQRCodeResponse
                        {
                            IsValid = false,
                            ErrorMessage = "Bu QR kod bu tamirhaneye ait değil."
                        },
                        "Bu QR kod bu tamirhaneye ait değil."
                    );
                }

                // Entity kontrolü
                bool entityExists = false;
                if (qrData.EntityId.HasValue)
                {
                    entityExists = qrData.Type switch
                    {
                        "WorkOrder" => await _workOrderRepository.GetByIdAsync(qrData.EntityId.Value, cancellationToken) != null,
                        "Vehicle" => await _vehicleRepository.GetByIdAsync(qrData.EntityId.Value, cancellationToken) != null,
                        "Customer" => await _customerRepository.GetByIdAsync(qrData.EntityId.Value, cancellationToken) != null,
                        _ => false
                    };
                }

                if (!entityExists && qrData.EntityId.HasValue)
                {
                    return new ErrorDataResult<ScanQRCodeResponse>(
                        new ScanQRCodeResponse
                        {
                            IsValid = false,
                            ErrorMessage = "QR kod ile ilişkili kayıt bulunamadı."
                        },
                        "QR kod ile ilişkili kayıt bulunamadı."
                    );
                }

                // QR kod tipini enum'a çevir
                var qrType = Enum.TryParse<QRCodeType>(qrData.Type, out var type) 
                    ? type 
                    : QRCodeType.QuickService;

                return new SuccessDataResult<ScanQRCodeResponse>(
                    new ScanQRCodeResponse
                    {
                        Type = qrType,
                        EntityId = qrData.EntityId,
                        Data = qrData.Data,
                        IsValid = true
                    },
                    "QR kod başarıyla okundu."
                );
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<ScanQRCodeResponse>(
                    new ScanQRCodeResponse
                    {
                        IsValid = false,
                        ErrorMessage = $"QR kod okunurken hata oluştu: {ex.Message}"
                    },
                    $"QR kod okunurken hata oluştu: {ex.Message}"
                );
            }
        }

        private class QRCodeData
        {
            public string Type { get; set; } = string.Empty;
            public string Data { get; set; } = string.Empty;
            public int? EntityId { get; set; }
            public int ClientId { get; set; }
            public DateTime Timestamp { get; set; }
        }
    }
}
