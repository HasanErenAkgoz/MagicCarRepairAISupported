using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddItem;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddLabor;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddPhoto;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.ApproveByCustomer;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.Complete;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.Create;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.CreateMobile;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.Deliver;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.RejectByCustomer;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.RemoveItem;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.RequestCustomerApproval;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.Update;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.UpdateMobile;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.UpdateMobileStatus;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddMobilePart;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.UpdateMobilePart;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.DeleteMobilePart;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddMobileLabor;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.UpdateMobileLabor;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.DeleteMobileLabor;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddTimelineNote;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.UpdateStatus;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.Export;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GeneratePdf;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GenerateQrCode;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetActive;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetAll;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetByEmployee;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetById;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetMobileDetail;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetMobileList;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetPendingApprovals;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetTimeline;
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
    public class WorkOrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkOrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Tum is emirlerini listele (Mobil uygulama icin - dokumana uygun format)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? status,
            [FromQuery] int? page,
            [FromQuery] int? pageSize,
            [FromQuery] WorkOrderStatus? statusEnum,
            [FromQuery] int? customerId,
            [FromQuery] int? vehicleId,
            [FromQuery] int? employeeId,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] bool legacyFormat = false)
        {
            // Mobil kontrat varsayilan davranistir. Eski response sadece acikca istendiginde donulur.
            if (!legacyFormat)
            {
                var mobileQuery = new GetMobileWorkOrdersListQuery
                {
                    Status = status,
                    Page = page ?? 1,
                    PageSize = pageSize ?? 20,
                    CustomerId = customerId
                };

                var mobileResult = await _mediator.Send(mobileQuery);
                return Ok(new
                {
                    success = true,
                    data = mobileResult.Data,
                    totalCount = mobileResult.TotalCount,
                    page = mobileResult.Page,
                    pageSize = mobileResult.PageSize,
                    totalPages = mobileResult.TotalPages
                });
            }

            var query = new GetAllWorkOrdersQuery
            {
                Status = statusEnum,
                CustomerId = customerId,
                VehicleId = vehicleId,
                EmployeeId = employeeId,
                StartDate = startDate,
                EndDate = endDate
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActive()
        {
            var query = new GetActiveWorkOrdersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("by-employee/{employeeId}")]
        public async Task<IActionResult> GetByEmployee(
            int employeeId,
            [FromQuery] WorkOrderStatus? status,
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
        /// Is emrini ID'ye gore getir (Mobil uygulama icin - dokumana uygun format)
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, [FromQuery] bool legacyFormat = false)
        {
            if (!legacyFormat)
            {
                var mobileQuery = new GetMobileWorkOrderDetailQuery { Id = id };
                var mobileResult = await _mediator.Send(mobileQuery);
                return Ok(new { success = true, data = mobileResult });
            }

            if (int.TryParse(id, out var intId))
            {
                var query = new GetWorkOrderByIdQuery { Id = intId };
                var result = await _mediator.Send(query);
                return Ok(result);
            }

            return BadRequest(new { success = false, message = "Gecersiz ID formatı." });
        }

        [HttpGet("{id}/timeline")]
        public async Task<IActionResult> GetTimeline(int id)
        {
            var query = new GetWorkOrderTimelineQuery { WorkOrderId = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWorkOrderCommand? command, [FromQuery] bool? mobileFormat)
        {
            // Mobil format isteniyorsa
            if (mobileFormat == true)
            {
                return BadRequest(new { success = false, message = "Mobil format için POST /api/WorkOrders/mobile endpoint'ini kullanın." });
            }

            // Eski format
            if (command == null)
            {
                return BadRequest(new { success = false, message = "Request body boş olamaz." });
            }

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Yeni iş emri oluştur (Mobil uygulama için - müşteri ve araç inline)
        /// </summary>
        [HttpPost("mobile")]
        public async Task<IActionResult> CreateMobile([FromBody] CreateMobileWorkOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { success = true, message = "İş emri oluşturuldu.", data = result.Data });
        }

        /// <summary>
        /// İş emrini güncelle (Mobil uygulama için - dokümana uygun format)
        /// </summary>
        /// <remarks>
        /// Mobil uygulama için optimize edilmiş endpoint.
        /// Parts ve labor replace semantiğiyle çalışır.
        /// </remarks>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateMobileWorkOrderCommand? command, [FromQuery] bool? mobileFormat)
        {
            // Mobil format isteniyorsa
            if (mobileFormat == true || command != null)
            {
                if (command == null)
                {
                    return BadRequest(new { success = false, message = "Request body boş olamaz." });
                }
                command.Id = id;
                var result = await _mediator.Send(command);
                return Ok(new { success = true, message = "İş emri başarıyla güncellendi.", data = result.Data });
            }

            // Eski API formatı (int ID)
            if (int.TryParse(id, out var intId))
            {
                var legacyCommand = new UpdateWorkOrderCommand { WorkOrderId = intId };
                var legacyResult = await _mediator.Send(legacyCommand);
                return Ok(legacyResult);
            }

            return BadRequest(new { success = false, message = "Geçersiz ID formatı." });
        }

        [HttpPost("{id}/status")]
        public async Task<IActionResult> UpdateMobileStatus(string id, [FromBody] UpdateMobileWorkOrderStatusCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(new { success = true, data = result.Data });
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateWorkOrderStatusCommand command)
        {
            command.WorkOrderId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("{id}/complete")]
        public async Task<IActionResult> Complete(int id, [FromBody] CompleteWorkOrderCommand command)
        {
            command.WorkOrderId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("{id}/deliver")]
        public async Task<IActionResult> Deliver(int id, [FromBody] DeliverWorkOrderCommand command)
        {
            command.WorkOrderId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("{id}/items")]
        public async Task<IActionResult> AddItem(int id, [FromBody] AddWorkOrderItemCommand command)
        {
            command.WorkOrderId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

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

        [HttpPost("{id}/labors")]
        public async Task<IActionResult> AddLabor(int id, [FromBody] AddWorkOrderLaborCommand command)
        {
            command.WorkOrderId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("{id}/photos")]
        public async Task<IActionResult> AddPhoto(int id, [FromBody] AddWorkOrderPhotoCommand command)
        {
            command.WorkOrderId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // ──────────────────────────────────────────────
        // Mobil Format - Parts CRUD
        // ──────────────────────────────────────────────

        /// <summary>
        /// İş emrine parça ekle (Mobil uygulama için)
        /// </summary>
        [HttpPost("{id}/parts")]
        public async Task<IActionResult> AddMobilePart(string id, [FromBody] AddMobileWorkOrderPartCommand command)
        {
            command.WorkOrderId = id;
            var result = await _mediator.Send(command);
            return Ok(new { success = true, data = result });
        }

        /// <summary>
        /// İş emri parçasını güncelle (Mobil uygulama için)
        /// </summary>
        [HttpPut("{id}/parts/{partId}")]
        public async Task<IActionResult> UpdateMobilePart(string id, string partId, [FromBody] UpdateMobileWorkOrderPartCommand command)
        {
            command.WorkOrderId = id;
            command.PartId = partId;
            var result = await _mediator.Send(command);
            return Ok(new { success = true, data = result });
        }

        /// <summary>
        /// İş emri parçasını sil (Mobil uygulama için)
        /// </summary>
        [HttpDelete("{id}/parts/{partId}")]
        public async Task<IActionResult> DeleteMobilePart(string id, string partId)
        {
            var command = new DeleteMobileWorkOrderPartCommand
            {
                WorkOrderId = id,
                PartId = partId
            };
            var result = await _mediator.Send(command);
            return Ok(new { success = true, message = "Parça silindi.", data = result });
        }

        // ──────────────────────────────────────────────
        // Mobil Format - Labor CRUD
        // ──────────────────────────────────────────────

        /// <summary>
        /// İş emrine işçilik kalemi ekle (Mobil uygulama için)
        /// </summary>
        [HttpPost("{id}/labor")]
        public async Task<IActionResult> AddMobileLabor(string id, [FromBody] AddMobileWorkOrderLaborCommand command)
        {
            command.WorkOrderId = id;
            var result = await _mediator.Send(command);
            return Ok(new { success = true, data = result });
        }

        /// <summary>
        /// İş emri işçilik kalemini güncelle (Mobil uygulama için)
        /// </summary>
        [HttpPut("{id}/labor/{laborId}")]
        public async Task<IActionResult> UpdateMobileLabor(string id, string laborId, [FromBody] UpdateMobileWorkOrderLaborCommand command)
        {
            command.WorkOrderId = id;
            command.LaborId = laborId;
            var result = await _mediator.Send(command);
            return Ok(new { success = true, data = result });
        }

        /// <summary>
        /// İş emri işçilik kalemini sil (Mobil uygulama için)
        /// </summary>
        [HttpDelete("{id}/labor/{laborId}")]
        public async Task<IActionResult> DeleteMobileLabor(string id, string laborId)
        {
            var command = new DeleteMobileWorkOrderLaborCommand
            {
                WorkOrderId = id,
                LaborId = laborId
            };
            var result = await _mediator.Send(command);
            return Ok(new { success = true, message = "İşçilik kalemi silindi.", data = result });
        }

        // ──────────────────────────────────────────────
        // Mobil Format - Timeline
        // ──────────────────────────────────────────────

        /// <summary>
        /// İş emri timeline'a manuel not ekle (Mobil uygulama için)
        /// </summary>
        [HttpPost("{id}/timeline")]
        public async Task<IActionResult> AddTimelineNote(string id, [FromBody] AddWorkOrderTimelineNoteCommand command)
        {
            command.WorkOrderId = id;
            var result = await _mediator.Send(command);
            return Ok(new { success = true, data = result.Data });
        }

        [HttpGet("{id}/pdf")]
        public async Task<IActionResult> GeneratePdf(int id)
        {
            var query = new GenerateWorkOrderPdfQuery { WorkOrderId = id };
            var pdfBytes = await _mediator.Send(query);
            return File(pdfBytes, "application/pdf", $"WorkOrder-{id}.pdf");
        }

        [HttpGet("{id}/qr-code")]
        public async Task<IActionResult> GenerateQrCode(int id)
        {
            var query = new GenerateWorkOrderQrCodeQuery { WorkOrderId = id };
            var qrBytes = await _mediator.Send(query);
            return File(qrBytes, "image/png", $"WorkOrder-{id}-QR.png");
        }

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

        [HttpPost("{id}/request-approval")]
        public async Task<IActionResult> RequestCustomerApproval(int id, [FromBody] RequestCustomerApprovalCommand command)
        {
            command.WorkOrderId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApproveByCustomer(int id, [FromBody] ApproveWorkOrderByCustomerCommand command)
        {
            command.WorkOrderId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("{id}/reject")]
        public async Task<IActionResult> RejectByCustomer(int id, [FromBody] RejectWorkOrderByCustomerCommand command)
        {
            command.WorkOrderId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
