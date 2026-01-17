using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Commands.RejectPortfolio
{
    public class RejectPortfolioCommandHandler : IRequestHandler<RejectPortfolioCommand, IResult>
    {
        private readonly IServicePortfolioRepository _portfolioRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITenantService _tenantService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISignalRNotificationService _signalRNotificationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RejectPortfolioCommandHandler(
            IServicePortfolioRepository portfolioRepository,
            ICustomerRepository customerRepository,
            ITenantService tenantService,
            IUnitOfWork unitOfWork,
            ISignalRNotificationService signalRNotificationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _portfolioRepository = portfolioRepository;
            _customerRepository = customerRepository;
            _tenantService = tenantService;
            _unitOfWork = unitOfWork;
            _signalRNotificationService = signalRNotificationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IResult> Handle(RejectPortfolioCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            var portfolio = await _portfolioRepository.GetByIdAsync(request.PortfolioId);
            if (portfolio == null || portfolio.ClientId != clientId)
            {
                return new ErrorResult("PORTFOLIO_NOT_FOUND");
            }

            // Customer ID'yi HttpContext'ten al
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return new ErrorResult("USER_NOT_AUTHENTICATED");
            }

            var customers = await _customerRepository.GetListAsync(cancellationToken, c => c.UserId == userId && c.ClientId == clientId);
            var currentCustomer = customers.FirstOrDefault();
            if (currentCustomer == null)
            {
                return new ErrorResult("CUSTOMER_NOT_FOUND");
            }

            // WorkOrder'ın bu müşteriye ait olduğunu kontrol et
            if (portfolio.WorkOrder?.CustomerId != currentCustomer.Id)
            {
                return new ErrorResult("UNAUTHORIZED_PORTFOLIO_REJECTION");
            }

            // Red durumunu güncelle
            portfolio.CustomerApprovalStatus = CustomerApprovalStatus.Rejected;
            portfolio.CustomerApprovalDate = DateTime.UtcNow;
            portfolio.CustomerRejectionReason = request.RejectionReason;
            portfolio.IsPublished = false; // Yayından kaldır

            _portfolioRepository.Update(portfolio);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Tamirhaneye bildirim gönder
            await _signalRNotificationService.SendNotificationToClientAsync(
                clientId,
                "Portföy Reddi",
                $"Müşteri #{currentCustomer.Id}, iş emri #{portfolio.WorkOrder.WorkOrderNumber} için portföy onayını reddetti.{(string.IsNullOrEmpty(request.RejectionReason) ? "" : $" Sebep: {request.RejectionReason}")}",
                "Portfolio",
                portfolio.Id);

            return new SuccessResult("Portfolio rejected successfully");
        }
    }
}
