using MagicCarRepairAISupported.Application.Features.Media.Queries.GetCustomerWorkOrderPhoto;
using MagicCarRepairAISupported.WebAPI.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicCarRepairAISupported.WebAPI.Controllers;

[ApiController]
[Route("api/customer-media")]
[Authorize(Policy = AuthPolicyNames.CustomerOrSystemAdmin)]
public sealed class CustomerMediaController(IMediator mediator) : ControllerBase
{
    [HttpGet("customers/{customerId:int}/work-orders/{workOrderId:int}/photos/{photoId:int}")]
    public async Task<IActionResult> GetWorkOrderPhoto(int customerId, int workOrderId, int photoId, CancellationToken cancellationToken)
    {
        var media = await mediator.Send(new GetCustomerWorkOrderPhotoQuery { CustomerId = customerId, WorkOrderId = workOrderId, PhotoId = photoId }, cancellationToken);
        return media is null ? NotFound() : File(media.Content, media.ContentType, enableRangeProcessing: true);
    }
}
