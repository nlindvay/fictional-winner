using AutoMapper;
using Fw.Application.Tms.Interfaces;
using Fw.Domain.Tms.Contracts;
using Fw.Domain.Tms.Entities;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Tms.Consumers;

public class SubmitPackHandler : MediatorRequestHandler<SubmitPack, PackSubmitted>
{
    readonly ITmsDbContext _context;
    readonly ILogger<SubmitPackHandler> _logger;
    readonly IMapper _mapper;

    public SubmitPackHandler(ITmsDbContext context, ILogger<SubmitPackHandler> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }


    protected override Task<PackSubmitted> Handle(SubmitPack request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Submitting Pack for shipment {ShipmentId}", request.ShipmentId);

        var pack = _mapper.Map<Pack>(request);

        _context.Packs.Add(pack);
        _context.SaveChangesAsync(cancellationToken);


        return Task.FromResult(new PackSubmitted { PackId = pack.Id });
    }
}