namespace Fw.Domain.Ams.Contracts;

public record InvoiceLineSubmitted
{
    public Guid InvoiceLineId { get; set; }
}