using MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivateVehiclePhoto;
using MagicCarRepairAISupported.Application.Features.Media.Queries.GetPublicPortfolioPhoto;
using MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivateWorkOrderPhoto;
using MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivateQuoteRequestPhoto;
using MagicCarRepairAISupported.Application.Features.Media.Queries.GetPrivatePartPhoto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MagicCarRepairAISupported.WebAPI.Authorization;

namespace MagicCarRepairAISupported.WebAPI.Controllers;

[ApiController]
[Route("api/media")]
[Authorize(Policy = AuthPolicyNames.ShopStaff)]
public sealed class MediaController(IMediator mediator) : ControllerBase
{
    /// <summary>Returns a private vehicle photo only after tenant and resource ownership are verified.</summary>
    [HttpGet("vehicles/{vehicleId:int}/photos/{photoId:int}")]
    public async Task<IActionResult> GetVehiclePhoto(int vehicleId, int photoId, CancellationToken cancellationToken)
    {
        var media = await mediator.Send(new GetPrivateVehiclePhotoQuery { VehicleId = vehicleId, PhotoId = photoId }, cancellationToken);
        return media is null ? NotFound() : File(media.Content, media.ContentType, enableRangeProcessing: true);
    }

    [HttpGet("work-orders/{workOrderId:int}/photos/{photoId:int}")]
    public async Task<IActionResult> GetWorkOrderPhoto(int workOrderId, int photoId, CancellationToken cancellationToken)
    {
        var media = await mediator.Send(new GetPrivateWorkOrderPhotoQuery { WorkOrderId = workOrderId, PhotoId = photoId }, cancellationToken);
        return media is null ? NotFound() : File(media.Content, media.ContentType, enableRangeProcessing: true);
    }

    [HttpGet("quote-requests/{quoteRequestId:int}/photos/{photoId:int}")]
    public async Task<IActionResult> GetQuoteRequestPhoto(int quoteRequestId, int photoId, CancellationToken cancellationToken)
    {
        var media = await mediator.Send(new GetPrivateQuoteRequestPhotoQuery { QuoteRequestId = quoteRequestId, PhotoId = photoId }, cancellationToken);
        return media is null ? NotFound() : File(media.Content, media.ContentType, enableRangeProcessing: true);
    }

    [HttpGet("parts/{partId:int}/photos/{photoId:int}")]
    public async Task<IActionResult> GetPartPhoto(int partId, int photoId, CancellationToken cancellationToken)
    {
        var media = await mediator.Send(new GetPrivatePartPhotoQuery { PartId = partId, PhotoId = photoId }, cancellationToken);
        return media is null ? NotFound() : File(media.Content, media.ContentType, enableRangeProcessing: true);
    }
}

[ApiController]
[Route("api/public-media")]
public sealed class PublicMediaController(IMediator mediator) : ControllerBase
{
    /// <summary>Anonymous access is possible only for media explicitly selected in a published portfolio.</summary>
    [AllowAnonymous]
    [HttpGet("clients/{clientId:int}/portfolios/{portfolioId:int}/photos/{photoId:int}")]
    public async Task<IActionResult> GetPortfolioPhoto(int clientId, int portfolioId, int photoId, CancellationToken cancellationToken)
    {
        var media = await mediator.Send(new GetPublicPortfolioPhotoQuery { ClientId = clientId, PortfolioId = portfolioId, PhotoId = photoId }, cancellationToken);
        return media is null ? NotFound() : File(media.Content, media.ContentType, enableRangeProcessing: true);
    }
}
