using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddItem;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddLabor;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddPhoto;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.ApproveByCustomer;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.Complete;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.Create;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.Deliver;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.RejectByCustomer;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.RemoveItem;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.RequestCustomerApproval;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.Update;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.UpdateStatus;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetActive;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetAll;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetById;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetPendingApprovals;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetTimeline;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.Export;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GeneratePdf;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GenerateQrCode;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetByEmployee;
using MagicCarRepairAISupported.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WorkOrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkOrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Tüm iş emirlerini listele
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] WorkOrderStatus? status,
            [FromQuery] int? customerId,
            [FromQuery] int? vehicleId,
            [FromQuery] int? employeeId,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            var query = new GetAllWorkOrdersQuery
            {
                Status = status,
                CustomerId = customerId,
                VehicleId = vehicleId,
                EmployeeId = employeeId,
                StartDate = startDate,
                EndDate = endDate
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Aktif iş emirlerini listele
        /// </summary>
        [HttpGet("active")]
        public async Task<IActionResult> GetActive()
        {
            var query = new GetActiveWorkOrdersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Belirli bir personele atanmış iş emirlerini listele
        /// </summary>
        [HttpGet("by-employee/{employeeId}")]
        public async Task<IActionResult> GetByEmployee(
            int employeeId,
            [FromQuery] Domain.Enums.WorkOrderStatus? status,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] bool onlyActive = false)
        {
            var query = new GetWorkOrdersByEmployeeQuery
            {
                EmployeeId = employeeId,
                Status = status,
                StartDate = startDate,
                EndDate = endDate,
                OnlyActive = onlyActive
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// İş emrini ID'ye göre getir
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetWorkOrderByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// İş emri timeline'ını getir
        /// </summary>
        [HttpGet("{id}/timeline")]
        public async Task<IActionResult> GetTimeline(int id)
        {
            var query = new GetWorkOrderTimelineQuery { WorkOrderId = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Yeni iş emri oluştur
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWorkOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// İş emri bilgilerini güncelle
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateWorkOrderCommand command)
        {
            command.WorkOrderId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// İş emri durumunu güncelle
        /// </summary>
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateWorkOrderStatusCommand command)
        {
            command.WorkOrderId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// İş emrini tamamla
        /// </summary>
        [HttpPost("{id}/complete")]
        public async Task<IActionResult> Complete(int id, [FromBody] CompleteWorkOrderCommand command)
        {
            command.WorkOrderId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// İş emrini teslim et
        /// </summary>
        [HttpPost("{id}/deliver")]
        public async Task<IActionResult> Deliver(int id, [FromBody] DeliverWorkOrderCommand command)
        {
            command.WorkOrderId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// İş emrine item (parça/işçilik) ekle
        /// </summary>
        [HttpPost("{id}/items")]
        public async Task<IActionResult> AddItem(int id, [FromBody] AddWorkOrderItemCommand command)
        {
            command.WorkOrderId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// İş emrinden item kaldır
        /// </summary>
        [HttpDelete("{id}/items/{itemId}")]
        public async Task<IActionResult> RemoveItem(int id, int itemId)
        {
            var command = new RemoveWorkOrderItemCommand
            {
                WorkOrderId = id,
                ItemId = itemId
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// İş emrine işçilik ekle
        /// </summary>
        [HttpPost("{id}/labors")]
        public async Task<IActionResult> AddLabor(int id, [FromBody] AddWorkOrderLaborCommand command)
        {
            command.WorkOrderId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// İş emrine fotoğraf ekle
        /// </summary>
        [HttpPost("{id}/photos")]
        public async Task<IActionResult> AddPhoto(int id, [FromBody] AddWorkOrderPhotoCommand command)
        {
            command.WorkOrderId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// İş emri PDF'i oluştur
        /// </summary>
        [HttpGet("{id}/pdf")]
        public async Task<IActionResult> GeneratePdf(int id)
        {
            var query = new GenerateWorkOrderPdfQuery { WorkOrderId = id };
            var pdfBytes = await _mediator.Send(query);
            return File(pdfBytes, "application/pdf", $"WorkOrder-{id}.pdf");
        }

        /// <summary>
        /// İş emri QR kodu oluştur
        /// </summary>
        [HttpGet("{id}/qr-code")]
        public async Task<IActionResult> GenerateQrCode(int id)
        {
            var query = new GenerateWorkOrderQrCodeQuery { WorkOrderId = id };
            var qrBytes = await _mediator.Send(query);
            return File(qrBytes, "image/png", $"WorkOrder-{id}-QR.png");
        }

        /// <summary>
        /// İş emirlerini export et (Excel, CSV, PDF)
        /// </summary>
        [HttpGet("export")]
        public async Task<IActionResult> Export([FromQuery] ExportWorkOrdersQuery query)
        {
            var result = await _mediator.Send(query);
            var contentType = query.Format.ToLower() switch
            {
                "excel" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "csv" => "text/csv",
                "pdf" => "application/pdf",
                _ => "application/octet-stream"
            };
            var fileName = $"WorkOrders-{DateTime.UtcNow:yyyyMMddHHmmss}.{query.Format.ToLower()}";
            return File(result, contentType, fileName);
        }

        /// <summary>
        /// Müşteri onayı bekleyen iş emirlerini listele
        /// </summary>
        [HttpGet("pending-approvals")]
        public async Task<IActionResult> GetPendingApprovals([FromQuery] int? customerId, [FromQuery] int? skip, [FromQuery] int? take)
        {
            var query = new GetPendingApprovalsQuery
            {
                CustomerId = customerId,
                Skip = skip,
                Take = take
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// İş emri için müşteri onayı talep et
        /// </summary>
        [HttpPost("{id}/request-approval")]
        public async Task<IActionResult> RequestCustomerApproval(int id, [FromBody] RequestCustomerApprovalCommand command)
        {
            command.WorkOrderId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// İş emrini müşteri olarak onayla
        /// </summary>
        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApproveByCustomer(int id, [FromBody] ApproveWorkOrderByCustomerCommand command)
        {
            command.WorkOrderId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// İş emrini müşteri olarak reddet
        /// </summary>
        [HttpPost("{id}/reject")]
        public async Task<IActionResult> RejectByCustomer(int id, [FromBody] RejectWorkOrderByCustomerCommand command)
        {
            command.WorkOrderId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}

