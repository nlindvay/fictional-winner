using Fw.Application.Wms.Interfaces;
using Fw.Domain.Wms.Contracts;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Wms.Consumers;

public class GetOrderConsumer : MediatorRequestHandler<GetOrder, OrderDto>
{
    readonly IApplicationDbContext _context;
    private readonly ILogger<GetOrderConsumer> _logger;

    public GetOrderConsumer(IApplicationDbContext context, ILogger<GetOrderConsumer> logger)
    {
        _context = context;
        _logger = logger;
    }

    protected override async Task<OrderDto> Handle(GetOrder request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetOrderConsumer: {OrderId}", request.OrderId);

        var order = _context.Orders.FirstOrDefault(order => order.OrderId == request.OrderId);

        if (order == null)
            throw new Exception("Order not found");

        return new OrderDto
        {
            ClientId = order.ClientId,
            CustomerId = order.CustomerId,
            OrderId = order.OrderId,
            OrderReference = order.OrderReference,
            CustomerReference = order.CustomerReference,
            OrderStatus = order.OrderStatus
        };
    }
}
