using MagicCarRepairAISupported.Application.Features.Parts.Commands.AddPhoto;
using MagicCarRepairAISupported.Application.Features.Parts.Commands.CreatePart;
using MagicCarRepairAISupported.Application.Features.Parts.Commands.DeletePart;
using MagicCarRepairAISupported.Application.Features.Parts.Commands.DeletePhoto;
using MagicCarRepairAISupported.Application.Features.Parts.Commands.Import;
using MagicCarRepairAISupported.Application.Features.Parts.Commands.UpdatePart;
using MagicCarRepairAISupported.Application.Features.Parts.Commands.RepairUtf8Mojibake;
using MagicCarRepairAISupported.Application.Features.Parts.Commands.UpdatePartStock;
using MagicCarRepairAISupported.Application.Features.Parts.Queries.Export;
using MagicCarRepairAISupported.Application.Features.Parts.Queries.GetAllParts;
using MagicCarRepairAISupported.Application.Features.Parts.Queries.GetImportTemplate;
using MagicCarRepairAISupported.Application.Features.Parts.Queries.GetLowStockParts;
using MagicCarRepairAISupported.Application.Features.Parts.Queries.GetPartById;
using MagicCarRepairAISupported.Application.Features.Parts.Queries.GenerateQrCode;
using MagicCarRepairAISupported.Application.Features.Parts.Commands.Barcode;
using MagicCarRepairAISupported.Application.Features.Parts.Commands.BulkDeleteParts;
using MagicCarRepairAISupported.Application.Features.Parts.Queries.Barcode;
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
    public class PartsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PartsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Tüm parçaları listele (Sayfalama, Arama, Filtreleme)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize,
            [FromQuery] string? searchTerm,
            [FromQuery] PartCategory? category,
            [FromQuery] PartBrandType? brandType,
            [FromQuery] bool? lowStockOnly)
        {
            var query = new GetAllPartsQuery
            {
                PageNumber = pageNumber ?? 1,
                PageSize = pageSize ?? 20,
                SearchTerm = searchTerm,
                Category = category,
                BrandType = brandType,
                LowStockOnly = lowStockOnly
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Parçayı ID'ye göre getir
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetPartByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Yeni parça oluştur
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePartCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Parça güncelle
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePartCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Parça sil (soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeletePartCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Parçaları toplu sil (soft delete).
        /// Aynı business rule geçerli: aktif iş emirlerinde kullanılan parçalar silinmez.
        /// </summary>
        [HttpPost("bulk-delete")]
        public async Task<IActionResult> BulkDelete([FromBody] BulkDeletePartsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Parça stok güncelle
        /// </summary>
        [HttpPatch("{id}/stock")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] UpdatePartStockCommand command)
        {
            if (id != command.PartId)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Kritik stok seviyesindeki parçaları listele
        /// </summary>
        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStock()
        {
            var query = new GetLowStockPartsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Parça QR kodu oluştur
        /// </summary>
        [HttpGet("{id}/qr-code")]
        public async Task<IActionResult> GenerateQrCode(int id)
        {
            var query = new GeneratePartQrCodeQuery { PartId = id };
            var qrBytes = await _mediator.Send(query);
            return File(qrBytes, "image/png", $"Part-{id}-QR.png");
        }

        /// <summary>
        /// Parçaları export et (Excel, CSV)
        /// </summary>
        [HttpGet("export")]
        public async Task<IActionResult> Export([FromQuery] ExportPartsQuery query)
        {
            var result = await _mediator.Send(query);
            var contentType = query.Format.ToLower() switch
            {
                "excel" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "csv" => "text/csv",
                _ => "application/octet-stream"
            };
            var fileName = $"Parts-{DateTime.UtcNow:yyyyMMddHHmmss}.{query.Format.ToLower()}";
            return File(result, contentType, fileName);
        }

        /// <summary>
        /// Parça import template'i indir
        /// </summary>
        [HttpGet("import/template")]
        public async Task<IActionResult> GetImportTemplate()
        {
            var query = new GetPartImportTemplateQuery();
            var template = await _mediator.Send(query);
            return File(template, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PartsImportTemplate.xlsx");
        }

        /// <summary>
        /// Parça / stok metinlerinde UTF-8 mojibake onarımı (ör. KÃ¶rÃ¼ → Körü). JWT'deki ClientId veya X-Client-Id kullanılır.
        /// SystemAdmin için: <c>?forClientId=1</c> ile hedef bayi (shop) seçilebilir.
        /// </summary>
        [HttpPost("repair-utf8-mojibake")]
        [AllowAnonymous]
        public async Task<IActionResult> RepairUtf8Mojibake([FromQuery] int? forClientId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new RepairUtf8MojibakeCommand { ForClientId = forClientId },
                cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Parçaları import et (Excel, CSV)
        /// </summary>
        [HttpPost("import")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Import(IFormFile file, [FromForm] string format = "Excel")
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is required");
            }

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var fileData = memoryStream.ToArray();

            var command = new ImportPartsCommand
            {
                FileData = fileData,
                FileName = file.FileName,
                Format = format
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Parça için barcode oluştur
        /// </summary>
        [HttpPost("{id}/barcode")]
        public async Task<IActionResult> GenerateBarcode(int id, [FromBody] GeneratePartBarcodeCommand? command = null)
        {
            command ??= new GeneratePartBarcodeCommand();
            command.PartId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Barcode görselini getir
        /// </summary>
        [HttpGet("{id}/barcode/image")]
        public async Task<IActionResult> GetBarcodeImage(int id, [FromQuery] BarcodeType? barcodeType = null)
        {
            var command = new GeneratePartBarcodeCommand
            {
                PartId = id,
                BarcodeType = barcodeType
            };
            var result = await _mediator.Send(command);
            return File(result.BarcodeImage, "image/png", $"Part-{id}-Barcode.png");
        }

        /// <summary>
        /// Barcode ile parça bul (Scanner için)
        /// </summary>
        [HttpPost("scan-barcode")]
        public async Task<IActionResult> ScanBarcode([FromBody] GetPartByBarcodeQuery query)
        {
            if (string.IsNullOrWhiteSpace(query.Barcode))
            {
                return BadRequest("Barcode is required");
            }

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Barcode ile parça bul (GET endpoint - URL'den barcode)
        /// </summary>
        [HttpGet("by-barcode/{barcode}")]
        public async Task<IActionResult> GetByBarcode(string barcode)
        {
            var query = new GetPartByBarcodeQuery { Barcode = barcode };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Parçaya fotoğraf ekle
        /// </summary>
        [HttpPost("{id}/photos")]
        public async Task<IActionResult> AddPhoto(int id, [FromBody] AddPartPhotoCommand command)
        {
            command.PartId = id;
            var result = await _mediator.Send(command);
            return Ok(new { success = true, data = result });
        }

        /// <summary>
        /// Parça fotoğrafını sil
        /// </summary>
        [HttpDelete("{id}/photos/{photoId}")]
        public async Task<IActionResult> DeletePhoto(int id, int photoId)
        {
            var command = new DeletePartPhotoCommand { PartId = id, PhotoId = photoId };
            var result = await _mediator.Send(command);
            if (!result.Success)
                return BadRequest(result);
            return Ok(new { success = true, message = "Fotoğraf silindi." });
        }
    }
}

