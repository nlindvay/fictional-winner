using MapsterMapper;
using Fw.Application.Tms.Interfaces;
using Fw.Domain.Common.Contracts;
using Fw.Domain.Tms.Contracts;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fw.Application.Tms.Handlers;

public class RequestShipmentInvoicingHandler : MediatorRequestHandler<RequestShipmentInvoicing, InvoiceShipment>
{
    readonly ITmsDbContext _context;
    readonly ILogger<RequestShipmentInvoicingHandler> _logger;
    readonly IMapper _mapper;
    readonly IBus _bus;

    public RequestShipmentInvoicingHandler(ITmsDbContext context, ILogger<RequestShipmentInvoicingHandler> logger, IMapper mapper, IBus bus)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
        _bus = bus;
    }

    protected override async Task<InvoiceShipment> Handle(RequestShipmentInvoicing request, CancellationToken cancellationToken)
    {
        var shipment = _context.Shipments
            .Include(shipment => shipment.Packages)
            .ThenInclude(pack => pack.PackLines)
            .FirstOrDefaultAsync(shipment => shipment.Id == request.ShipmentId, cancellationToken);

        var invoiceShipment = _mapper.Map<InvoiceShipment>(shipment);

        if (shipment == null)
        {
            _logger.LogInformation("RequestShipmentInvoicingHandler: {ShipmentId} not found", request.ShipmentId);
            return null;
        }
        else
        {
            await _bus.Publish(invoiceShipment, cancellationToken);
            return invoiceShipment;
        }
    }
}
