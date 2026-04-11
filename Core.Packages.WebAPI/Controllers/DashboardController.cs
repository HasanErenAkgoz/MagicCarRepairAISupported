using MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetDashboardStats;
using MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetFleetStatus;
using MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetIncomeExpenseChart;
using MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetRecentActivities;
using MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetTodayRevenue;
using MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetTopCustomers;
using MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetWeeklyRevenue;
using MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetWorkOrderStatusChart;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Dashboard genel istatistiklerini getirir
        /// </summary>
        [HttpGet("stats")]
        public async Task<IActionResult> GetDashboardStats([FromQuery] GetDashboardStatsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Gelir-Gider grafik verilerini getirir
        /// </summary>
        [HttpGet("income-expense-chart")]
        public async Task<IActionResult> GetIncomeExpenseChart([FromQuery] GetIncomeExpenseChartQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// İş emri durum grafik verilerini getirir
        /// </summary>
        [HttpGet("workorder-status-chart")]
        public async Task<IActionResult> GetWorkOrderStatusChart()
        {
            var query = new GetWorkOrderStatusChartQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Son aktiviteleri getirir
        /// </summary>
        [HttpGet("recent-activities")]
        public async Task<IActionResult> GetRecentActivities([FromQuery] GetRecentActivitiesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// En çok harcama yapan müşterileri getirir
        /// </summary>
        [HttpGet("top-customers")]
        public async Task<IActionResult> GetTopCustomers([FromQuery] GetTopCustomersQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Bugünün gelir bilgilerini getirir (bugün, dün ve değişim yüzdesi)
        /// </summary>
        [HttpGet("today-revenue")]
        public async Task<IActionResult> GetTodayRevenue()
        {
            var query = new GetTodayRevenueQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Fleet status bilgilerini getirir (Repairing, Completed, Waiting, Efficiency)
        /// </summary>
        [HttpGet("fleet-status")]
        public async Task<IActionResult> GetFleetStatus()
        {
            var query = new GetFleetStatusQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Haftalık gelir analitik verilerini getirir
        /// </summary>
        [HttpGet("weekly-revenue")]
        public async Task<IActionResult> GetWeeklyRevenue([FromQuery] GetWeeklyRevenueQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}

