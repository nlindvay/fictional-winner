using Fw.Application.Wms.Interfaces;
using Fw.Domain.Wms.Contracts;
using Fw.Domain.Wms.Entities;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Wms.Consumers;

public class SubmitOrderConsumer : MediatorRequestHandler<SubmitOrder, OrderSubmitted>

{
    readonly IApplicationDbContext _context;
    readonly ILogger<SubmitOrderConsumer> _logger;

    public SubmitOrderConsumer(IApplicationDbContext context, ILogger<SubmitOrderConsumer> logger)
    {
        _context = context;
        _logger = logger;
    }

    protected override async Task<OrderSubmitted> Handle(SubmitOrder request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("SubmitOrderConsumer: {OrderReference}", request.OrderReference);
        
        var order = new Order
        {
            OrderId = NewId.NextGuid(),
            ClientId = NewId.NextGuid(),
            CustomerId = NewId.NextGuid(),
            OrderReference = request.OrderReference,
            CustomerReference = request.CustomerReference,
            OrderStatus = Domain.Wms.Enums.OrderStatus.Submitted
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);

        return new OrderSubmitted
        {
            OrderId = order.OrderId
        };
    }
}