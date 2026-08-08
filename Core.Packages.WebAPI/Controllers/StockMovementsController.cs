using MagicCarRepairAISupported.Application.Features.StockMovements.Commands.RecordStockMovement;
using MagicCarRepairAISupported.Application.Features.StockMovements.Queries.GetStockMovementHistory;
using MagicCarRepairAISupported.Domain.Enums;
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
    public class StockMovementsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockMovementsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Stok hareketi kaydet (Giriş/Çıkış)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> RecordMovement([FromBody] RecordStockMovementCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Stok hareket geçmişini getir (Filtreleme + Pagination)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetHistory(
            [FromQuery] int? partId,
            [FromQuery] StockMovementType? movementType,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] int? employeeId,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            var query = new GetStockMovementHistoryQuery
            {
                PartId = partId,
                MovementType = movementType,
                StartDate = startDate,
                EndDate = endDate,
                EmployeeId = employeeId,
                PageNumber = pageNumber ?? 1,
                PageSize = pageSize ?? 20
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Belirli bir parça için stok hareket geçmişini getir
        /// </summary>
        [HttpGet("part/{partId}")]
        public async Task<IActionResult> GetHistoryByPart(
            int partId,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            var query = new GetStockMovementHistoryQuery
            {
                PartId = partId,
                PageNumber = pageNumber ?? 1,
                PageSize = pageSize ?? 20
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}

