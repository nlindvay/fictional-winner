using MapsterMapper;
using Fw.Application.Wms.Interfaces;
using Fw.Domain.Wms.Contracts;
using Fw.Domain.Wms.Entities;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Wms.Handlers;

public class AddOrderLineHandler : MediatorRequestHandler<AddOrderLine, OrderLineAdded>
{

    readonly IWmsDbContext _context;
    readonly ILogger<GetOrderHandler> _logger;
    readonly IMapper _mapper;
    readonly IPublishEndpoint _publisher;


    public AddOrderLineHandler(IWmsDbContext context, ILogger<GetOrderHandler> logger, IMapper mapper, IPublishEndpoint publisher)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
        _publisher = publisher;
    }

    protected override async Task<OrderLineAdded> Handle(AddOrderLine request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("AddOrderLineConsumer: {OrderId} {SkuId} {Quantity}", request.OrderId, request.SkuId, request.Quantity);
        
        var order = await _context.Orders.FindAsync(request.OrderId, cancellationToken);
        var sku = await _context.Skus.FindAsync(request.SkuId, cancellationToken);

        var orderLine = new OrderLine
        {
            Id = NewId.NextGuid(),
            OrderId = order.Id,
            SkuId = sku.Id,
            LineQuantity = request.Quantity
        };

        await _context.OrderLines.AddAsync(orderLine, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        var orderLineAdded = new OrderLineAdded
        {
            OrderId = order.Id,
            OrderLineId = orderLine.Id
        };

        
        await _publisher.Publish(orderLineAdded, cancellationToken);

        return orderLineAdded;
    }
}