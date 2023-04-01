using AutoMapper;
using Fw.Application.Wms.Interfaces;
using Fw.Domain.Wms.Contracts;
using Fw.Domain.Wms.Entities;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Wms.Consumers;

public class AddOrderLineHandler : MediatorRequestHandler<AddOrderLine,OrderLineAdded>
{

    readonly IApplicationDbContext _context;
    readonly ILogger<GetOrderHandler> _logger;
    readonly IMapper _mapper; 

    public AddOrderLineHandler(IApplicationDbContext context, ILogger<GetOrderHandler> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    protected override async Task<OrderLineAdded> Handle(AddOrderLine request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("AddOrderLineConsumer: {OrderId} {SkuId} {Quantity}", request.OrderId, request.SkuId, request.Quantity);

        var order = await _context.Orders.FindAsync(request.OrderId);
        var sku = await _context.Skus.FindAsync(request.SkuId);

        var orderLine = new OrderLine
        {
            Id = NewId.NextGuid(),
            OrderId = order.Id,
            SkuId = sku.Id,
            Quantity = request.Quantity
        };

        _context.OrderLines.Add(orderLine);
        await _context.SaveChangesAsync(cancellationToken);

        return new OrderLineAdded
        {
            OrderId = order.Id,
            OrderLineId = orderLine.Id
        };
    }
}