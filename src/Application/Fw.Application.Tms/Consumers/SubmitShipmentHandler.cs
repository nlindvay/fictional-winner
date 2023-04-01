using AutoMapper;
using Fw.Application.Tms.Interfaces;
using Fw.Domain.Tms.Contracts;
using Fw.Domain.Tms.Entities;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Tms.Consumers;

public class SubmitShipmentHandler : MediatorRequestHandler<SubmitShipment, ShipmentSubmitted>
{
    readonly ITmsDbContext _context;
    readonly ILogger<SubmitShipmentHandler> _logger;
    readonly IMapper _mapper;

    public SubmitShipmentHandler(ITmsDbContext context, ILogger<SubmitShipmentHandler> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }


    protected override Task<ShipmentSubmitted> Handle(SubmitShipment request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Submitting Shipment {PrimaryReference}", request.PrimaryReference);

        var shipment = _mapper.Map<Shipment>(request);

        _context.Shipments.Add(shipment);
        _context.SaveChangesAsync(cancellationToken);

        return Task.FromResult(new ShipmentSubmitted { ShipmentId = shipment.Id });
    }
}