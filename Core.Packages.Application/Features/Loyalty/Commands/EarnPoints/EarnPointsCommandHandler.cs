using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Loyalty.Commands.EarnPoints
{
    public class EarnPointsCommandHandler : IRequestHandler<EarnPointsCommand, IDataResult<EarnPointsResponse>>
    {
        private readonly ILoyaltyPointRepository _loyaltyPointRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITenantService _tenantService;
        private readonly IUnitOfWork _unitOfWork;

        public EarnPointsCommandHandler(
            ILoyaltyPointRepository loyaltyPointRepository,
            ICustomerRepository customerRepository,
            ITenantService tenantService,
            IUnitOfWork unitOfWork)
        {
            _loyaltyPointRepository = loyaltyPointRepository;
            _customerRepository = customerRepository;
            _tenantService = tenantService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<EarnPointsResponse>> Handle(EarnPointsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Müşteri kontrolü
                var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
                if (customer == null)
                {
                    return new ErrorDataResult<EarnPointsResponse>("Müşteri bulunamadı.");
                }

                var clientId = _tenantService.GetCurrentClientId() ?? customer.ClientId;

                // Puan son kullanma tarihi (varsayılan: 1 yıl)
                var expiryDate = request.ExpiryDate ?? DateTime.UtcNow.AddYears(1);

                // Loyalty point oluştur
                var loyaltyPoint = new LoyaltyPoint
                {
                    CustomerId = request.CustomerId,
                    Points = request.Points,
                    Type = LoyaltyPointType.Earned,
                    Description = request.Description,
                    WorkOrderId = request.WorkOrderId,
                    ExpiryDate = expiryDate,
                    ClientId = clientId
                };

                await _loyaltyPointRepository.AddAsync(loyaltyPoint, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // Toplam puanı hesapla
                var totalPoints = await _loyaltyPointRepository.GetTotalPointsByCustomerIdAsync(request.CustomerId, cancellationToken);

                return new SuccessDataResult<EarnPointsResponse>(
                    new EarnPointsResponse
                    {
                        LoyaltyPointId = loyaltyPoint.Id,
                        TotalPoints = totalPoints,
                        Message = $"{request.Points} puan kazandınız. Toplam puanınız: {totalPoints}"
                    },
                    "Puan başarıyla eklendi."
                );
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<EarnPointsResponse>($"Puan eklenirken hata oluştu: {ex.Message}");
            }
        }
    }
}
