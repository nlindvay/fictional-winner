using Fw.Domain.Common.Contracts;
using MassTransit.Mediator;

namespace Fw.Domain.Wms.Contracts;

public record RequestShipOrder : Request<ShipOrder>

{
    public Guid OrderId { get; init; }
}