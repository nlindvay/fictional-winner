namespace Fw.Domain.Tms.Contracts;

public record PackLineSubmitted
{
    public Guid PackLineId { get; set; }
}