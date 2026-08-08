using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Export;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.Export
{
    public class ExportWorkOrdersQueryHandler : IRequestHandler<ExportWorkOrdersQuery, byte[]>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;
        private readonly IExportService _exportService;

        public ExportWorkOrdersQueryHandler(
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService,
            IExportService exportService)
        {
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
            _exportService = exportService;
        }

        public async Task<byte[]> Handle(ExportWorkOrdersQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            var query = _workOrderRepository.Query()
                .Include(wo => wo.Customer)
                .Include(wo => wo.Vehicle)
                .Include(wo => wo.AssignedEmployee)
                .Where(wo => wo.ClientId == clientId);

            // Filtreleme
            if (request.StartDate.HasValue)
            {
                query = query.Where(wo => wo.EntryDate >= request.StartDate.Value);
            }

            if (request.EndDate.HasValue)
            {
                query = query.Where(wo => wo.EntryDate <= request.EndDate.Value);
            }

            if (request.Status.HasValue)
            {
                query = query.Where(wo => (int)wo.Status == request.Status.Value);
            }

            if (request.CustomerId.HasValue)
            {
                query = query.Where(wo => wo.CustomerId == request.CustomerId.Value);
            }

            if (request.EmployeeId.HasValue)
            {
                query = query.Where(wo => wo.AssignedEmployeeId == request.EmployeeId.Value);
            }

            var workOrders = await query
                .OrderByDescending(wo => wo.EntryDate)
                .ToListAsync(cancellationToken);

            // DTO'ya dönüştür
            var exportData = workOrders.Select(wo => new
            {
                WorkOrderNumber = wo.WorkOrderNumber,
                CustomerName = wo.Customer?.FullName ?? "N/A",
                VehicleInfo = wo.Vehicle != null ? $"{wo.Vehicle.Brand} {wo.Vehicle.Model} - {wo.Vehicle.LicensePlate}" : "N/A",
                EntryDate = wo.EntryDate.ToString("dd.MM.yyyy HH:mm"),
                EstimatedDeliveryDate = wo.EstimatedDeliveryDate?.ToString("dd.MM.yyyy") ?? "N/A",
                ActualDeliveryDate = wo.ActualDeliveryDate?.ToString("dd.MM.yyyy") ?? "N/A",
                Status = wo.Status.ToString(),
                Priority = wo.Priority.ToString(),
                Kilometers = wo.Kilometers?.ToString() ?? "N/A",
                FuelLevel = wo.FuelLevel?.ToString() ?? "N/A",
                SubTotal = wo.SubTotal.ToString("N2"),
                DiscountAmount = wo.DiscountAmount.ToString("N2"),
                TaxAmount = wo.TaxAmount.ToString("N2"),
                TotalAmount = wo.TotalAmount.ToString("N2"),
                PaidAmount = wo.PaidAmount.ToString("N2"),
                PaymentStatus = wo.PaymentStatus.ToString(),
                AssignedEmployee = wo.AssignedEmployee?.FullName ?? "N/A",
                Notes = wo.Notes ?? "N/A"
            }).ToList();

            // Format'a göre export et
            return request.Format.ToLower() switch
            {
                "excel" => await _exportService.ExportToExcelAsync(exportData, "WorkOrders", cancellationToken),
                "csv" => await _exportService.ExportToCsvAsync(exportData, cancellationToken),
                "pdf" => await _exportService.ExportToPdfAsync(exportData, "İş Emirleri", cancellationToken),
                _ => throw new NotSupportedException($"Format {request.Format} is not supported")
            };
        }
    }
}
