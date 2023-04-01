using Fw.Domain.Common.Contracts;
using MassTransit.Mediator;

namespace Fw.Domain.Wms.Contracts;

public record RequestOrderBooking : Request<OrderBookingRequested>

{
    public Guid OrderId { get; init; }
}