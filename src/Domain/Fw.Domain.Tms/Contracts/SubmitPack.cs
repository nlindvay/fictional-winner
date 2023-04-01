using MassTransit.Mediator;

namespace Fw.Domain.Tms.Contracts;

public record SubmitPack : Request<PackSubmitted>

{
    public Guid ShipmentId { get; set; }
}