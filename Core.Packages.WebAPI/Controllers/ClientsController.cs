using MagicCarRepairAISupported.Application.Features.Clients.Commands.ApproveClient;
using MagicCarRepairAISupported.Application.Features.Clients.Commands.CreateClient;
using MagicCarRepairAISupported.Application.Features.Clients.Commands.DeactivateClient;
using MagicCarRepairAISupported.Application.Features.Clients.Commands.DeleteClient;
using MagicCarRepairAISupported.Application.Features.Clients.Commands.UpdateClient;
using MagicCarRepairAISupported.Application.Features.Clients.Queries.GetAllClients;
using MagicCarRepairAISupported.Application.Features.Clients.Queries.GetClientById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using MagicCarRepairAISupported.WebAPI.Authorization;
using MagicCarRepairAISupported.WebAPI.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Tüm endpoint'ler login gerektiriyor
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Sadece giriş yapan kullanıcının UserType claim'inden SystemAdmin (1) olup olmadığını kontrol eder
        /// </summary>
        private bool IsSystemAdmin()
        {
            var userTypeClaim = User.FindFirst("UserType")?.Value;
            // UserType.SystemAdmin = 1
            return userTypeClaim == "1";
        }

        /// <summary>
        /// Get all clients — Sadece SystemAdmin
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllClients([FromQuery] bool? isActive = null)
        {
            if (!IsSystemAdmin())
                return Forbid(); // 403

            var query = new GetAllClientsQuery { IsActive = isActive };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get client by ID — Sadece SystemAdmin
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            if (!IsSystemAdmin())
                return Forbid();

            var result = await _mediator.Send(new GetClientByIdQuery { Id = id });

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        /// <summary>
        /// Create a new client — Sadece SystemAdmin
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientCommand command)
        {
            if (!IsSystemAdmin())
                return Forbid();

            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Update a client — Sadece SystemAdmin
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] UpdateClientCommand command)
        {
            if (!IsSystemAdmin())
                return Forbid();

            command.Id = id;
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Approve a client (shop) — Sadece SystemAdmin
        /// </summary>
        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApproveClient(int id)
        {
            if (!IsSystemAdmin())
                return Forbid(); // 403 — Manager/Employee/Customer erişemez

            var command = new ApproveClientCommand { ClientId = id };
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Deactivate (suspend) an active client — Sadece SystemAdmin
        /// </summary>
        [HttpPost("{id}/deactivate")]
        public async Task<IActionResult> DeactivateClient(int id)
        {
            if (!IsSystemAdmin())
                return Forbid();

            var command = new DeactivateClientCommand { ClientId = id };
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Permanently delete a client and all its data — Sadece SystemAdmin
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            if (!IsSystemAdmin())
                return Forbid();

            var command = new DeleteClientCommand { ClientId = id };
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
