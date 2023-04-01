using Fw.Domain.Common.Enums;
using Fw.Domain.Tms.Dtos;
using MassTransit.Mediator;

namespace Fw.Domain.Tms.Contracts;

public class PaginateShipments : Request<ShipmentDto[]>

{
    public int Page { get; set; }
    public int Size { get; set; }
    public SortDirection Direction { get; set; }
}