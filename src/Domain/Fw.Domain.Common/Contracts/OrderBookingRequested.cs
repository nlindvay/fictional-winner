using Fw.Domain.Common.Dtos;

namespace Fw.Domain.Common.Contracts;

public record OrderBookingRequested
{
    public OrderDto Order { get; init; }
}