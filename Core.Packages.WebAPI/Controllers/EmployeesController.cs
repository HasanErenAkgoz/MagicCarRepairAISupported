using MagicCarRepairAISupported.Application.Features.Employees.Commands.CreateEmployee;
using MagicCarRepairAISupported.Application.Features.Employees.Commands.DeleteAllEmployees;
using MagicCarRepairAISupported.Application.Features.Employees.Commands.DeleteEmployee;
using MagicCarRepairAISupported.Application.Features.Employees.Commands.DeleteEmployeePermanently;
using MagicCarRepairAISupported.Application.Features.Employees.Commands.UpdateEmployee;
using MagicCarRepairAISupported.Application.Features.Employees.Queries.GetAllEmployees;
using MagicCarRepairAISupported.Application.Features.Employees.Queries.GetEmployeeById;
using MagicCarRepairAISupported.Application.Features.Employees.Queries.GetEmployeesByPosition;
using MagicCarRepairAISupported.Application.Features.Employees.Queries.GenerateQrCode;
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
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Tüm personelleri listele
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] EmploymentStatus? employmentStatus,
            [FromQuery] EmployeePosition? position,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            var query = new GetAllEmployeesQuery
            {
                EmploymentStatus = employmentStatus,
                Position = position,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Personeli ID'ye göre getir
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetEmployeeByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Pozisyona göre personelleri getir
        /// </summary>
        [HttpGet("by-position/{position}")]
        public async Task<IActionResult> GetByPosition(EmployeePosition position)
        {
            var query = new GetEmployeesByPositionQuery { Position = position };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Yeni personel oluştur
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Personel bilgilerini güncelle
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Tüm çalışanları kalıcı olarak sil — Sadece SystemAdmin
        /// </summary>
        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAll([FromQuery] int clientId)
        {
            var userTypeClaim = User.FindFirst("UserType")?.Value;
            if (userTypeClaim != "1")
                return Forbid();

            var result = await _mediator.Send(new DeleteAllEmployeesCommand { ClientId = clientId });
            return Ok(result);
        }

        /// <summary>
        /// Personeli sil (Soft Delete)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteEmployeeCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Personeli kalıcı olarak sil (Hard Delete) — SystemAdmin ve Manager
        /// </summary>
        [HttpDelete("{id}/permanent")]
        public async Task<IActionResult> DeletePermanent(int id)
        {
            var userTypeClaim = User.FindFirst("UserType")?.Value;
            if (userTypeClaim != "1" && userTypeClaim != "2")
                return Forbid();

            var command = new DeleteEmployeePermanentlyCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Personel QR kodu oluştur (kimlik kartı için)
        /// </summary>
        [HttpGet("{id}/qr-code")]
        public async Task<IActionResult> GenerateQrCode(int id)
        {
            var query = new GenerateEmployeeQrCodeQuery { EmployeeId = id };
            var qrBytes = await _mediator.Send(query);
            return File(qrBytes, "image/png", $"Employee-{id}-QR.png");
        }
    }
}

