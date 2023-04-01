using MassTransit.Mediator;

namespace Fw.Domain.Tms.Contracts;

public record SubmitPackLine : Request<PackLineSubmitted>

{
    public Guid PackId { get; set; }
    public int LineNumber { get; set; }
    public int LineQuantity { get; set; }
    public string LineDescription { get; set; }
}