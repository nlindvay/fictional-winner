using AutoMapper;
using Fw.Application.Wms.Interfaces;
using Fw.Domain.Wms.Contracts;
using Fw.Domain.Wms.Entities;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Wms.Consumers;

public class SubmitOrderHandler : MediatorRequestHandler<SubmitOrder, OrderSubmitted>

{
    readonly IApplicationDbContext _context;
    readonly ILogger<SubmitOrderHandler> _logger;
    readonly IMapper _mapper; 

    public SubmitOrderHandler(IApplicationDbContext context, ILogger<SubmitOrderHandler> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
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
            OrderStatus = Domain.Wms.Enums.OrderStatus.Submitted
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);

        return new OrderSubmitted
        {
            OrderId = order.Id
        };
    }
}