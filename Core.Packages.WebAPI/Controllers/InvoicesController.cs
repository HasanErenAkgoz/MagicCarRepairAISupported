using MagicCarRepairAISupported.Application.Features.Invoices.Commands.Create;
using MagicCarRepairAISupported.Application.Features.Invoices.Commands.GenerateFromWorkOrder;
using MagicCarRepairAISupported.Application.Features.Invoices.Commands.SendByEmail;
using MagicCarRepairAISupported.Application.Features.Invoices.Commands.UpdateStatus;
using MagicCarRepairAISupported.Application.Features.Invoices.Queries.ExportToExcel;
using MagicCarRepairAISupported.Application.Features.Invoices.Queries.GeneratePdf;
using MagicCarRepairAISupported.Application.Features.Invoices.Queries.GenerateQrCode;
using MagicCarRepairAISupported.Application.Features.Invoices.Queries.GetAll;
using MagicCarRepairAISupported.Application.Features.Invoices.Queries.GetById;
using MagicCarRepairAISupported.Application.Features.Invoices.Queries.GetOverdue;
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
    public class InvoicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvoicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Tüm faturaları listele (Filtreleme ve sayfalama ile)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllInvoicesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Faturayı ID'ye göre getir
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetInvoiceByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Vadesi geçen faturaları getir
        /// </summary>
        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdue([FromQuery] GetOverdueInvoicesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Yeni fatura oluştur
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInvoiceCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// İş emrinden fatura oluştur
        /// </summary>
        [HttpPost("from-workorder/{workOrderId}")]
        public async Task<IActionResult> GenerateFromWorkOrder(int workOrderId, [FromBody] GenerateInvoiceFromWorkOrderCommand? command = null)
        {
            if (command == null)
            {
                command = new GenerateInvoiceFromWorkOrderCommand();
            }
            command.WorkOrderId = workOrderId;
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.InvoiceId }, result);
        }

        /// <summary>
        /// Fatura durumunu güncelle
        /// </summary>
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateInvoiceStatusCommand command)
        {
            command.InvoiceId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Fatura PDF oluştur
        /// </summary>
        [HttpGet("{id}/pdf")]
        public async Task<IActionResult> GeneratePdf(int id)
        {
            var query = new GenerateInvoicePdfQuery { InvoiceId = id };
            var pdfBytes = await _mediator.Send(query);
            return File(pdfBytes, "application/pdf", $"Invoice_{id}.pdf");
        }

        /// <summary>
        /// Faturayı email ile gönder
        /// </summary>
        [HttpPost("{id}/send-email")]
        public async Task<IActionResult> SendByEmail(int id, [FromBody] SendInvoiceByEmailCommand command)
        {
            command.InvoiceId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Fatura QR Code oluştur
        /// </summary>
        [HttpGet("{id}/qrcode")]
        public async Task<IActionResult> GenerateQrCode(int id, [FromQuery] int size = 300)
        {
            var query = new GenerateInvoiceQrCodeQuery { InvoiceId = id, Size = size };
            var qrCodeBytes = await _mediator.Send(query);
            return File(qrCodeBytes, "image/png", $"Invoice_{id}_QRCode.png");
        }

        /// <summary>
        /// Faturayı Excel formatında export et
        /// </summary>
        [HttpGet("{id}/excel")]
        public async Task<IActionResult> ExportToExcel(int id)
        {
            var query = new ExportInvoiceToExcelQuery { InvoiceId = id };
            var excelBytes = await _mediator.Send(query);
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Invoice_{id}.xlsx");
        }
    }
}

