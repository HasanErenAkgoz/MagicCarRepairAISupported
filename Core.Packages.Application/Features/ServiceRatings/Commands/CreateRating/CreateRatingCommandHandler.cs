using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;
using System.Text.Json;

namespace MagicCarRepairAISupported.Application.Features.ServiceRatings.Commands.CreateRating
{
    public class CreateRatingCommandHandler : IRequestHandler<CreateRatingCommand, IDataResult<CreateRatingResponse>>
    {
        private readonly IServiceRatingRepository _ratingRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRatingCommandHandler(
            IServiceRatingRepository ratingRepository,
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService,
            IUnitOfWork unitOfWork)
        {
            _ratingRepository = ratingRepository;
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<CreateRatingResponse>> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // WorkOrder kontrolü
                var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId, cancellationToken);
                if (workOrder == null)
                {
                    return new ErrorDataResult<CreateRatingResponse>("İş emri bulunamadı.");
                }

                // Müşteri kontrolü
                if (workOrder.CustomerId != request.CustomerId)
                {
                    return new ErrorDataResult<CreateRatingResponse>("Bu iş emri için değerlendirme yapma yetkiniz yok.");
                }

                // Daha önce değerlendirme yapılmış mı kontrol et
                var existingRating = await _ratingRepository.GetListAsync(cancellationToken, r => r.WorkOrderId == request.WorkOrderId && r.CustomerId == request.CustomerId);
                var hasRating = existingRating.Any();
                
                if (hasRating)
                {
                    return new ErrorDataResult<CreateRatingResponse>("Bu iş emri için zaten değerlendirme yapılmış.");
                }

                var clientId = _tenantService.GetCurrentClientId() ?? workOrder.ClientId;

                // Rating entity oluştur
                var rating = new ServiceRating
                {
                    WorkOrderId = request.WorkOrderId,
                    CustomerId = request.CustomerId,
                    ClientId = clientId,
                    Rating = request.Rating,
                    ServiceQuality = request.ServiceQuality,
                    PriceValue = request.PriceValue,
                    OnTimeDelivery = request.OnTimeDelivery,
                    StaffBehavior = request.StaffBehavior,
                    Comment = request.Comment,
                    Photos = request.Photos != null && request.Photos.Any() 
                        ? JsonSerializer.Serialize(request.Photos) 
                        : null,
                    Status = RatingStatus.Pending // Admin onayı bekliyor
                };

                await _ratingRepository.AddAsync(rating, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new SuccessDataResult<CreateRatingResponse>(
                    new CreateRatingResponse
                    {
                        RatingId = rating.Id,
                        Message = "Değerlendirme başarıyla oluşturuldu. Onay bekleniyor."
                    },
                    "Değerlendirme başarıyla oluşturuldu."
                );
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CreateRatingResponse>($"Değerlendirme oluşturulurken hata oluştu: {ex.Message}");
            }
        }
    }
}
