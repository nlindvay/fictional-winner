using MapsterMapper;
using Fw.Application.Ams.Interfaces;
using Fw.Domain.Ams.Contracts;
using Fw.Domain.Ams.Entities;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Ams.Handlers;

public class SubmitInvoiceLineHandler : MediatorRequestHandler<SubmitInvoiceLine, InvoiceLineSubmitted>

{
    readonly IAmsDbContext _context;

    readonly ILogger<SubmitInvoiceLineHandler> _logger;
    readonly IMapper _mapper;

    public SubmitInvoiceLineHandler(IAmsDbContext context, ILogger<SubmitInvoiceLineHandler> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    protected override async Task<InvoiceLineSubmitted> Handle(SubmitInvoiceLine request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Submitting Invoice Line {LineDescription}", request.LineDescription);

        var invoiceLine = _mapper.Map<InvoiceLine>(request);
        await _context.InvoiceLines.AddAsync(invoiceLine, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new InvoiceLineSubmitted { InvoiceLineId = invoiceLine.Id };
    }
}
