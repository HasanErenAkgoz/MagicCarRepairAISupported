using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Onboarding.Queries.GetOnboardingStatus
{
    public class GetOnboardingStatusQuery : IRequest<IDataResult<OnboardingStatusResponse>>
    {
        public int UserId { get; set; }
    }

    public class OnboardingStatusResponse
    {
        public bool HasCompletedOnboarding { get; set; }
        public List<string> CompletedSteps { get; set; } = new();
        public List<string> RemainingSteps { get; set; } = new();
    }

    public class GetOnboardingStatusQueryHandler : IRequestHandler<GetOnboardingStatusQuery, IDataResult<OnboardingStatusResponse>>
    {
        private readonly Domain.Repositories.EntityFrameworkCore.IUserRepository _userRepository;

        public GetOnboardingStatusQueryHandler(Domain.Repositories.EntityFrameworkCore.IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IDataResult<OnboardingStatusResponse>> Handle(GetOnboardingStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
                if (user == null)
                {
                    return new ErrorDataResult<OnboardingStatusResponse>("Kullanıcı bulunamadı.");
                }

                var response = new OnboardingStatusResponse
                {
                    HasCompletedOnboarding = user.HasCompletedOnboarding,
                    CompletedSteps = user.HasCompletedOnboarding 
                        ? new List<string> { "welcome", "dashboard", "workorders", "customers", "inventory" }
                        : new List<string>(),
                    RemainingSteps = user.HasCompletedOnboarding
                        ? new List<string>()
                        : new List<string> { "welcome", "dashboard", "workorders", "customers", "inventory" }
                };

                return new SuccessDataResult<OnboardingStatusResponse>(response);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<OnboardingStatusResponse>($"Onboarding durumu alınırken hata oluştu: {ex.Message}");
            }
        }
    }
}
