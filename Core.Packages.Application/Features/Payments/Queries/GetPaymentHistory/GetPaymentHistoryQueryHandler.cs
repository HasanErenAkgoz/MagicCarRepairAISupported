using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Application.Features.Payments.Queries.GetPaymentHistory
{
    /// <summary>
    /// Ödeme geçmişi sorgu handler'ı
    /// </summary>
    public class GetPaymentHistoryQueryHandler : IRequestHandler<GetPaymentHistoryQuery, IDataResult<List<GetPaymentHistoryResponse>>>
    {
        private readonly IEntityRepository<Domain.Entities.Payment, int> _paymentRepository;
        private readonly ILogger<GetPaymentHistoryQueryHandler> _logger;

        public GetPaymentHistoryQueryHandler(
            IEntityRepository<Domain.Entities.Payment, int> paymentRepository,
            ILogger<GetPaymentHistoryQueryHandler> logger)
        {
            _paymentRepository = paymentRepository;
            _logger = logger;
        }

        public async Task<IDataResult<List<GetPaymentHistoryResponse>>> Handle(GetPaymentHistoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _paymentRepository.Query();

                if (request.CustomerId.HasValue)
                {
                    query = query.Where(p => p.CustomerId == request.CustomerId.Value);
                }

                if (request.InvoiceId.HasValue)
                {
                    query = query.Where(p => p.InvoiceId == request.InvoiceId.Value);
                }

                if (request.WorkOrderId.HasValue)
                {
                    query = query.Where(p => p.WorkOrderId == request.WorkOrderId.Value);
                }

                if (request.StartDate.HasValue)
                {
                    query = query.Where(p => p.PaymentDate >= request.StartDate.Value);
                }

                if (request.EndDate.HasValue)
                {
                    query = query.Where(p => p.PaymentDate <= request.EndDate.Value);
                }

                var payments = await query
                    .OrderByDescending(p => p.PaymentDate)
                    .ToListAsync(cancellationToken);

                var result = payments.Select(p => new GetPaymentHistoryResponse
                {
                    Id = p.Id,
                    PaymentNumber = p.PaymentNumber,
                    Amount = p.Amount,
                    PaymentMethod = p.PaymentMethod,
                    PaymentStatus = p.PaymentStatus,
                    PaymentGateway = p.PaymentGateway,
                    PaymentDate = p.PaymentDate,
                    InvoiceId = p.InvoiceId,
                    WorkOrderId = p.WorkOrderId,
                    CardLastFourDigits = p.CardLastFourDigits,
                    InstallmentCount = p.InstallmentCount,
                    IsRefunded = p.IsRefunded
                }).ToList();

                return new SuccessDataResult<List<GetPaymentHistoryResponse>>(result, "Ödeme geçmişi başarıyla getirildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ödeme geçmişi getirme hatası");
                return new ErrorDataResult<List<GetPaymentHistoryResponse>>("Ödeme geçmişi getirilirken bir hata oluştu");
            }
        }
    }
}





