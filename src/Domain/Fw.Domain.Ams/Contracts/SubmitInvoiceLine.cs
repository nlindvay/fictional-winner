using MassTransit.Mediator;

namespace Fw.Domain.Ams.Contracts;

public record SubmitInvoiceLine : Request<InvoiceLineSubmitted>

{
    public Guid InvoiceId { get; set; }
    public int LineNumber { get; set; }
    public int LineQuantity { get; set; }
    public string LineDescription { get; set; }
}