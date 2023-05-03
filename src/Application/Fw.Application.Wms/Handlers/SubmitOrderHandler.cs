using MapsterMapper;
using Fw.Application.Wms.Interfaces;
using Fw.Domain.Common.Enums;
using Fw.Domain.Wms.Contracts;
using Fw.Domain.Wms.Entities;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Wms.Handlers;

public class SubmitOrderHandler : MediatorRequestHandler<SubmitOrder, OrderSubmitted>

{
    readonly IWmsDbContext _context;
    readonly ILogger<SubmitOrderHandler> _logger;
    readonly IMapper _mapper;
    readonly IPublishEndpoint _publisher;

    public SubmitOrderHandler(IWmsDbContext context, ILogger<SubmitOrderHandler> logger, IMapper mapper, IPublishEndpoint publisher)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
        _publisher = publisher;
    }

    protected override async Task<OrderSubmitted> Handle(SubmitOrder request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("SubmitOrderConsumer: {PrimaryReference}", request.PrimaryReference);

        var order = new Order
        {
            Id = NewId.NextGuid(),
            ClientId = NewId.NextGuid(),
            CustomerId = NewId.NextGuid(),
            PrimaryReference = request.PrimaryReference,
            SecondaryReference = request.SecondaryReference,
            OrderStatus = OrderStatus.Draft,
            Version = 1
        };

        await _context.Orders.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var orderSubmitted = new OrderSubmitted
        {
            OrderId = order.Id
        };

        await _publisher.Publish(orderSubmitted, cancellationToken);

        return orderSubmitted;
    }
}