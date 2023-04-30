using AutoMapper;
using Fw.Application.Tms.Interfaces;
using Fw.Domain.Common.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Tms.Consumers;

public class InvoiceCreatedConsumer : IConsumer<InvoiceCreated>
{
    readonly ITmsDbContext _context;
    readonly ILogger<InvoiceCreatedConsumer> _logger;
    readonly IMapper _mapper;

    public InvoiceCreatedConsumer(ITmsDbContext context, ILogger<InvoiceCreatedConsumer> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<InvoiceCreated> context)
    {
        _logger.LogInformation("InvoiceCreatedConsumer: {InvoiceId} {InvoiceStatus}", context.Message.InvoiceId, context.Message.InvoiceStatus);

        if (context.Message.ShipmentId == null)
        {
            _logger.LogWarning("InvoiceCreatedConsumer: ShipmentId is empty");
            return;
        }
        var shipment = await _context.Shipments.FindAsync(context.Message.ShipmentId, context.CancellationToken);

        shipment.InvoiceId = context.Message.InvoiceId;
        shipment.InvoiceStatus = context.Message.InvoiceStatus;

        await _context.SaveChangesAsync(context.CancellationToken);
    }
}