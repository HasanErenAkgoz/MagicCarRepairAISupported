using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Subscriptions.Commands.CancelSubscription;
using MagicCarRepairAISupported.Application.Features.Subscriptions.Commands.CreateSubscription;
using MagicCarRepairAISupported.Application.Features.Subscriptions.Commands.UpgradeSubscription;
using MagicCarRepairAISupported.Application.Features.Subscriptions.Queries.GetSubscription;
using MagicCarRepairAISupported.Application.Features.Subscriptions.Queries.GetSubscriptionPlans;
using Microsoft.AspNetCore.Authorization;
using MagicCarRepairAISupported.WebAPI.Authorization;
using MagicCarRepairAISupported.WebAPI.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Controllers;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = AuthPolicyNames.ShopStaff)]
    public class SubscriptionController : BaseApiController
    {
        private readonly ITenantService _tenantService;

        public SubscriptionController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        /// <summary>
        /// Mevcut abonelik bilgilerini getirir
        /// </summary>
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrent()
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 0;
            if (clientId == 0)
            {
                return BadRequest(new { success = false, message = "Client ID bulunamadı." });
            }

            var query = new GetSubscriptionQuery { ClientId = clientId };
            var result = await Mediator.Send(query);
            return GetResponse(result);
        }

        /// <summary>
        /// Tüm abonelik planlarını getirir
        /// </summary>
        [HttpGet("plans")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPlans()
        {
            var query = new GetSubscriptionPlansQuery();
            var result = await Mediator.Send(query);
            return GetResponse(result);
        }

        /// <summary>
        /// Yeni abonelik oluşturur
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateSubscriptionCommand command)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 0;
            if (clientId == 0)
            {
                return BadRequest(new { success = false, message = "Client ID bulunamadı." });
            }

            command.ClientId = clientId;
            var result = await Mediator.Send(command);
            return GetResponse(result);
        }

        /// <summary>
        /// Abonelik planını yükseltir
        /// </summary>
        [HttpPost("upgrade")]
        public async Task<IActionResult> Upgrade([FromBody] UpgradeSubscriptionCommand command)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 0;
            if (clientId == 0)
            {
                return BadRequest(new { success = false, message = "Client ID bulunamadı." });
            }

            command.ClientId = clientId;
            var result = await Mediator.Send(command);
            return GetResponse(result);
        }

        /// <summary>
        /// Aboneliği iptal eder
        /// </summary>
        [HttpPost("cancel")]
        public async Task<IActionResult> Cancel([FromBody] CancelSubscriptionCommand command)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 0;
            if (clientId == 0)
            {
                return BadRequest(new { success = false, message = "Client ID bulunamadı." });
            }

            command.ClientId = clientId;
            var result = await Mediator.Send(command);
            return GetResponse(result);
        }
    }
}
