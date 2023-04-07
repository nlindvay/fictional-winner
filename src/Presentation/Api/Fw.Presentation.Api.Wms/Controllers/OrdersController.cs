using Fw.Domain.Wms.Contracts;
using Fw.Domain.Common.Enums;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Fw.Presentation.Api.Wms.Controllers;

[ApiController]
[Route("Wms/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    readonly IMediator _mediator;

    public OrdersController(ILogger<OrdersController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost()]
    public async Task<IActionResult> SubmitOrder([FromBody] SubmitOrder submitOrder, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Submitting order {PrimaryReference}", submitOrder.PrimaryReference);

        return Ok(await _mediator.SendRequest(submitOrder, cancellationToken));

    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrder(Guid orderId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting order {OrderId}", orderId);

        return Ok(await _mediator.SendRequest(new GetOrder { OrderId = orderId }, cancellationToken));
    }

    [HttpGet()]
    public async Task<IActionResult> PaginateOrders(int page, int size, SortDirection direction, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting orders");

        return Ok(await _mediator.SendRequest(new PaginateOrders { Page = page, Size = size, Direction = default }, cancellationToken));
    }

    [HttpPost("{orderId}/lines")]
    public async Task<IActionResult> AddOrderLine(Guid orderId, AddOrderLine addOrderLine, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding order line to order {OrderId}", orderId);

        return Ok(await _mediator.SendRequest(addOrderLine, cancellationToken));
    }

    [HttpPost("{orderId}/actions/ship")]
    public async Task<IActionResult> ShipOrder(Guid orderId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Requesting booking for order {OrderId}", orderId);

        return Ok(await _mediator.SendRequest(new RequestShipOrder { OrderId = orderId }, cancellationToken));
    }
}