using Fw.Domain.Common.Contracts;
using MassTransit.Mediator;

namespace Fw.Domain.Tms.Contracts;

public record RequestShipmentInvoicing : Request<ShipmentInvoicingRequested>

{
    public Guid ShipmentId { get; init; }
}