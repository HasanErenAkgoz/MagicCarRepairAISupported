using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Commands.AddWorkOrderPhoto
{
    public class AddWorkOrderPhotoCommandHandler : IRequestHandler<AddWorkOrderPhotoCommand, AddWorkOrderPhotoResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IEntityRepository<WorkOrderPhoto, int> _workOrderPhotoRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITenantService _tenantService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddWorkOrderPhotoCommandHandler(
            IWorkOrderRepository workOrderRepository,
            IEntityRepository<WorkOrderPhoto, int> workOrderPhotoRepository,
            ICustomerRepository customerRepository,
            ITenantService tenantService,
            IHttpContextAccessor httpContextAccessor)
        {
            _workOrderRepository = workOrderRepository;
            _workOrderPhotoRepository = workOrderPhotoRepository;
            _customerRepository = customerRepository;
            _tenantService = tenantService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AddWorkOrderPhotoResponse> Handle(AddWorkOrderPhotoCommand request, CancellationToken cancellationToken)
        {
            // Get current user ID from HttpContext
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("User not authenticated");
            }

            var clientId = _tenantService.GetCurrentClientId();
            if (!clientId.HasValue)
            {
                throw new UnauthorizedAccessException("Client ID not found");
            }

            // Find customer by UserId
            var currentCustomer = await _customerRepository.GetByUserIdForTenantAsync(
                userId, clientId.Value, cancellationToken);
            
            if (currentCustomer == null)
            {
                throw new UnauthorizedAccessException("Customer not found");
            }

            // WorkOrder'ı getir
            var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId, cancellationToken);
            if (workOrder == null)
            {
                throw new DomainException("WORKORDER_NOT_FOUND", new { WorkOrderId = request.WorkOrderId });
            }

            // Müşteri kontrolü - WorkOrder'ın CustomerId'si ile mevcut kullanıcının CustomerId'si eşleşmeli
            if (workOrder.CustomerId != currentCustomer.Id)
            {
                throw new UnauthorizedAccessException("You don't have permission to add photos to this work order");
            }

            // WorkOrderPhoto oluştur
            var photo = new WorkOrderPhoto
            {
                WorkOrderId = request.WorkOrderId,
                FilePath = request.FilePath,
                UploadedFileId = request.UploadedFileId,
                Description = request.Description ?? "Müşteri tarafından yüklenen fotoğraf",
                PhotoType = request.PhotoType,
                UploadDate = DateTime.UtcNow
            };

            // Photo'yu ekle
            await _workOrderPhotoRepository.AddAsync(photo, cancellationToken);

            return new AddWorkOrderPhotoResponse
            {
                PhotoId = photo.Id,
                WorkOrderId = workOrder.Id,
                FilePath = photo.FilePath
            };
        }
    }
}
