using MagicCarRepairAISupported.Application.Features.Insurance.Commands.CreateInsuranceCompany;
using MagicCarRepairAISupported.Application.Features.Insurance.Commands.CreateInsuranceClaim;
using MagicCarRepairAISupported.Application.Features.Insurance.Commands.CreateInsurancePolicy;
using MagicCarRepairAISupported.Application.Features.Insurance.Commands.UpdateInsuranceClaimStatus;
using MagicCarRepairAISupported.Application.Features.Insurance.Commands.UpdateInsuranceCompany;
using MagicCarRepairAISupported.Application.Features.Insurance.Commands.RenewInsurancePolicy;
using MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetAllInsuranceCompanies;
using MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetExpiringPolicies;
using MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetInsurancePoliciesByVehicle;
using MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetInsurancePoliciesByCustomer;
using MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetAllInsuranceClaims;
using MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetInsuranceClaimsByCustomer;
using MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetInsuranceClaimsByWorkOrder;
using MagicCarRepairAISupported.Application.Features.Insurance.Queries.GetInsuranceClaimById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using MagicCarRepairAISupported.WebAPI.Authorization;
using MagicCarRepairAISupported.WebAPI.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers
{
    [ApiController]
    [Route("api/insurance")]
    [Authorize(Policy = AuthPolicyNames.ShopStaff)]
    public class InsuranceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InsuranceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Tüm sigorta firmalarını listele
        /// </summary>
        [HttpGet("companies")]
        public async Task<IActionResult> GetAllInsuranceCompanies([FromQuery] GetAllInsuranceCompaniesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(new { success = true, data = result.Companies, meta = new { result.TotalCount, result.PageNumber, result.PageSize } });
        }

        /// <summary>
        /// Sigorta firması oluştur
        /// </summary>
        [HttpPost("companies")]
        public async Task<IActionResult> CreateInsuranceCompany([FromBody] CreateInsuranceCompanyCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAllInsuranceCompanies), new { id = result.Id }, result);
        }

        /// <summary>
        /// Sigorta firması güncelle
        /// </summary>
        [HttpPut("companies/{id}")]
        public async Task<IActionResult> UpdateInsuranceCompany(int id, [FromBody] UpdateInsuranceCompanyCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Poliçe oluştur
        /// </summary>
        [HttpPost("policies")]
        public async Task<IActionResult> CreateInsurancePolicy([FromBody] CreateInsurancePolicyCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateInsurancePolicy), new { id = result.Id }, result);
        }

        /// <summary>
        /// Süresi yaklaşan poliçeleri getir
        /// </summary>
        [HttpGet("policies/expiring")]
        public async Task<IActionResult> GetExpiringPolicies([FromQuery] int daysBeforeExpiration = 30)
        {
            var query = new GetExpiringPoliciesQuery { DaysBeforeExpiration = daysBeforeExpiration };
            var result = await _mediator.Send(query);
            return Ok(new { success = true, data = result.Policies, meta = new { result.TotalCount } });
        }

        /// <summary>
        /// Tüm sigorta hasarlarını listele
        /// </summary>
        [HttpGet("claims")]
        public async Task<IActionResult> GetAllInsuranceClaims()
        {
            var query = new GetAllInsuranceClaimsQuery();
            var result = await _mediator.Send(query);
            return Ok(new { success = true, data = result.Claims });
        }

        /// <summary>
        /// Sigorta hasarı oluştur
        /// </summary>
        [HttpPost("claims")]
        public async Task<IActionResult> CreateInsuranceClaim([FromBody] CreateInsuranceClaimCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateInsuranceClaim), new { id = result.Id }, result);
        }

        /// <summary>
        /// Araç bazlı poliçeleri getir
        /// </summary>
        [HttpGet("policies/vehicle/{vehicleId}")]
        public async Task<IActionResult> GetInsurancePoliciesByVehicle(int vehicleId)
        {
            var query = new GetInsurancePoliciesByVehicleQuery { VehicleId = vehicleId };
            var result = await _mediator.Send(query);
            return Ok(new { success = true, data = result.Policies });
        }

        /// <summary>
        /// Müşteri bazlı poliçeleri getir
        /// </summary>
        [HttpGet("policies/customer/{customerId}")]
        public async Task<IActionResult> GetInsurancePoliciesByCustomer(int customerId, [FromQuery] bool? activeOnly = null)
        {
            var query = new GetInsurancePoliciesByCustomerQuery { CustomerId = customerId, ActiveOnly = activeOnly };
            var result = await _mediator.Send(query);
            return Ok(new { success = true, data = result.Policies });
        }

        /// <summary>
        /// Poliçe yenile
        /// </summary>
        [HttpPost("policies/{id}/renew")]
        public async Task<IActionResult> RenewInsurancePolicy(int id, [FromBody] RenewInsurancePolicyCommand command)
        {
            command.PolicyId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// İş emri bazlı hasarları getir
        /// </summary>
        [HttpGet("claims/workorder/{workOrderId}")]
        public async Task<IActionResult> GetInsuranceClaimsByWorkOrder(int workOrderId)
        {
            var query = new GetInsuranceClaimsByWorkOrderQuery { WorkOrderId = workOrderId };
            var result = await _mediator.Send(query);
            return Ok(new { success = true, data = result.Claims });
        }

        /// <summary>
        /// Sigorta hasar durumunu güncelle
        /// </summary>
        [HttpPut("claims/{id}/status")]
        public async Task<IActionResult> UpdateInsuranceClaimStatus(int id, [FromBody] UpdateInsuranceClaimStatusCommand command)
        {
            command.ClaimId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Müşteri bazlı hasar taleplerini getir
        /// </summary>
        [HttpGet("claims/customer/{customerId}")]
        public async Task<IActionResult> GetInsuranceClaimsByCustomer(int customerId)
        {
            var query = new GetInsuranceClaimsByCustomerQuery { CustomerId = customerId };
            var result = await _mediator.Send(query);
            return Ok(new { success = true, data = result.Claims });
        }

        /// <summary>
        /// Sigorta hasarı detayını getir
        /// </summary>
        [HttpGet("claims/{id}")]
        public async Task<IActionResult> GetInsuranceClaimById(int id)
        {
            var query = new GetInsuranceClaimByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(new { success = true, data = result });
        }
    }
}

