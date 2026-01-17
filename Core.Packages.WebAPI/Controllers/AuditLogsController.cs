using MagicCarRepairAISupported.Application.Features.Audit.Queries.ExportAuditLogs;
using MagicCarRepairAISupported.Application.Features.Audit.Queries.GetAuditLogs;
using MagicCarRepairAISupported.Application.Features.Audit.Queries.GetDataChangeHistory;
using MagicCarRepairAISupported.Application.Features.Audit.Queries.GetUserActivity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuditLogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuditLogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Audit log kayıtlarını getirir
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAuditLogs([FromQuery] GetAuditLogsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Kullanıcı aktivitelerini getirir
        /// </summary>
        [HttpGet("user-activity")]
        public async Task<IActionResult> GetUserActivity([FromQuery] GetUserActivityQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Entity değişiklik geçmişini getirir
        /// </summary>
        [HttpGet("change-history")]
        public async Task<IActionResult> GetDataChangeHistory([FromQuery] GetDataChangeHistoryQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Audit log'ları export eder (Excel veya CSV)
        /// </summary>
        [HttpGet("export")]
        public async Task<IActionResult> ExportAuditLogs([FromQuery] ExportAuditLogsQuery query)
        {
            var result = await _mediator.Send(query);
            var contentType = query.Format == "CSV" ? "text/csv" : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = $"AuditLogs_{DateTime.UtcNow:yyyyMMddHHmmss}.{(query.Format == "CSV" ? "csv" : "xlsx")}";
            return File(result, contentType, fileName);
        }
    }
}
                    