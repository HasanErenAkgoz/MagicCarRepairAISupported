using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.QuoteResponses.Commands.Submit
{
    public class SubmitQuoteResponseCommandHandler : IRequestHandler<SubmitQuoteResponseCommand, IDataResult<SubmitQuoteResponseResponse>>
    {
        private readonly IQuoteResponseRepository _quoteResponseRepository;
        private readonly IQuoteRequestRepository _quoteRequestRepository;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public SubmitQuoteResponseCommandHandler(
            IQuoteResponseRepository quoteResponseRepository,
            IQuoteRequestRepository quoteRequestRepository,
            INotificationService notificationService,
            IMapper mapper,
            ITenantService tenantService)
        {
            _quoteResponseRepository = quoteResponseRepository;
            _quoteRequestRepository = quoteRequestRepository;
            _notificationService = notificationService;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IDataResult<SubmitQuoteResponseResponse>> Handle(SubmitQuoteResponseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId() ?? 1;

                // QuoteRequest kontrolü
                var quoteRequest = await _quoteRequestRepository.GetQuoteRequestDetailsAsync(request.QuoteRequestId, cancellationToken);
                if (quoteRequest == null)
                {
                    return new ErrorDataResult<SubmitQuoteResponseResponse>("Quote request not found");
                }

                // QuoteRequest'in açık ve teklif alabilir durumda olduğunu kontrol et
                if (!quoteRequest.CanAcceptQuotes())
                {
                    return new ErrorDataResult<SubmitQuoteResponseResponse>("Quote request is not open or expired");
                }

                // Bu client'ın daha önce teklif verip vermediğini kontrol et
                var hasSubmitted = await _quoteRequestRepository.HasClientSubmittedQuoteAsync(request.QuoteRequestId, clientId, cancellationToken);
                if (hasSubmitted)
                {
                    return new ErrorDataResult<SubmitQuoteResponseResponse>("You have already submitted a quote for this request");
                }

                // Kendi talebine teklif veremez
                if (quoteRequest.ClientId == clientId)
                {
                    return new ErrorDataResult<SubmitQuoteResponseResponse>("You cannot submit a quote for your own request");
                }

                // QuoteResponse oluştur
                var quoteResponse = new QuoteResponse
                {
                    QuoteRequestId = request.QuoteRequestId,
                    ClientId = clientId,
                    Description = request.Description,
                    EstimatedDays = request.EstimatedDays,
                    QuoteAmount = request.QuoteAmount,
                    DiscountRate = request.DiscountRate,
                    TaxRate = request.TaxRate,
                    WarrantyMonths = request.WarrantyMonths,
                    Status = QuoteResponseStatus.Pending.ToString(),
                    QuoteDate = DateTime.UtcNow,
                    ValidUntilDate = request.ValidUntilDate ?? DateTime.UtcNow.AddDays(30),
                    Notes = request.Notes
                };

                // Tutarları hesapla
                quoteResponse.CalculateAmounts();

                // QuoteResponse kaydet
                await _quoteResponseRepository.AddAsync(quoteResponse, cancellationToken);

                // QuoteNumber oluştur
                quoteResponse.GenerateQuoteNumber();
                _quoteResponseRepository.Update(quoteResponse);

                // QuoteRequest durumunu güncelle (Teklifler alındı)
                if (quoteRequest.Status == QuoteStatus.Open)
                {
                    quoteRequest.UpdateStatus(QuoteStatus.QuotesReceived);
                    _quoteRequestRepository.Update(quoteRequest);
                }

                // Müşteriye bildirim gönder (Email/SMS)
                if (quoteRequest.CustomerId > 0)
                {
                    var customer = quoteRequest.Customer;
                    if (customer != null)
                    {
                        var notificationTitle = "Yeni Teklif Alındı";
                        var notificationContent = $"'{quoteRequest.RequestNumber}' numaralı talebinize yeni bir teklif geldi. Detayları görüntülemek için sisteme giriş yapabilirsiniz.";
                        
                        // Email bildirimi
                        if (!string.IsNullOrEmpty(customer.Email))
                        {
                            await _notificationService.SendEmailNotificationAsync(
                                customer.UserId,
                                customer.Email,
                                notificationTitle,
                                notificationContent,
                                "QuoteRequest",
                                quoteRequest.Id);
                        }

                        // SMS bildirimi
                        if (!string.IsNullOrEmpty(customer.PhoneNumber))
                        {
                            await _notificationService.SendSmsNotificationAsync(
                                customer.UserId,
                                customer.PhoneNumber,
                                $"Yeni teklif: {quoteRequest.RequestNumber}. Detaylar için sisteme giriş yapın.",
                                "QuoteRequest",
                                quoteRequest.Id);
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(quoteRequest.CustomerEmail) || !string.IsNullOrEmpty(quoteRequest.CustomerPhone))
                {
                    // Misafir kullanıcı için bildirim
                    var notificationTitle = "Yeni Teklif Alındı";
                    var notificationContent = $"'{quoteRequest.RequestNumber}' numaralı talebinize yeni bir teklif geldi.";

                    if (!string.IsNullOrEmpty(quoteRequest.CustomerEmail))
                    {
                        await _notificationService.SendEmailNotificationAsync(
                            null,
                            quoteRequest.CustomerEmail,
                            notificationTitle,
                            notificationContent,
                            "QuoteRequest",
                            quoteRequest.Id);
                    }

                    if (!string.IsNullOrEmpty(quoteRequest.CustomerPhone))
                    {
                        await _notificationService.SendSmsNotificationAsync(
                            null,
                            quoteRequest.CustomerPhone,
                            $"Yeni teklif: {quoteRequest.RequestNumber}",
                            "QuoteRequest",
                            quoteRequest.Id);
                    }
                }

                var response = _mapper.Map<SubmitQuoteResponseResponse>(quoteResponse);
                return new SuccessDataResult<SubmitQuoteResponseResponse>(response, "Quote submitted successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<SubmitQuoteResponseResponse>(ex.Message);
            }
        }
    }
}
