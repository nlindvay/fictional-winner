namespace Fw.Domain.Tms.Contracts;

public record PackSubmitted
{
    public Guid PackId { get; set; }
}