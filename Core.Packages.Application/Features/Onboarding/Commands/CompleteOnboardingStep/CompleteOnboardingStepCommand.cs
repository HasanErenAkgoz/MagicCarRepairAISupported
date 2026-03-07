using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Onboarding.Commands.CompleteOnboardingStep
{
    public class CompleteOnboardingStepCommand : IRequest<IResult>
    {
        public int UserId { get; set; }
        public string Step { get; set; } = string.Empty; // e.g., "welcome", "dashboard", "workorders", etc.
    }

    public class CompleteOnboardingStepCommandHandler : IRequestHandler<CompleteOnboardingStepCommand, IResult>
    {
        private readonly Domain.Repositories.EntityFrameworkCore.IUserRepository _userRepository;
        private readonly Domain.UnitOfWork.IUnitOfWork _unitOfWork;

        public CompleteOnboardingStepCommandHandler(
            Domain.Repositories.EntityFrameworkCore.IUserRepository userRepository,
            Domain.UnitOfWork.IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Handle(CompleteOnboardingStepCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
                if (user == null)
                {
                    return new ErrorResult("Kullanıcı bulunamadı.");
                }

                // Onboarding adımını tamamla
                // Basit implementasyon: Tüm adımlar tamamlandığında HasCompletedOnboarding = true
                // Daha gelişmiş versiyonda her adımı ayrı ayrı takip edebilirsiniz
                if (request.Step == "complete")
                {
                    user.HasCompletedOnboarding = true;
                    _userRepository.Update(user);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }

                return new SuccessResult("Onboarding adımı tamamlandı.");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Onboarding adımı tamamlanırken hata oluştu: {ex.Message}");
            }
        }
    }
}
