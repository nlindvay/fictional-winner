using MassTransit.Mediator;

namespace Fw.Domain.Tms.Contracts;

public record SubmitShipment : Request<ShipmentSubmitted>

{
    public string PrimaryReference { get; set; }
    public string SecondaryReference { get; set; }
}