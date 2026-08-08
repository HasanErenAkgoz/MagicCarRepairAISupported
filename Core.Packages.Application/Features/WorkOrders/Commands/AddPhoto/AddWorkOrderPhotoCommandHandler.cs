using MagicCarRepairAISupported.Application.Common.Messages;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddPhoto
{
    public class AddWorkOrderPhotoCommandHandler : IRequestHandler<AddWorkOrderPhotoCommand, AddWorkOrderPhotoResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IEntityRepository<WorkOrderPhoto, int> _workOrderPhotoRepository;
        private readonly IEntityRepository<Employee, int> _employeeRepository;
        private readonly ITenantService _tenantService;

        public AddWorkOrderPhotoCommandHandler(
            IWorkOrderRepository workOrderRepository,
            IEntityRepository<WorkOrderPhoto, int> workOrderPhotoRepository,
            IEntityRepository<Employee, int> employeeRepository,
            ITenantService tenantService)
        {
            _workOrderRepository = workOrderRepository;
            _workOrderPhotoRepository = workOrderPhotoRepository;
            _employeeRepository = employeeRepository;
            _tenantService = tenantService;
        }

        public async Task<AddWorkOrderPhotoResponse> Handle(AddWorkOrderPhotoCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            // WorkOrder kontrolü
            var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId);
            if (workOrder == null)
            {
                throw new DomainException(Messages.NotFound, new { Entity = "WorkOrder", Id = request.WorkOrderId });
            }

            if (workOrder.ClientId != clientId)
            {
                throw new DomainException("WORKORDER_NOT_BELONG_TO_CLIENT", new { WorkOrderId = request.WorkOrderId });
            }

            // Employee kontrolü (opsiyonel)
            if (request.UploadedByEmployeeId.HasValue)
            {
                var employee = await _employeeRepository.GetByIdAsync(request.UploadedByEmployeeId.Value);
                if (employee == null || employee.ClientId != clientId)
                {
                    throw new DomainException("EMPLOYEE_NOT_FOUND", new { EmployeeId = request.UploadedByEmployeeId.Value });
                }
            }

            // WorkOrderPhoto oluştur
            var photo = new WorkOrderPhoto
            {
                WorkOrderId = request.WorkOrderId,
                FilePath = request.FilePath,
                UploadedFileId = request.UploadedFileId,
                Description = request.Description,
                PhotoType = request.PhotoType,
                TimelineId = request.TimelineId,
                UploadedByEmployeeId = request.UploadedByEmployeeId,
                UploadDate = DateTime.UtcNow
            };

            // Photo'yu ekle
            await _workOrderPhotoRepository.AddAsync(photo, cancellationToken);

            return new AddWorkOrderPhotoResponse
            {
                PhotoId = photo.Id,
                WorkOrderId = workOrder.Id,
                MediaUrl = $"/api/media/work-orders/{workOrder.Id}/photos/{photo.Id}",
                FilePath = photo.FilePath
            };
        }
    }
}
