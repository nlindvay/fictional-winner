using AutoMapper;
using Fw.Application.Wms.Interfaces;
using Fw.Domain.Common.Contracts;
using Fw.Domain.Wms.Contracts;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Wms.Handlers;

public class RequestShipOrderHandler : MediatorRequestHandler<RequestShipOrder, ShipOrder>

{
    readonly IWmsDbContext _context;
    private readonly ILogger<RequestShipOrderHandler> _logger;
    readonly IMapper _mapper;
    readonly IBus _bus;

    public RequestShipOrderHandler(IWmsDbContext context, ILogger<RequestShipOrderHandler> logger, IMapper mapper, IBus bus)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
        _bus = bus;
    }

    protected override async Task<ShipOrder> Handle(RequestShipOrder request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .Include(order => order.OrderLines)
            .FirstOrDefaultAsync(order => order.Id == request.OrderId, cancellationToken);

        var ShipOrder = _mapper.Map<ShipOrder>(order);

        if (order == null)
        {
            _logger.LogInformation("RequestOrderBookingHandler: {OrderId} not found", request.OrderId);
            return null;
        }
        else
        {
            await _bus.Publish(ShipOrder, cancellationToken);
            return ShipOrder;
        }
    }
}