using MagicCarRepairAISupported.Application.Features.Accounting.Income.Commands.Create;
using MagicCarRepairAISupported.Application.Features.Accounting.Income.Commands.Delete;
using MagicCarRepairAISupported.Application.Features.Accounting.Income.Commands.Update;
using MagicCarRepairAISupported.Application.Features.Accounting.Income.Queries.GetAll;
using MagicCarRepairAISupported.Application.Features.Accounting.Income.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class IncomesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IncomesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Tüm gelirleri listele (Filtreleme ve sayfalama ile)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllIncomesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Geliri ID'ye göre getir
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetIncomeByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Yeni gelir ekle
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateIncomeCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Gelir bilgilerini güncelle
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateIncomeCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Geliri sil (Soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteIncomeCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}

