using MapsterMapper;
using Fw.Application.Tms.Interfaces;
using Fw.Domain.Common.Contracts;
using Fw.Domain.Tms.Contracts;
using Fw.Domain.Tms.Entities;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Tms.Handlers;

public class SubmitShipmentHandler : MediatorRequestHandler<SubmitShipment, ShipmentSubmitted>
{
    readonly ITmsDbContext _context;
    readonly ILogger<SubmitShipmentHandler> _logger;
    readonly IMapper _mapper;
    readonly IPublishEndpoint _publishEndpoint;

    public SubmitShipmentHandler(ITmsDbContext context, ILogger<SubmitShipmentHandler> logger, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }


    protected override async Task<ShipmentSubmitted> Handle(SubmitShipment request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Submitting Shipment {PrimaryReference}", request.PrimaryReference);

        var shipment = _mapper.Map<Shipment>(request);

        await _context.Shipments.AddAsync(shipment, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        await _publishEndpoint.Publish<ShipmentCreated>(new
        {
            ShipmentId = shipment.Id,
            ShipmentStatus = shipment.ShipmentStatus,
            OrderId = shipment.OrderId
        }, cancellationToken);

        return new ShipmentSubmitted { ShipmentId = shipment.Id };
    }
}