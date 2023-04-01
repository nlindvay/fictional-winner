using Fw.Domain.Common.Dtos;
using Fw.Domain.Common.Enums;
using MassTransit.Mediator;

namespace Fw.Domain.Wms.Contracts;

public record PaginateOrders: Request<OrderDto[]>
{
    public int Page { get; set; }
    public int Size { get; set; }
    public SortDirection Direction { get; set; }
}