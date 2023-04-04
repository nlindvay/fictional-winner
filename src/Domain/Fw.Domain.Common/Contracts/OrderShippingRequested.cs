using Fw.Domain.Common.Dtos;

namespace Fw.Domain.Common.Contracts;

public record OrderShippingRequested
{
    public OrderDto Order { get; init; }
}