using MagicCarRepairAISupported.Application.Features.Customers.Commands.Create;
using MagicCarRepairAISupported.Application.Features.Customers.Commands.Delete;
using MagicCarRepairAISupported.Application.Features.Customers.Commands.Import;
using MagicCarRepairAISupported.Application.Features.Customers.Commands.Update;
using MagicCarRepairAISupported.Application.Features.Customers.Queries.Export;
using MagicCarRepairAISupported.Application.Features.Customers.Queries.GetAll;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Customers.Queries.GetById;
using MagicCarRepairAISupported.Application.Features.Customers.Queries.GetImportTemplate;
using MagicCarRepairAISupported.WebAPI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using MagicCarRepairAISupported.WebAPI.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = AuthPolicyNames.ShopStaff)]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDomainErrorResponseWriter _domainErrorResponseWriter;

        public CustomersController(IMediator mediator, IDomainErrorResponseWriter domainErrorResponseWriter)
        {
            _mediator = mediator;
            _domainErrorResponseWriter = domainErrorResponseWriter;
        }

        /// <summary>
        /// Tüm müşterileri listele (Arama ve sayfalama ile)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? searchTerm,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            var query = new GetAllCustomersQuery
            {
                SearchTerm = searchTerm,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Müşteriyi ID'ye göre getir
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetCustomerByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return await result.ToActionResultAsync(_domainErrorResponseWriter, HttpContext);
        }

        /// <summary>
        /// Yeni müşteri ekle
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Müşteri bilgilerini güncelle
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Müşteriyi sil (Soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteCustomerCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Müşterileri export et (Excel, CSV)
        /// </summary>
        [HttpGet("export")]
        public async Task<IActionResult> Export([FromQuery] ExportCustomersQuery query)
        {
            var result = await _mediator.Send(query);
            var contentType = query.Format.ToLower() switch
            {
                "excel" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "csv" => "text/csv",
                _ => "application/octet-stream"
            };
            var fileName = $"Customers-{DateTime.UtcNow:yyyyMMddHHmmss}.{query.Format.ToLower()}";
            return File(result, contentType, fileName);
        }

        /// <summary>
        /// Müşteri import template'i indir
        /// </summary>
        [HttpGet("import/template")]
        public async Task<IActionResult> GetImportTemplate()
        {
            var query = new GetCustomerImportTemplateQuery();
            var template = await _mediator.Send(query);
            return File(template, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomersImportTemplate.xlsx");
        }

        /// <summary>
        /// Müşterileri import et (Excel, CSV)
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

            var command = new ImportCustomersCommand
            {
                FileData = fileData,
                FileName = file.FileName,
                Format = format
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}

