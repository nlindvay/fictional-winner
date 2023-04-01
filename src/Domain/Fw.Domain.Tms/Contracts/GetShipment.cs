using Fw.Domain.Tms.Dtos;
using MassTransit.Mediator;

namespace Fw.Domain.Tms.Contracts;

public class GetShipment : Request<ShipmentDto>
{
    public Guid ShipmentId { get; set; }
}