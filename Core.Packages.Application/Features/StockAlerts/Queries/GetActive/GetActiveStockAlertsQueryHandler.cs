using MagicCarRepairAISupported.Application.Common.Services.Stock;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.StockAlerts.Queries.GetActive
{
    public class GetActiveStockAlertsQueryHandler : IRequestHandler<GetActiveStockAlertsQuery, IDataResult<List<GetActiveStockAlertsResponse>>>
    {
        private readonly IStockAlertService _stockAlertService;
        private readonly IEntityRepository<StockAlert, int> _stockAlertRepository;

        public GetActiveStockAlertsQueryHandler(
            IStockAlertService stockAlertService,
            IEntityRepository<StockAlert, int> stockAlertRepository)
        {
            _stockAlertService = stockAlertService;
            _stockAlertRepository = stockAlertRepository;
        }

        public async Task<IDataResult<List<GetActiveStockAlertsResponse>>> Handle(GetActiveStockAlertsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var alerts = await _stockAlertService.GetActiveAlertsAsync(cancellationToken);

                // Tip filtresi
                if (request.AlertType.HasValue)
                {
                    alerts = alerts.Where(a => a.AlertType == request.AlertType.Value).ToList();
                }

                // Pagination
                var pagedAlerts = alerts
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                var response = pagedAlerts.Select(a => new GetActiveStockAlertsResponse
                {
                    Id = a.Id,
                    PartId = a.PartId,
                    PartCode = a.Part?.PartCode ?? string.Empty,
                    PartName = a.Part?.Name ?? string.Empty,
                    AlertType = a.AlertType,
                    AlertTypeName = a.AlertType.ToString(),
                    CurrentStock = a.CurrentStock,
                    MinimumStock = a.MinimumStock,
                    RecommendedOrderQuantity = a.RecommendedOrderQuantity,
                    Message = a.Message,
                    FirstAlertDate = a.FirstAlertDate,
                    AutoOrderCreated = a.AutoOrderCreated,
                    AutoOrderId = a.AutoOrderId
                }).ToList();

                return new SuccessDataResult<List<GetActiveStockAlertsResponse>>(response);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<GetActiveStockAlertsResponse>>(ex.Message);
            }
        }
    }
}

