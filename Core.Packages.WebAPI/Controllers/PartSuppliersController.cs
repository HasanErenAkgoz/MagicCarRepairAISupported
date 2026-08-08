using MagicCarRepairAISupported.Application.Features.PartSuppliers.Commands.Create;
using MagicCarRepairAISupported.Application.Features.PartSuppliers.Commands.Delete;
using MagicCarRepairAISupported.Application.Features.PartSuppliers.Commands.Update;
using MagicCarRepairAISupported.Application.Features.PartSuppliers.Queries.GetAll;
using MagicCarRepairAISupported.Application.Features.PartSuppliers.Queries.GetById;
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
    public class PartSuppliersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PartSuppliersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Tüm tedarikçileri listele (Sayfalama, Arama, Filtreleme)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize,
            [FromQuery] string? searchTerm,
            [FromQuery] bool? isActiveOnly)
        {
            var query = new GetAllPartSuppliersQuery
            {
                PageNumber = pageNumber ?? 1,
                PageSize = pageSize ?? 20,
                SearchTerm = searchTerm,
                IsActiveOnly = isActiveOnly
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Tedarikçiyi ID'ye göre getir
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetPartSupplierByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Yeni tedarikçi oluştur
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePartSupplierCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Tedarikçi güncelle
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePartSupplierCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Tedarikçi sil (soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeletePartSupplierCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}

