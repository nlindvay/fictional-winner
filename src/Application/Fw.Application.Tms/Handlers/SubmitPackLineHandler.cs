using AutoMapper;
using Fw.Application.Tms.Interfaces;
using Fw.Domain.Tms.Contracts;
using Fw.Domain.Tms.Entities;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Tms.Handlers;

public class SubmitPackLineHandler : MediatorRequestHandler<SubmitPackLine, PackLineSubmitted>
{
    readonly ITmsDbContext _context;
    readonly ILogger<SubmitPackLineHandler> _logger;
    readonly IMapper _mapper;

    public SubmitPackLineHandler(ITmsDbContext context, ILogger<SubmitPackLineHandler> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }


    protected async override Task<PackLineSubmitted> Handle(SubmitPackLine request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Submitting PackLine {LineDescription}", request.LineDescription);

        var packLine = _mapper.Map<PackLine>(request);

        await _context.PackLines.AddAsync(packLine, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);


        return new PackLineSubmitted { PackLineId = packLine.Id };
    }
}