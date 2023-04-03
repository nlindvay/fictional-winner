using MassTransit.Mediator;

namespace Fw.Domain.Ams.Contracts;

public record SubmitInvoice : Request<InvoiceSubmitted>

{
    public string PrimaryReference { get; set; }
    public string SecondaryReference { get; set; }
}