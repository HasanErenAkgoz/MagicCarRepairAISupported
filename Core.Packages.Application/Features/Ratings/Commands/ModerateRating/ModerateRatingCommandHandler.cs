using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Ratings.Commands.ModerateRating
{
    public class ModerateRatingCommandHandler : IRequestHandler<ModerateRatingCommand, ModerateRatingResponse>
    {
        private readonly IServiceRatingRepository _serviceRatingRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public ModerateRatingCommandHandler(
            IServiceRatingRepository serviceRatingRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _serviceRatingRepository = serviceRatingRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<ModerateRatingResponse> Handle(ModerateRatingCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            var rating = await _serviceRatingRepository.GetByIdAsync(request.RatingId);
            if (rating == null || rating.ClientId != clientId)
            {
                throw new DomainException("RATING_NOT_FOUND", new { RatingId = request.RatingId });
            }

            rating.Status = request.Status;

            if (request.Status == RatingStatus.Rejected && string.IsNullOrWhiteSpace(request.RejectionReason))
            {
                throw new DomainException("REJECTION_REASON_REQUIRED_FOR_REJECTION");
            }

            _serviceRatingRepository.Update(rating);
            await _serviceRatingRepository.SaveChangesAsync();

            return _mapper.Map<ModerateRatingResponse>(rating);
        }
    }
}
