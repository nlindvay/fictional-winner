using AutoMapper;
using Fw.Application.Wms.Interfaces;
using Fw.Domain.Common.Enums;
using Fw.Domain.Wms.Contracts;
using Fw.Domain.Wms.Entities;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Wms.Consumers;

public class SubmitOrderHandler : MediatorRequestHandler<SubmitOrder, OrderSubmitted>

{
    readonly IWmsDbContext _context;
    readonly ILogger<SubmitOrderHandler> _logger;
    readonly IMapper _mapper;
    readonly IBus _bus;

    public SubmitOrderHandler(IWmsDbContext context, ILogger<SubmitOrderHandler> logger, IMapper mapper, IBus bus)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
        _bus = bus;
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

        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);

        var orderSubmitted = new OrderSubmitted
        {
            OrderId = order.Id
        };

        await _bus.Publish(orderSubmitted);

        return orderSubmitted;
    }
}