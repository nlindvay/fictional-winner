using Fw.Domain.Common.Dtos;

namespace Fw.Domain.Common.Contracts;

public record ShipOrder
{
    public OrderDto Order { get; init; }
}