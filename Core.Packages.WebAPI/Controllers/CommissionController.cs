using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Commission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommissionController : BaseApiController
    {
        private readonly ICommissionService _commissionService;
        private readonly ITenantService _tenantService;

        public CommissionController(
            ICommissionService commissionService,
            ITenantService tenantService)
        {
            _commissionService = commissionService;
            _tenantService = tenantService;
        }

        /// <summary>
        /// Toplam komisyonu getirir
        /// </summary>
        [HttpGet("total")]
        public async Task<IActionResult> GetTotal([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 0;
            if (clientId == 0)
            {
                return BadRequest(new { success = false, message = "Client ID bulunamadı." });
            }

            var total = await _commissionService.GetTotalCommissionsAsync(clientId, startDate, endDate);
            return Ok(new { success = true, data = total });
        }

        /// <summary>
        /// Komisyon raporu getirir
        /// </summary>
        [HttpGet("report")]
        public async Task<IActionResult> GetReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 0;
            if (clientId == 0)
            {
                return BadRequest(new { success = false, message = "Client ID bulunamadı." });
            }

            var report = await _commissionService.GetCommissionReportAsync(clientId, startDate, endDate);
            return Ok(new { success = true, data = report });
        }
    }
}
