using Fw.Domain.Tms.Contracts;
using Fw.Domain.Common.Enums;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Fw.Presentation.Api.Tms.Controllers;

[ApiController]
[Route("Tms/[controller]")]
public class ShipmentsController : ControllerBase
{
    private readonly ILogger<ShipmentsController> _logger;
    readonly IMediator _mediator;

    public ShipmentsController(ILogger<ShipmentsController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost()]
    public async Task<IActionResult> SubmitShipment([FromBody] SubmitShipment submitShipment, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Submitting Shipment {PrimaryReference}", submitShipment.PrimaryReference);

        return Ok(await _mediator.SendRequest(submitShipment, cancellationToken));

    }

    [HttpGet("{shipmentId}")]
    public async Task<IActionResult> GetShipment(Guid shipmentId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting Shipment {ShipmentId}", shipmentId);

        return Ok(await _mediator.SendRequest(new GetShipment { ShipmentId = shipmentId }, cancellationToken));
    }

    [HttpGet()]
    public async Task<IActionResult> PaginateShipments(int page, int size, SortDirection direction, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting Shipments");

        return Ok(await _mediator.SendRequest(new PaginateShipments { Page = page, Size = size, Direction = default }, cancellationToken));
    }

    [HttpPost("{shipmentId}/packages")]
    public async Task<IActionResult> AddPackage(Guid shipmentId, [FromBody] SubmitPack submitPack, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding Package to Shipment {ShipmentId}", shipmentId);

        return Ok(await _mediator.SendRequest(submitPack, cancellationToken));
    }

    [HttpPost("{shipmentId}/packages/{packageId}/packlines")]
    public async Task<IActionResult> AddPackLine(Guid shipmentId, Guid packageId, [FromBody] SubmitPackLine submitPackLine, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding Pack line to package {PackageId}", packageId);

        return Ok(await _mediator.SendRequest(submitPackLine, cancellationToken));
    }

    [HttpPost("{shipmentId}/actions/invoice")]
    public async Task<IActionResult> RequestShipmentInvoicing(Guid shipmentId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Requesting Shipment Invoicing {ShipmentId}", shipmentId);

        return Ok(await _mediator.SendRequest(new RequestShipmentInvoicing { ShipmentId = shipmentId }, cancellationToken));
    }
}