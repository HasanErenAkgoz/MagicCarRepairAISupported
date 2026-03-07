using MagicCarRepairAISupported.Application.Features.Vehicles.Commands.AddPhoto;
using MagicCarRepairAISupported.Application.Features.Vehicles.Commands.Create;
using MagicCarRepairAISupported.Application.Features.Vehicles.Commands.Delete;
using MagicCarRepairAISupported.Application.Features.Vehicles.Commands.DeletePhoto;
using MagicCarRepairAISupported.Application.Features.Vehicles.Commands.Update;
using MagicCarRepairAISupported.Application.Features.Vehicles.Queries.GetAll;
using MagicCarRepairAISupported.Application.Features.Vehicles.Queries.GetById;
using MagicCarRepairAISupported.Application.Features.Vehicles.Queries.GetByCustomer;
using MagicCarRepairAISupported.Application.Features.Vehicles.Queries.GenerateQrCode;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VehiclesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehiclesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Tüm araçları listele (Filtreleme ve sayfalama ile)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int? customerId,
            [FromQuery] string? searchTerm,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            var query = new GetAllVehiclesQuery
            {
                CustomerId = customerId,
                SearchTerm = searchTerm,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Aracı ID'ye göre getir
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetVehicleByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Müşterinin araçlarını getir
        /// </summary>
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetByCustomer(int customerId)
        {
            var query = new GetVehiclesByCustomerQuery { CustomerId = customerId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Yeni araç ekle
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Araç bilgilerini güncelle
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateVehicleCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Aracı sil (Soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteVehicleCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Araç QR kodu oluştur
        /// </summary>
        [HttpGet("{id}/qr-code")]
        public async Task<IActionResult> GenerateQrCode(int id)
        {
            var query = new GenerateVehicleQrCodeQuery { VehicleId = id };
            var qrBytes = await _mediator.Send(query);
            return File(qrBytes, "image/png", $"Vehicle-{id}-QR.png");
        }

        /// <summary>
        /// Araca fotoğraf ekle
        /// </summary>
        [HttpPost("{id}/photos")]
        public async Task<IActionResult> AddPhoto(int id, [FromBody] AddVehiclePhotoCommand command)
        {
            command.VehicleId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Araç fotoğrafını sil
        /// </summary>
        [HttpDelete("{id}/photos/{photoId}")]
        public async Task<IActionResult> DeletePhoto(int id, int photoId)
        {
            var command = new DeleteVehiclePhotoCommand { VehicleId = id, PhotoId = photoId };
            var result = await _mediator.Send(command);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}

