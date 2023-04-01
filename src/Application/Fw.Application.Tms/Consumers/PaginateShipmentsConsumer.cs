using AutoMapper;
using Fw.Application.Tms.Interfaces;
using Fw.Domain.Tms.Contracts;
using Fw.Domain.Tms.Dtos;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Tms.Consumers;

public class PaginateShipmentsConsumer : MediatorRequestHandler<PaginateShipments, ShipmentDto[]>
{
    readonly ITmsDbContext _context;
    readonly ILogger<PaginateShipmentsConsumer> _logger;
    readonly IMapper _mapper;

    public PaginateShipmentsConsumer(ITmsDbContext context, ILogger<PaginateShipmentsConsumer> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    protected override async Task<ShipmentDto[]> Handle(PaginateShipments request, CancellationToken cancellationToken)
    {
        return await _context.Shipments
            .Include(s => s.Packages)
            .ThenInclude(p => p.PackLines)
            .Skip(request.Page * request.Size)
            .Take(request.Size)
            .Select(s => _mapper.Map<ShipmentDto>(s))
            .ToArrayAsync(cancellationToken);
    }

}