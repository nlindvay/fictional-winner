using AutoMapper;
using Fw.Application.Tms.Interfaces;
using Fw.Domain.Tms.Contracts;
using Fw.Domain.Tms.Entities;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Tms.Consumers;

public class SubmitPackLineConsumer : MediatorRequestHandler<SubmitPackLine, PackLineSubmitted>
{
    readonly ITmsDbContext _context;
    readonly ILogger<SubmitPackLineConsumer> _logger;
    readonly IMapper _mapper;

    public SubmitPackLineConsumer(ITmsDbContext context, ILogger<SubmitPackLineConsumer> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }


    protected override Task<PackLineSubmitted> Handle(SubmitPackLine request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Submitting PackLine {LineDescription}", request.LineDescription);

        var packLine = _mapper.Map<PackLine>(request);

        _context.PackLines.Add(packLine);
        _context.SaveChangesAsync(cancellationToken);


        return Task.FromResult(new PackLineSubmitted { PackLineId = packLine.Id });
    }
}