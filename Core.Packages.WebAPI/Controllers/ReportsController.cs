using MagicCarRepairAISupported.Application.Features.Reports.Queries.CustomerAnalytics;
using MagicCarRepairAISupported.Application.Features.Reports.Queries.FinancialCharts;
using MagicCarRepairAISupported.Application.Features.Reports.Queries.PartUsageReport;
using MagicCarRepairAISupported.Application.Features.Reports.Queries.WorkOrderStatistics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using MagicCarRepairAISupported.WebAPI.Authorization;
using MagicCarRepairAISupported.WebAPI.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = AuthPolicyNames.ShopStaff)]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// İş emri istatistikleri raporu
        /// </summary>
        [HttpGet("workorder-statistics")]
        public async Task<IActionResult> GetWorkOrderStatistics(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] int? employeeId,
            [FromQuery] bool? includeCompletedOnly)
        {
            var query = new GetWorkOrderStatisticsQuery
            {
                StartDate = startDate,
                EndDate = endDate,
                EmployeeId = employeeId,
                IncludeCompletedOnly = includeCompletedOnly ?? false
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Parça kullanım raporu
        /// </summary>
        [HttpGet("part-usage")]
        public async Task<IActionResult> GetPartUsageReport(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] int? partId,
            [FromQuery] int? categoryId,
            [FromQuery] int? topN)
        {
            var query = new GetPartUsageReportQuery
            {
                StartDate = startDate,
                EndDate = endDate,
                PartId = partId,
                CategoryId = categoryId,
                TopN = topN ?? 10
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Müşteri analitikleri raporu
        /// </summary>
        [HttpGet("customer-analytics")]
        public async Task<IActionResult> GetCustomerAnalytics(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] int? customerId)
        {
            var query = new GetCustomerAnalyticsQuery
            {
                StartDate = startDate,
                EndDate = endDate,
                CustomerId = customerId
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Finansal grafikler raporu
        /// </summary>
        [HttpGet("financial-charts")]
        public async Task<IActionResult> GetFinancialCharts(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] string? groupBy)
        {
            var query = new GetFinancialChartsQuery
            {
                StartDate = startDate,
                EndDate = endDate,
                GroupBy = groupBy ?? "month"
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}

