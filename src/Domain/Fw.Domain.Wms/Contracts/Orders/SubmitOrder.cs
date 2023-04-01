using MassTransit.Mediator;

namespace Fw.Domain.Wms.Contracts;

public record SubmitOrder : Request<OrderSubmitted>
{
    public string PrimaryReference { get; set; }
    public string SecondaryReference { get; set; }
}