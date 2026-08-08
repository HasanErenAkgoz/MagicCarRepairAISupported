using MagicCarRepairAISupported.Application.Features.Accounting.Tax.Commands.Create;
using MagicCarRepairAISupported.Application.Features.Accounting.Tax.Commands.Delete;
using MagicCarRepairAISupported.Application.Features.Accounting.Tax.Commands.PayTax;
using MagicCarRepairAISupported.Application.Features.Accounting.Tax.Commands.Update;
using MagicCarRepairAISupported.Application.Features.Accounting.Tax.Queries.GetAll;
using MagicCarRepairAISupported.Application.Features.Accounting.Tax.Queries.GetById;
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
    public class TaxesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaxesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Tüm vergileri listele (Filtreleme ve sayfalama ile)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllTaxesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Vergiyi ID'ye göre getir
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetTaxByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Yeni vergi ekle
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaxCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Vergi bilgilerini güncelle
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTaxCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Vergi ödemesi yap
        /// </summary>
        [HttpPost("{id}/pay")]
        public async Task<IActionResult> PayTax(int id, [FromBody] PayTaxCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Vergiyi sil (Soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteTaxCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
