using Fw.Domain.Wms.Contracts;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Fw.Presentation.Api.Wms.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WmsController : ControllerBase
    {
        private readonly ILogger<WmsController> _logger;
        readonly IMediator _mediator;

        public WmsController(ILogger<WmsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("submit-order")]
        public async Task<IActionResult> SubmitOrder([FromBody] SubmitOrder submitOrder, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Submitting order {OrderReference}", submitOrder.OrderReference);

            return Ok(await _mediator.SendRequest(submitOrder, cancellationToken));

        }

        [HttpGet("get-order/{orderId}")]
        public async Task<IActionResult> GetOrder(Guid orderId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting order {OrderId}", orderId);

            return Ok(await _mediator.SendRequest(new GetOrder { OrderId = orderId }, cancellationToken));
        }
    }
}