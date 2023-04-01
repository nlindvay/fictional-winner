using Fw.Domain.Common.Dtos;
using MassTransit.Mediator;

namespace Fw.Domain.Tms.Contracts;

public record BookOrder : Request<OrderBooked>

{
    OrderDto Order { get; init; }
}