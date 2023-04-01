using AutoMapper;
using Fw.Application.Tms.Interfaces;
using Fw.Domain.Tms.Contracts;
using Fw.Domain.Tms.Dtos;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Tms.Consumers;

public class GetShipmentConsumer : MediatorRequestHandler<GetShipment, ShipmentDto>
{
    readonly ITmsDbContext _context;
    readonly ILogger<GetShipmentConsumer> _logger;
    readonly IMapper _mapper;

    public GetShipmentConsumer(ITmsDbContext context, ILogger<GetShipmentConsumer> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    protected override async Task<ShipmentDto> Handle(GetShipment request, CancellationToken cancellationToken)
    {
        var shipment = await _context.Shipments
            .Include(s => s.Packages)
            .ThenInclude(p => p.PackLines)
            .FirstOrDefaultAsync(s => s.Id == request.ShipmentId, cancellationToken);
            
        return shipment == null? null : _mapper.Map<ShipmentDto>(shipment);
    }
}