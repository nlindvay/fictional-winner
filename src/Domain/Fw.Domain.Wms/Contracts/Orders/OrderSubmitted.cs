namespace Fw.Domain.Wms.Contracts;

public record OrderSubmitted
{
    public Guid OrderId { get; set; }
}