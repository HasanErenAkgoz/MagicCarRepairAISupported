using System.Security.Claims;
using MagicCarRepairAISupported.Application.Features.Reminders.Commands.CreateReminder;
using MagicCarRepairAISupported.Application.Features.Reminders.Queries.GetReminderById;
using MagicCarRepairAISupported.Application.Features.Reminders.Queries.GetReminders;
using MagicCarRepairAISupported.Domain.Entities;
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
    public class RemindersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RemindersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Hatirlatma olusturur
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateReminder([FromBody] CreateReminderCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Tum hatirlatmalari getirir (opsiyonel filtreler: userId, type, isSent)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetReminders(
            [FromQuery] int? userId,
            [FromQuery] ReminderType? type,
            [FromQuery] bool? isSent)
        {
            var query = new GetRemindersQuery
            {
                UserId = userId,
                Type = type,
                IsSent = isSent
            };

            var result = await _mediator.Send(query);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// Giris yapan kullanicinin hatirlatmalarini getirir
        /// </summary>
        [HttpGet("my")]
        public async Task<IActionResult> GetMyReminders(
            [FromQuery] ReminderType? type,
            [FromQuery] bool? isSent)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var query = new GetRemindersQuery
            {
                UserId = userId,
                Type = type,
                IsSent = isSent
            };

            var result = await _mediator.Send(query);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        /// ID ile hatirlatma getirir
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReminderById(int id)
        {
            var query = new GetReminderByIdQuery { Id = id };

            var result = await _mediator.Send(query);
            if (result.Success)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
