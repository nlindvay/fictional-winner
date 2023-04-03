using AutoMapper;
using Fw.Application.Ams.Interfaces;
using Fw.Domain.Ams.Entities;
using Fw.Domain.Common.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Ams.Consumers;

public class ShipmentInvoicingRequestedConsumer : IConsumer<ShipmentInvoicingRequested>
{
    readonly IAmsDbContext _context;
    readonly ILogger<ShipmentInvoicingRequestedConsumer> _logger;
    readonly IMapper _mapper;

    public async Task Consume(ConsumeContext<ShipmentInvoicingRequested> context)
    {
        _logger.LogInformation("Shipment Invoicing Requested {ShipmentId}", context.Message.Shipment.Id);
        
        var invoice = _mapper.Map<Invoice>(context.Message.Shipment);

        _context.Invoices.Add(invoice);

        await _context.SaveChangesAsync(context.CancellationToken);

        await context.Publish(new InvoiceCreated { ShipmentId = context.Message.Shipment.Id, InvoiceId = invoice.Id });
    }
}