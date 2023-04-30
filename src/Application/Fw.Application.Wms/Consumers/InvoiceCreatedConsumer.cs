using AutoMapper;
using Fw.Application.Wms.Interfaces;
using Fw.Domain.Common.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Wms.Consumers;

public class InvoiceCreatedConsumer : IConsumer<InvoiceCreated>
{
    readonly IWmsDbContext _context;
    readonly ILogger<InvoiceCreatedConsumer> _logger;
    readonly IMapper _mapper;

    public InvoiceCreatedConsumer(IWmsDbContext context, ILogger<InvoiceCreatedConsumer> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<InvoiceCreated> context)
    {
        _logger.LogInformation("InvoiceCreatedConsumer: {InvoiceId} {InvoiceStatus}", context.Message.InvoiceId, context.Message.InvoiceStatus);

        if (context.Message.OrderId == null)
        {
            _logger.LogWarning("InvoiceCreatedConsumer: OrderId is empty");
            return;
        }

        var order = await _context.Orders.FirstOrDefaultAsync(order => order.Id == context.Message.OrderId, context.CancellationToken);

        order.InvoiceId = context.Message.InvoiceId;
        order.InvoiceStatus = context.Message.InvoiceStatus;

        await _context.SaveChangesAsync(context.CancellationToken);
    }
}