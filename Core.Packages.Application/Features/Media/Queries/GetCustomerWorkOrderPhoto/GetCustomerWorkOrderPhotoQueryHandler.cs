using System.Security.Claims;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivateVehiclePhoto;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace MagicCarRepairAISupported.Application.Features.Media.Queries.GetCustomerWorkOrderPhoto;

public sealed class GetCustomerWorkOrderPhotoQueryHandler : IRequestHandler<GetCustomerWorkOrderPhotoQuery, MediaFile?>
{
    private readonly IWorkOrderRepository _workOrders;
    private readonly IEntityRepository<WorkOrderPhoto, int> _photos;
    private readonly ICustomerRepository _customers;
    private readonly ITenantService _tenants;
    private readonly IHttpContextAccessor _http;
    private readonly IFileStorageService _files;

    public GetCustomerWorkOrderPhotoQueryHandler(IWorkOrderRepository workOrders, IEntityRepository<WorkOrderPhoto, int> photos,
        ICustomerRepository customers, ITenantService tenants, IHttpContextAccessor http, IFileStorageService files)
        => (_workOrders, _photos, _customers, _tenants, _http, _files) = (workOrders, photos, customers, tenants, http, files);

    public async Task<MediaFile?> Handle(GetCustomerWorkOrderPhotoQuery request, CancellationToken cancellationToken)
    {
        var principal = _http.HttpContext?.User ?? throw new UnauthorizedAccessException("User not authenticated");
        var isSystemAdmin = principal.FindFirst("UserType")?.Value == "1";
        var workOrder = await _workOrders.GetByIdAsync(request.WorkOrderId, cancellationToken);
        var photo = await _photos.GetByIdAsync(request.PhotoId, cancellationToken);
        if (workOrder is null || photo is null || workOrder.CustomerId != request.CustomerId || photo.WorkOrderId != workOrder.Id || photo.ClientId != workOrder.ClientId)
            throw new DomainException("CUSTOMER_WORK_ORDER_PHOTO_NOT_FOUND", new { request.WorkOrderId, request.PhotoId });

        if (!isSystemAdmin)
            await VerifyCurrentCustomerOwnsWorkOrder(principal, request.CustomerId, workOrder.ClientId, cancellationToken);

        if (!StoredMediaPath.TryParse(photo.FilePath, out var container, out var name))
            throw new DomainException("CUSTOMER_WORK_ORDER_PHOTO_STORAGE_INVALID", new { request.WorkOrderId, request.PhotoId });
        var content = await _files.GetFileAsync(name, container);
        return content is null ? null : new MediaFile { Content = content, DownloadName = name, ContentType = StoredMediaPath.ContentType(name) };
    }

    private async Task VerifyCurrentCustomerOwnsWorkOrder(ClaimsPrincipal principal, int customerId, int workOrderClientId, CancellationToken cancellationToken)
    {
        var userIdValue = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdValue, out var userId)) throw new UnauthorizedAccessException("User not authenticated");
        var clientId = _tenants.GetCurrentClientId();
        if (!clientId.HasValue || clientId.Value != workOrderClientId) throw new UnauthorizedAccessException("Tenant mismatch");
        var customer = await _customers.GetByUserIdForTenantAsync(userId, clientId.Value, cancellationToken);
        if (customer is null || customer.Id != customerId) throw new UnauthorizedAccessException("Customer does not own this work order");
    }
}
