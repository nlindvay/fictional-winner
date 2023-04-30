using AutoMapper;
using Fw.Application.Wms.Interfaces;
using Fw.Domain.Common.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Wms.Consumers;

public class ShipmentCreatedConsumer : IConsumer<ShipmentCreated>
{
    readonly IWmsDbContext _context;
    readonly ILogger<ShipmentCreatedConsumer> _logger;
    readonly IMapper _mapper;

    public ShipmentCreatedConsumer(IWmsDbContext context, ILogger<ShipmentCreatedConsumer> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<ShipmentCreated> context)
    {
        _logger.LogInformation("ShipmentCreatedConsumer: {ShipmentId} {ShipmentStatus}", context.Message.ShipmentId, context.Message.ShipmentStatus);

        if (context.Message.OrderId == null)
        {
            _logger.LogWarning("ShipmentCreatedConsumer: OrderId is empty");
            return;
        }

        var order = await _context.Orders.FirstOrDefaultAsync(order => order.Id == context.Message.OrderId, context.CancellationToken);

        order.ShipmentId = context.Message.ShipmentId;
        order.ShipmentStatus = context.Message.ShipmentStatus;

        await _context.SaveChangesAsync(context.CancellationToken);
    }
}