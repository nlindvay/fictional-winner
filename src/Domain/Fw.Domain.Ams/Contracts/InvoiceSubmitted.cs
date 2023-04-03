namespace Fw.Domain.Ams.Contracts;

public record InvoiceSubmitted
{
    public Guid InvoiceId { get; set; }
}