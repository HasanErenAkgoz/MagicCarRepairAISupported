using MagicCarRepairAISupported.Application.Features.Clients.Commands.CreateClient;
using MagicCarRepairAISupported.Application.Features.Clients.Queries.GetClientById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new client (tenant/service)
        /// </summary>
        [HttpPost]
        [Authorize] // Sadece admin kullanıcılar client oluşturabilir
        public async Task<IActionResult> CreateClient([FromBody] CreateClientCommand command)
        {
            var result = await _mediator.Send(command);
            
            if (!result.Success)
                return BadRequest(result);
            
            return Ok(result);
        }

        /// <summary>
        /// Get client by ID
        /// </summary>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetClientById(int id)
        {
            var result = await _mediator.Send(new GetClientByIdQuery { Id = id });
            
            if (!result.Success)
                return NotFound(result);
            
            return Ok(result);
        }
    }
}

