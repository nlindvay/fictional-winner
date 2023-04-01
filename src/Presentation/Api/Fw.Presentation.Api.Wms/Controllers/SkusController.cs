using Fw.Domain.Wms.Contracts;
using Fw.Domain.Wms.Enums;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Fw.Presentation.Api.Wms.Controllers;

[ApiController]
[Route("Wms/[controller]")]
public class SkusController : ControllerBase
{
    private readonly ILogger<SkusController> _logger;
    readonly IMediator _mediator;

    public SkusController(ILogger<SkusController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost()]
    public async Task<IActionResult> SubmitSku([FromBody] SubmitSku submitSku, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Submitting Sku {SkuCode}", submitSku.SkuCode);

        return Ok(await _mediator.SendRequest(submitSku, cancellationToken));

    }

    [HttpGet("{skuId}")]
    public async Task<IActionResult> GetSku(Guid skuId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting Sku {SkuId}", skuId);

        return Ok(await _mediator.SendRequest(new GetSku { SkuId = skuId }, cancellationToken));
    }

    [HttpGet()]
    public async Task<IActionResult> PaginateSkus(int page, int size, SortDirection direction, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting Skus");

        return Ok(await _mediator.SendRequest(new PaginateSkus { Page = page, Size = size, Direction = default }, cancellationToken));
    }
}