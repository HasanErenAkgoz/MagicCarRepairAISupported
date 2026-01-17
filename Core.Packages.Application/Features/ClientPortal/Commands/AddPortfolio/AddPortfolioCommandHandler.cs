using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;
using System.Text.Json;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.AddPortfolio
{
    public class AddPortfolioCommandHandler : IRequestHandler<AddPortfolioCommand, AddPortfolioResponse>
    {
        private readonly IServicePortfolioRepository _portfolioRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;
        private readonly ISignalRNotificationService _signalRNotificationService;
        private readonly IUnitOfWork _unitOfWork;

        public AddPortfolioCommandHandler(
            IServicePortfolioRepository portfolioRepository,
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService,
            ISignalRNotificationService signalRNotificationService,
            IUnitOfWork unitOfWork)
        {
            _portfolioRepository = portfolioRepository;
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
            _signalRNotificationService = signalRNotificationService;
            _unitOfWork = unitOfWork;
        }

        public async Task<AddPortfolioResponse> Handle(AddPortfolioCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            // WorkOrder kontrolü
            var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId);
            if (workOrder == null || workOrder.ClientId != clientId)
            {
                throw new DomainException("WORK_ORDER_NOT_FOUND");
            }

            // WorkOrder'ın teslim edilmiş olması gerekiyor
            if (workOrder.Status != WorkOrderStatus.Delivered)
            {
                throw new DomainException("WORK_ORDER_MUST_BE_DELIVERED");
            }

            // Zaten portföyde var mı kontrol et
            var existingPortfolio = await _portfolioRepository.GetByWorkOrderIdAsync(request.WorkOrderId, cancellationToken);
            if (existingPortfolio != null)
            {
                throw new DomainException("PORTFOLIO_ALREADY_EXISTS");
            }

            // FeaturedPhotoIds'i JSON formatına çevir
            var featuredPhotoIdsJson = string.Empty;
            if (request.FeaturedPhotoIds != null && request.FeaturedPhotoIds.Any())
            {
                featuredPhotoIdsJson = JsonSerializer.Serialize(request.FeaturedPhotoIds);
            }

            var portfolio = new ServicePortfolio
            {
                WorkOrderId = request.WorkOrderId,
                Title = request.Title,
                Description = request.Description,
                Categories = request.Categories,
                FeaturedPhotoIds = featuredPhotoIdsJson,
                DisplayOrder = request.DisplayOrder,
                IsPublished = false, // Müşteri onayından sonra yayınlanacak
                CustomerApprovalStatus = request.RequestCustomerApproval 
                    ? CustomerApprovalStatus.Pending 
                    : CustomerApprovalStatus.Approved,
                ClientId = clientId
            };

            await _portfolioRepository.AddAsync(portfolio, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Müşteriye onay bildirimi gönder
            if (request.RequestCustomerApproval)
            {
                await _signalRNotificationService.SendNotificationToUserAsync(
                    workOrder.Customer.UserId ?? 0,
                    "Portföy Onayı",
                    $"İş emriniz (#{workOrder.WorkOrderNumber}) portföyümüzde sergilenmek için onayınızı bekliyor.",
                    "Portfolio",
                    portfolio.Id);
            }

            return new AddPortfolioResponse
            {
                Id = portfolio.Id,
                WorkOrderId = portfolio.WorkOrderId,
                Title = portfolio.Title,
                RequiresCustomerApproval = request.RequestCustomerApproval
            };
        }
    }
}
