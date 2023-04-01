using Fw.Domain.Common.Dtos;
using MassTransit.Mediator;

namespace Fw.Domain.Wms.Contracts;

public record GetOrder : Request<OrderDto>
{
    public Guid OrderId { get; set; }
} 