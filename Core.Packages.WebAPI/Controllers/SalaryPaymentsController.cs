using MagicCarRepairAISupported.Application.Features.Accounting.SalaryPayment.Commands.Create;
using MagicCarRepairAISupported.Application.Features.Accounting.SalaryPayment.Commands.Delete;
using MagicCarRepairAISupported.Application.Features.Accounting.SalaryPayment.Commands.Update;
using MagicCarRepairAISupported.Application.Features.Accounting.SalaryPayment.Queries.GetAll;
using MagicCarRepairAISupported.Application.Features.Accounting.SalaryPayment.Queries.GetById;
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
    public class SalaryPaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalaryPaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Tüm maaş ödemelerini listele (Filtreleme ve sayfalama ile)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllSalaryPaymentsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Maaş ödemesini ID'ye göre getir
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetSalaryPaymentByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Yeni maaş ödemesi ekle
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSalaryPaymentCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Maaş ödemesi bilgilerini güncelle
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSalaryPaymentCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Maaş ödemesini sil (Soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteSalaryPaymentCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
