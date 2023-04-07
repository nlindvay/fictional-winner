using AutoMapper;
using Fw.Application.Ams.Interfaces;
using Fw.Domain.Ams.Entities;
using Fw.Domain.Common.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Ams.Consumers;

public class InvoiceShipmentConsumer : IConsumer<InvoiceShipment>

{
    readonly IAmsDbContext _context;
    readonly ILogger<InvoiceShipmentConsumer> _logger;
    readonly IMapper _mapper;

    public InvoiceShipmentConsumer(IAmsDbContext context, ILogger<InvoiceShipmentConsumer> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<InvoiceShipment> context)
    {
        _logger.LogInformation("Invoice Shipment {ShipmentId}", context.Message.Shipment.Id);
        
        var invoice = _mapper.Map<Invoice>(context.Message.Shipment);

        _context.Invoices.Add(invoice);

        await _context.SaveChangesAsync(context.CancellationToken);

        await context.Publish(new InvoiceCreated { ShipmentId = context.Message.Shipment.Id, InvoiceId = invoice.Id });
    }
}