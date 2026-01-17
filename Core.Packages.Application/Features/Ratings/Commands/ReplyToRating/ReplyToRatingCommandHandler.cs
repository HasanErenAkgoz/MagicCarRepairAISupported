using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Ratings.Commands.ReplyToRating
{
    public class ReplyToRatingCommandHandler : IRequestHandler<ReplyToRatingCommand, ReplyToRatingResponse>
    {
        private readonly IServiceRatingRepository _serviceRatingRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public ReplyToRatingCommandHandler(
            IServiceRatingRepository serviceRatingRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _serviceRatingRepository = serviceRatingRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<ReplyToRatingResponse> Handle(ReplyToRatingCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            var rating = await _serviceRatingRepository.GetByIdAsync(request.RatingId);
            if (rating == null || rating.ClientId != clientId)
            {
                throw new DomainException("RATING_NOT_FOUND", new { RatingId = request.RatingId });
            }

            rating.ServiceReply = request.ServiceReply;
            rating.ServiceReplyDate = DateTime.UtcNow;

            _serviceRatingRepository.Update(rating);
            await _serviceRatingRepository.SaveChangesAsync();

            return _mapper.Map<ReplyToRatingResponse>(rating);
        }
    }
}
