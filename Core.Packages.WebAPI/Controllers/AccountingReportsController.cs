using MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetCashFlow;
using MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetExpenseReport;
using MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetIncomeReport;
using MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetMonthlySummary;
using MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetProfitLossReport;
using MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetTaxReport;
using MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetYearlySummary;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/accounting-reports")]
    [Authorize]
    public class AccountingReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountingReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gelir raporu getirir
        /// </summary>
        [HttpGet("income")]
        public async Task<IActionResult> GetIncomeReport([FromQuery] GetIncomeReportQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Gider raporu getirir
        /// </summary>
        [HttpGet("expense")]
        public async Task<IActionResult> GetExpenseReport([FromQuery] GetExpenseReportQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Kar-Zarar raporu getirir
        /// </summary>
        [HttpGet("profit-loss")]
        public async Task<IActionResult> GetProfitLossReport([FromQuery] GetProfitLossReportQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Aylık özet raporu getirir
        /// </summary>
        [HttpGet("monthly-summary")]
        public async Task<IActionResult> GetMonthlySummary([FromQuery] GetMonthlySummaryQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Yıllık özet raporu getirir
        /// </summary>
        [HttpGet("yearly-summary")]
        public async Task<IActionResult> GetYearlySummary([FromQuery] GetYearlySummaryQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Nakit akış raporu getirir
        /// </summary>
        [HttpGet("cash-flow")]
        public async Task<IActionResult> GetCashFlow([FromQuery] GetCashFlowQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Vergi raporu getirir
        /// </summary>
        [HttpGet("tax")]
        public async Task<IActionResult> GetTaxReport([FromQuery] GetTaxReportQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}

