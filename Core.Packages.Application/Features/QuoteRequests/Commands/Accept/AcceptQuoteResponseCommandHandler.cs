using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.QuoteRequests.Commands.Accept
{
    public class AcceptQuoteResponseCommandHandler : IRequestHandler<AcceptQuoteResponseCommand, IDataResult<AcceptQuoteResponseResponse>>
    {
        private readonly IQuoteRequestRepository _quoteRequestRepository;
        private readonly IQuoteResponseRepository _quoteResponseRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IEntityRepository<Vehicle, int> _vehicleRepository;
        private readonly IEntityRepository<Customer, int> _customerRepository;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public AcceptQuoteResponseCommandHandler(
            IQuoteRequestRepository quoteRequestRepository,
            IQuoteResponseRepository quoteResponseRepository,
            IWorkOrderRepository workOrderRepository,
            IEntityRepository<Vehicle, int> vehicleRepository,
            IEntityRepository<Customer, int> customerRepository,
            INotificationService notificationService,
            IMapper mapper,
            ITenantService tenantService)
        {
            _quoteRequestRepository = quoteRequestRepository;
            _quoteResponseRepository = quoteResponseRepository;
            _workOrderRepository = workOrderRepository;
            _vehicleRepository = vehicleRepository;
            _customerRepository = customerRepository;
            _notificationService = notificationService;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IDataResult<AcceptQuoteResponseResponse>> Handle(AcceptQuoteResponseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId() ?? 1;

                // QuoteRequest kontrolü
                var quoteRequest = await _quoteRequestRepository.GetQuoteRequestDetailsAsync(request.QuoteRequestId, cancellationToken);
                if (quoteRequest == null)
                {
                    return new ErrorDataResult<AcceptQuoteResponseResponse>("Quote request not found");
                }

                // Müşteri kontrolü - sadece talep sahibi kabul edebilir
                if (quoteRequest.CustomerId.HasValue)
                {
                    var customer = await _customerRepository.GetByIdAsync(quoteRequest.CustomerId.Value);
                    if (customer == null || customer.ClientId != clientId)
                    {
                        return new ErrorDataResult<AcceptQuoteResponseResponse>("You are not authorized to accept this quote");
                    }
                }

                // QuoteResponse kontrolü
                var quoteResponse = await _quoteResponseRepository.GetQuoteResponseDetailsAsync(request.QuoteResponseId, cancellationToken);
                if (quoteResponse == null || quoteResponse.QuoteRequestId != request.QuoteRequestId)
                {
                    return new ErrorDataResult<AcceptQuoteResponseResponse>("Quote response not found or does not belong to this request");
                }

                // Teklif geçerli mi kontrol et
                if (!quoteResponse.IsValid())
                {
                    return new ErrorDataResult<AcceptQuoteResponseResponse>("Quote response is not valid or expired");
                }

                // Teklifi kabul et
                quoteResponse.Accept();
                _quoteResponseRepository.Update(quoteResponse);

                // QuoteRequest'i güncelle
                quoteRequest.SelectQuote(request.QuoteResponseId);
                _quoteRequestRepository.Update(quoteRequest);

                // Diğer teklifleri reddet
                var otherResponses = await _quoteResponseRepository.GetQuoteResponsesByRequestAsync(request.QuoteRequestId, cancellationToken);
                foreach (var otherResponse in otherResponses.Where(r => r.Id != request.QuoteResponseId))
                {
                    otherResponse.Reject("Another quote was accepted");
                    _quoteResponseRepository.Update(otherResponse);
                }

                // WorkOrder oluştur
                int? workOrderId = null;
                if (quoteRequest.VehicleId.HasValue && quoteRequest.CustomerId.HasValue)
                {
                    var vehicle = await _vehicleRepository.GetByIdAsync(quoteRequest.VehicleId.Value);
                    if (vehicle != null)
                    {
                        var workOrder = new WorkOrder
                        {
                            WorkOrderNumber = WorkOrder.GenerateWorkOrderNumber(),
                            VehicleId = quoteRequest.VehicleId.Value,
                            CustomerId = quoteRequest.CustomerId.Value,
                            EntryDate = DateTime.UtcNow,
                            EstimatedDeliveryDate = DateTime.UtcNow.AddDays(quoteResponse.EstimatedDays),
                            Priority = UrgencyLevelToWorkOrderPriority(quoteRequest.UrgencyLevel),
                            CustomerComplaints = quoteRequest.ProblemDescription,
                            TotalAmount = quoteResponse.NetAmount,
                            DiscountAmount = quoteResponse.DiscountAmount,
                            Status = WorkOrderStatus.VehicleEntered,
                            ClientId = quoteResponse.ClientId // Teklif veren servis
                        };

                        await _workOrderRepository.AddAsync(workOrder, cancellationToken);
                        workOrderId = workOrder.Id;
                    }
                }

                // Teklif veren servise bildirim gönder
                var client = quoteResponse.Client;
                if (client != null && !string.IsNullOrEmpty(client.ContactEmail))
                {
                    var notificationTitle = "Teklifiniz Kabul Edildi";
                    var notificationContent = $"'{quoteRequest.RequestNumber}' numaralı talebe verdiğiniz teklif kabul edildi. İş emri oluşturuldu.";
                    
                    await _notificationService.SendEmailNotificationAsync(
                        null,
                        client.ContactEmail,
                        notificationTitle,
                        notificationContent,
                        "QuoteResponse",
                        quoteResponse.Id,
                        new Dictionary<string, object> { { "WorkOrderId", workOrderId } });
                }

                var response = new AcceptQuoteResponseResponse
                {
                    QuoteRequestId = request.QuoteRequestId,
                    QuoteResponseId = request.QuoteResponseId,
                    Status = quoteRequest.Status,
                    AcceptedDate = DateTime.UtcNow,
                    WorkOrderId = workOrderId
                };

                return new SuccessDataResult<AcceptQuoteResponseResponse>(response, "Quote accepted successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<AcceptQuoteResponseResponse>(ex.Message);
            }
        }

        private WorkOrderPriority UrgencyLevelToWorkOrderPriority(UrgencyLevel urgencyLevel)
        {
            return urgencyLevel switch
            {
                UrgencyLevel.Low => WorkOrderPriority.Low,
                UrgencyLevel.Normal => WorkOrderPriority.Normal,
                UrgencyLevel.High => WorkOrderPriority.High,
                UrgencyLevel.Urgent => WorkOrderPriority.Urgent,
                _ => WorkOrderPriority.Normal
            };
        }
    }
}
