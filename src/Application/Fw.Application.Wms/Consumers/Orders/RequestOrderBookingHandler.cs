using AutoMapper;
using Fw.Application.Wms.Interfaces;
using Fw.Domain.Common.Contracts;
using Fw.Domain.Wms.Contracts;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Wms.Consumers;

public class RequestOrderBookingHandler : MediatorRequestHandler<RequestOrderBooking, OrderBookingRequested>

{
    readonly IWmsDbContext _context;
    private readonly ILogger<RequestOrderBookingHandler> _logger;
    readonly IMapper _mapper;
    readonly IBus _bus;

    public RequestOrderBookingHandler(IWmsDbContext context, ILogger<RequestOrderBookingHandler> logger, IMapper mapper, IBus bus)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
        _bus = bus;
    }

    protected override async Task<OrderBookingRequested> Handle(RequestOrderBooking request, CancellationToken cancellationToken)
    {
        var order = _context.Orders
            .Include(order => order.OrderLines)
            .FirstOrDefault(order => order.Id == request.OrderId);

        var orderBookingRequested = _mapper.Map<OrderBookingRequested>(order);

        if (order == null)
        {
            _logger.LogInformation("RequestOrderBookingHandler: {OrderId} not found", request.OrderId);
            return null;
        }
        else
        {
            await _bus.Publish(orderBookingRequested, cancellationToken);
            return orderBookingRequested;
        }
    }
}