using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Barcode;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.Barcode
{
    public class GeneratePartBarcodeCommandHandler : IRequestHandler<GeneratePartBarcodeCommand, GeneratePartBarcodeResponse>
    {
        private readonly IPartRepository _partRepository;
        private readonly IBarcodeService _barcodeService;
        private readonly ITenantService _tenantService;
        private readonly IUnitOfWork _unitOfWork;

        public GeneratePartBarcodeCommandHandler(
            IPartRepository partRepository,
            IBarcodeService barcodeService,
            ITenantService tenantService,
            IUnitOfWork unitOfWork)
        {
            _partRepository = partRepository;
            _barcodeService = barcodeService;
            _tenantService = tenantService;
            _unitOfWork = unitOfWork;
        }

        public async Task<GeneratePartBarcodeResponse> Handle(GeneratePartBarcodeCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            var part = await _partRepository.GetByIdAsync(request.PartId);
            if (part == null || part.ClientId != clientId)
            {
                throw new DomainException("PART_NOT_FOUND");
            }

            // Barcode oluştur (PartCode kullanarak)
            var barcodeData = part.PartCode;
            var barcodeType = request.BarcodeType.HasValue
                ? (Application.Common.Services.Barcode.BarcodeType)request.BarcodeType.Value
                : Application.Common.Services.Barcode.BarcodeType.Code128;

            // Barcode formatını doğrula
            if (!_barcodeService.ValidateBarcode(barcodeData, barcodeType))
            {
                // Eğer PartCode geçerli değilse, ID kullan
                barcodeData = $"PART-{part.Id}";
            }

            // Barcode görseli oluştur
            var barcodeImage = _barcodeService.GenerateBarcode(barcodeData, barcodeType);

            // Part entity'sine barcode kaydet
            part.Barcode = barcodeData;
            _partRepository.Update(part);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new GeneratePartBarcodeResponse
            {
                PartId = part.Id,
                PartCode = part.PartCode,
                Barcode = barcodeData,
                BarcodeType = barcodeType.ToString(),
                BarcodeImage = barcodeImage
            };
        }
    }
}
