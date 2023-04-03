using AutoMapper;
using Fw.Application.Tms.Interfaces;
using Fw.Domain.Common.Contracts;
using Fw.Domain.Tms.Entities;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Tms.Consumers;

public class OrderBookingRequestedConsumer : IConsumer<OrderBookingRequested>

{
    private readonly ITmsDbContext _context;
    private readonly ILogger<OrderBookingRequestedConsumer> _logger;
    private readonly IMapper _mapper;

    public OrderBookingRequestedConsumer(ITmsDbContext context, ILogger<OrderBookingRequestedConsumer> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<OrderBookingRequested> context)
    {
        _logger.LogInformation("OrderBookingRequestedConsumer: {OrderId}", context.Message.Order.Id);

        var shipment = _mapper.Map<Shipment>(context.Message.Order);

        _context.Shipments.Add(shipment);

        await _context.SaveChangesAsync(default);

        await context.Publish(new ShipmentCreated { ShipmentId = shipment.Id, OrderId = context.Message.Order.Id });
    }
}