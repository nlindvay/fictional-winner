using MapsterMapper;
using Fw.Application.Ams.Interfaces;
using Fw.Domain.Ams.Contracts;
using Fw.Domain.Ams.Entities;
using Fw.Domain.Common.Contracts;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Ams.Handlers;

public class SubmitInvoiceHandler : MediatorRequestHandler<SubmitInvoice, InvoiceSubmitted>

{
    readonly IAmsDbContext _context;
    readonly ILogger<SubmitInvoiceHandler> _logger;
    readonly IMapper _mapper;

    readonly IPublishEndpoint _publishEndpoint;

    public SubmitInvoiceHandler(IAmsDbContext context, ILogger<SubmitInvoiceHandler> logger, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    protected override async Task<InvoiceSubmitted> Handle(SubmitInvoice request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Submitting Invoice {PrimaryReference}", request.PrimaryReference);

        var invoice = _mapper.Map<Invoice>(request);
        await _context.Invoices.AddAsync(invoice, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var invoiceCreated = new
        {
            InvoiceId = invoice.Id,
            InvoiceStatus = invoice.InvoiceStatus,
            ShipmentId = invoice.ShipmentId,
            OrderId = invoice.OrderId
        };

        await _publishEndpoint.Publish<InvoiceCreated>(invoiceCreated, cancellationToken);

        return new InvoiceSubmitted { InvoiceId = invoice.Id };
    }
}
