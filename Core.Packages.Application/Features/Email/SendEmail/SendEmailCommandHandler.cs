using MagicCarRepairAISupported.Application.Common.Messages;
using MagicCarRepairAISupported.Application.Common.Services.Auth;
using MagicCarRepairAISupported.Application.Common.Services.Email;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Email.SendEmail
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, IResult>
    {
        private readonly IEmailService _emailService;
        private readonly IAuthenticationService _authenticationService;
        public SendEmailCommandHandler(IEmailService emailService, IAuthenticationService authenticationService)
        {
            _emailService = emailService;
            _authenticationService = authenticationService;
        }

        public async Task<IResult> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
             _authenticationService.EnsurePermissionForHandler<SendEmailCommandHandler>();
            try
            {
                await _emailService.SendEmailAsync(request.To, request.Subject, request.Body);
                return new SuccessResult(Messages.SendEmail);
            }
                catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }
    }

}
