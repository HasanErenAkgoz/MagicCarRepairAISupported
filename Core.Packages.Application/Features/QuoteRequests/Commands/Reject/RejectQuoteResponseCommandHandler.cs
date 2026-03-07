using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.QuoteRequests.Commands.Reject
{
    public class RejectQuoteResponseCommandHandler : IRequestHandler<RejectQuoteResponseCommand, IResult>
    {
        private readonly IQuoteRequestRepository _quoteRequestRepository;
        private readonly IQuoteResponseRepository _quoteResponseRepository;
        private readonly IEntityRepository<Customer, int> _customerRepository;
        private readonly ITenantService _tenantService;

        public RejectQuoteResponseCommandHandler(
            IQuoteRequestRepository quoteRequestRepository,
            IQuoteResponseRepository quoteResponseRepository,
            IEntityRepository<Customer, int> customerRepository,
            ITenantService tenantService)
        {
            _quoteRequestRepository = quoteRequestRepository;
            _quoteResponseRepository = quoteResponseRepository;
            _customerRepository = customerRepository;
            _tenantService = tenantService;
        }

        public async Task<IResult> Handle(RejectQuoteResponseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId() ?? 1;

                // QuoteRequest kontrolü
                var quoteRequest = await _quoteRequestRepository.GetQuoteRequestDetailsAsync(request.QuoteRequestId, cancellationToken);
                if (quoteRequest == null)
                {
                    return new ErrorResult("Quote request not found");
                }

                // Müşteri kontrolü - sadece talep sahibi reddedebilir
                if (quoteRequest.CustomerId > 0)
                {
                    var customer = await _customerRepository.GetByIdAsync(quoteRequest.CustomerId);
                    if (customer == null || customer.ClientId != clientId)
                    {
                        return new ErrorResult("You are not authorized to reject this quote");
                    }
                }

                // QuoteResponse kontrolü
                var quoteResponse = await _quoteResponseRepository.GetQuoteResponseDetailsAsync(request.QuoteResponseId, cancellationToken);
                if (quoteResponse == null || quoteResponse.QuoteRequestId != request.QuoteRequestId)
                {
                    return new ErrorResult("Quote response not found or does not belong to this request");
                }

                // Teklifi reddet
                quoteResponse.Reject(request.Reason);
                _quoteResponseRepository.Update(quoteResponse);

                return new SuccessResult("Quote rejected successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }
    }
}
